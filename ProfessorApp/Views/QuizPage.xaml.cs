/*Diego: Added function to add questions to specific banks and add question banks*/
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Collections.ObjectModel;

namespace ProfessorApp.Pages
{
    public class QuestionWithSelection
    {
        public string QuestionText { get; set; } = string.Empty;
        public bool IsChecked { get; set; }
    }
    public partial class QuizPage : ContentPage
    {
        private readonly ClientService _clientService;
        public ObservableCollection<string> BankList { get; set; } = new ObservableCollection<string>();
        public string? SelectedBank { get; set; }
        public List<QuestionWithSelection> QuestionTextList { get; set; } = new List<QuestionWithSelection>();
        private StackLayout QuestionsCheckBoxLayout;
        private int? _courseID;

        public QuizPage(ClientService clientService, int? courseID)
        {
            InitializeComponent();
            _clientService = clientService;
            _courseID = courseID; //Store the course ID
            BindingContext = this;
            QuestionsCheckBoxLayout = new StackLayout();
            LoadBankNamesAsync();
        }

        private async void LoadBankNamesAsync()
        {
            if (_courseID == null)
            {
                await DisplayAlert("Error", "Course ID is not provided.", "OK");
                return;
            }

            //Fetch banks associated with the specific course
            var bankNames = await _clientService.GetQuizBanksByCourseIdAsync((int)_courseID);

            BankList.Clear();
            foreach (var bankName in bankNames)
            {
                BankList.Add(bankName.BankName);
            }
            OnPropertyChanged(nameof(BankList));
            OnPropertyChanged(nameof(SelectedBank));
        }

        //Event handler for adding bank question through form (Add Question button)
        private void OnAddBankClicked(object sender, EventArgs e)
        {
            //Toggle the Add Question form visibility
            AddBankPopup.IsVisible = !AddBankPopup.IsVisible;

            if (AddQuestionPopup.IsVisible)
            {
                BankNameEntry.Text = string.Empty;
            }
        }

        //Submit question data to database based on manual
        private async void OnSubmitQuizBankClicked(object sender, EventArgs e)
        {
            var bankName = BankNameEntry.Text?.Trim();

            //Validate the input
            if (string.IsNullOrEmpty(bankName))
            {
                await DisplayAlert("Input Error", "Please fill in the quiz bank name.", "OK");
                return;
            }

            //Check if the CourseID is null
            if (_courseID == null)
            {
                await DisplayAlert("Error", "Course ID is not provided.", "OK");
                return;
            }

            //Create the new QuizQuestionBankDTO object with the current CourseID
            var newBank = new QuizQuestionBankDTO
            {
                BankName = bankName,
                CourseID = (int)_courseID
            };

            try
            {
                //Make an API call to add the new quiz bank
                bool response = await _clientService.CreateQuizQuestionBankAsync(newBank);

                if (response)
                {
                    await DisplayAlert("Success", "Quiz Bank added successfully.", "OK");

                    //Close the form and refresh the list of banks
                    BankPicker.SelectedItem = null;
                    AddBankPopup.IsVisible = false;
                    LoadBankNamesAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add quiz bank. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void OnCancelQuizBankClicked(object sender, EventArgs e)
        {
            //Clear all fields
            BankNameEntry.Text = string.Empty;
            //Hide the form
            AddBankPopup.IsVisible = false;
        }
        //Method to delete a student by UTDID
        private void OnDeleteBankClicked(object sender, EventArgs e)
        {
            //Toggle the Delete Student form visibility
            DeleteBankPopup.IsVisible = !DeleteBankPopup.IsVisible;
        }

        //Method to Delete Bank and All the questions within it
        private void OnSubmitDeleteBankClicked(object sender, EventArgs e)
        {
            if (SelectedBank == null)
            {
                await DisplayAlert("Error", "Please select a bank to delete.", "OK");
            }
            var selected = await _clientService.GetQuestionBankIdByNameAsync(SelectedBank);
            var deleteBankResponse = await _clientService.DeleteQuizQuestionBankAsync(selected);
            if (deleteBankResponse)
            {
                LoadBankNamesAsync();
                await Application.Current.MainPage.DisplayAlert("Success", "Bank deleted successfully.", "OK");
                DeleteBankPopup.IsVisible = false;
                LoadBankNamesAsync();
            }
            else
            {
                await DisplayAlert("Error", $"Failed to delete bank.", "OK");
            }
        }
        //Cancel Deleting Question Bank
        private void OnCancelDeleteBankClicked(object sender, EventArgs e)
        {
            //Hide delete student form
            DeleteBankPopup.IsVisible = false;
        }
        //Event handler for adding quiz question through form (Add Question button)
        private void OnAddQuestionClicked(object sender, EventArgs e)
        {
            //Toggle the Add Question form visibility
            AddQuestionPopup.IsVisible = !AddQuestionPopup.IsVisible;

            if (AddQuestionPopup.IsVisible)
            {
                QuestionTextEntry.Text = string.Empty;
                Option1Entry.Text = string.Empty;
                Option2Entry.Text = string.Empty;
                Option3Entry.Text = string.Empty;
                Option4Entry.Text = string.Empty;
                AnswerEntry.Text = null;
                SelectedBank = null;
            }
        }
        private async void OnSubmitQuestionClicked(object sender, EventArgs e)
        {
            var questionText = QuestionTextEntry.Text?.Trim();
            var option1 = Option1Entry.Text?.Trim();
            var option2 = Option2Entry.Text?.Trim();
            var option3 = Option3Entry.Text?.Trim();
            var option4 = Option4Entry.Text?.Trim();
            var answer = AnswerEntry.Text?.Trim();
            var parsed = 0;
            //Checking to see if the question, and at least 2 answer fields are filled in
            if (string.IsNullOrEmpty(questionText) ||
                string.IsNullOrEmpty(option1) || string.IsNullOrEmpty(option2))
            {
                await DisplayAlert("Input Error", "Please fill in all fields.", "OK");
                return;
            }
            else if (!int.TryParse(answer, out int parsedAnswer) || parsedAnswer < 1 || parsedAnswer > 4)
            {
                await DisplayAlert("Input Error", "Answer must be a number between 1 and 4.", "OK");
                return;
            }
            else
            {
                parsed = parsedAnswer;

                //Check if the selected answer option is filled
                string selectedOptionText = parsed switch
                {
                    1 => option1,
                    2 => option2,
                    3 => option3,
                    4 => option4,
                    _ => null
                };

                if (string.IsNullOrWhiteSpace(selectedOptionText))
                {
                    await DisplayAlert("Input Error", $"Option {parsed} is empty. Please fill it in or select a different option for an answer.", "OK");
                    return;
                }
            }

            //Getting BankID by using the Bank Name chosen from the picker
            int? questionBankID = SelectedBank != null ? await _clientService.GetQuestionBankIdByNameAsync(SelectedBank) : (int?)null;

            if (questionBankID == null)
            {
                await DisplayAlert("Error", $"Could not find ID for bank '{SelectedBank}'.", "OK");
                return;
            }

            //Creating question item
            var question = new QuizQuestionDTO
            {
                QuestionBankID = (int)questionBankID,
                QuestionText = questionText,
                Option1 = option1,
                Option2 = option2,
                Option3 = option3,
                Option4 = option4,
                Answer = parsed
            };

            try
            {
                //API call to create new question
                bool response = await _clientService.CreateQuizQuestionAsync(question);
                if (response)
                {
                    await DisplayAlert("Success", "Question Added successfully.", "OK");

                    QuestionTextEntry.Text = string.Empty;
                    Option1Entry.Text = string.Empty;
                    Option2Entry.Text = string.Empty;
                    Option3Entry.Text = string.Empty;
                    Option4Entry.Text = string.Empty;
                    AnswerEntry.Text = null;
                    SelectedBank = null;
                    BankPicker.SelectedItem = null;
                    QuestionTextList.Clear();
                    AddQuestionPopup.IsVisible = false;
                    await OnSelectedBankChangedAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add question. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        //Cancel adding question button
        private void OnCancelClicked(object sender, EventArgs e)
        {
            //Clear all fields
            QuestionTextEntry.Text = string.Empty;
            Option1Entry.Text = string.Empty;
            Option2Entry.Text = string.Empty;
            Option3Entry.Text = string.Empty;
            Option4Entry.Text = string.Empty;
            AnswerEntry.Text = null;
            SelectedBank = null;
            //Hide the form
            AddQuestionPopup.IsVisible = false;
        }

        private void OnBankPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedBankChangedAsync();
        }

        private async Task OnSelectedBankChangedAsync()
        {
            if (SelectedBank != null)
            {
                var bankId = await _clientService.GetQuestionBankIdByNameAsync(SelectedBank);

                if (bankId != null)
                {
                    //Fetch questions based on the bank ID
                    var questions = await _clientService.GetQuestionsByBankIdAsync(bankId);

                    //Map the questions to a list of QuestionWithSelection 
                    QuestionTextList = questions?.Select(q => new QuestionWithSelection
                    {
                        QuestionText = q.QuestionText,
                        //Initially, all checkboxes are unchecked
                        IsChecked = false
                    }).ToList() ?? new List<QuestionWithSelection>();

                    OnPropertyChanged(nameof(QuestionTextList));
                }
            }
        }
        //Method to save the selected questions to a list
        private void OnSubmitSelectedQuestionsClicked(object sender, EventArgs e)
        {
            var selectedQuestions = QuestionTextList.Where(q => q.IsChecked).ToList();

            if (selectedQuestions.Count == 0)
            {
                DisplayAlert("No Selection", "Please select at least one question.", "OK");
                return;
            }

            string selectedQuestionTexts = string.Join("\n", selectedQuestions.Select(q => q.QuestionText));
            DisplayAlert("Selected Questions", selectedQuestionTexts, "OK");

        }

    }
}
