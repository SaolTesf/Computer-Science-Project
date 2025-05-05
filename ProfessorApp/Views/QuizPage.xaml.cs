/*Diego: 
 * Added function to add questions to specific banks and add question banks
 which are mapped to the specific course the page was accessed from,
it also allows the user to pick what questions they want to use 
for a specific quiz on a specific session*/
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
        public ObservableCollection<QuestionWithSelection> AllQuestionsList { get; set; } = new ObservableCollection<QuestionWithSelection>();
        public string? SelectedFilterBank { get; set; }
        public ObservableCollection<QuestionWithSelection> FilteredQuestionsList { get; set; } = new ObservableCollection<QuestionWithSelection>();
        private StackLayout QuestionsCheckBoxLayout;
        private int? _courseID;

        public QuizPage(ClientService clientService, int? courseID)
        {
            InitializeComponent();
            _clientService = clientService;
            _courseID = courseID;
            BindingContext = this;
            QuestionsCheckBoxLayout = new StackLayout();
            LoadBankNamesAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Reset the picker and checkbox
            BankPicker.SelectedIndex = -1;
            QuestionCollectionView.IsVisible = false;
            LoadAllQuestionsForCourseAsync();
        }
        //Method to load/refresh the banks on the pickers
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

        //Event handler for adding bank 
        private void OnAddBankClicked(object sender, EventArgs e)
        {
            //Toggle the Add Bank form visibility
            AddBankPopup.IsVisible = !AddBankPopup.IsVisible;

            if (AddQuestionPopup.IsVisible)
            {
                BankNameEntry.Text = string.Empty;
            }
        }

        //Submit bank to database 
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
                //Make an API call to add the new bank
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
        //Method to open form to delete bank
        private void OnDeleteBankClicked(object sender, EventArgs e)
        {
            //Toggle the Delete Bank form visibility
            DeleteBankPopup.IsVisible = !DeleteBankPopup.IsVisible;
        }
        private async void OnSubmitDeleteBankClicked(object sender, EventArgs e)
        {
            DeleteConfirmationPopup.IsVisible = true;
        }
        //Method to Delete Bank and All the questions within it
        private async void OnConfirmDeleteBankClicked(object sender, EventArgs e)
        {
            if (SelectedBank == null)
            {
                await DisplayAlert("Error", "Please select a bank to delete.", "OK");
                return;
            }

            //API call to get BankID using the name
            var selectedBankId = await _clientService.GetQuestionBankIdByNameAsync(SelectedBank);

            //API call to Delete the bank using its ID
            bool deleteBankResponse = await _clientService.DeleteQuizQuestionBankAsync(selectedBankId);

            if (deleteBankResponse)
            {
                LoadBankNamesAsync();
                await DisplayAlert("Success", "Bank deleted successfully.", "OK");
                DeleteConfirmationPopup.IsVisible = false;
                DeleteBankPopup.IsVisible = false;
                LoadBankNamesAsync();
                await LoadAllQuestionsForCourseAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to delete bank.", "OK");
            }
        }
        //Cancel Deleting Question Bank
        private void OnCancelDeleteBankClicked(object sender, EventArgs e)
        {
            //Reset Picker inside the form
            BankPicker.SelectedIndex = -1;
            SelectedBank = null;
            OnPropertyChanged(nameof(SelectedBank));
            //Hide delete forms
            DeleteConfirmationPopup.IsVisible = false;
            DeleteBankPopup.IsVisible = false;
        }
        //Event handler for adding quiz question through form 
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
                    BankPicker.SelectedIndex = -1;
                    SelectedBank = null;
                    OnPropertyChanged(nameof(SelectedBank));
                    AddQuestionPopup.IsVisible = false;
                    await OnSelectedBankChangedAsync();
                    await LoadAllQuestionsForCourseAsync();
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
        private void OnCancelAddQuestionClicked(object sender, EventArgs e)
        {
            //Clear all fields
            QuestionTextEntry.Text = string.Empty;
            Option1Entry.Text = string.Empty;
            Option2Entry.Text = string.Empty;
            Option3Entry.Text = string.Empty;
            Option4Entry.Text = string.Empty;
            AnswerEntry.Text = null;
            BankPicker.SelectedIndex = -1;
            SelectedBank = null;
            OnPropertyChanged(nameof(SelectedBank));
            //Hide the form
            AddQuestionPopup.IsVisible = false;
        }

        //Method to notify that there has been a change in the pickers
        private void OnBankPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedBankChangedAsync();
        }

        //Method to help load the questions to the checkboxes
        private async Task OnSelectedBankChangedAsync()
        {
            if (SelectedBank != null)
            {
                var bankId = await _clientService.GetQuestionBankIdByNameAsync(SelectedBank);

                if (bankId != null)
                {
                    //Fetch questions based on the bank ID
                    var questions = await _clientService.GetQuestionsByBankIdAsync(bankId);
                    QuestionCollectionView.IsVisible = true;

                    //Map the questions to a list of QuestionWithSelection 
                    QuestionTextList = questions?.Select(q => new QuestionWithSelection
                    {
                        QuestionText = q.QuestionText,
                        //Initially all checkboxes are unchecked
                        IsChecked = false
                    }).ToList() ?? new List<QuestionWithSelection>();
                    OnPropertyChanged(nameof(QuestionTextList));
                }
            }
        }

        //Button to toggle form for creating a quiz
        private void OnCreateQuizClicked(object sender, EventArgs e)
        {
            //Toggle the Add Quiz form visibility
            CreateQuizPopup.IsVisible = !AddQuestionPopup.IsVisible;
        }
        private void OnCancelCreateQuizClicked(object sender, EventArgs e)
        {
            //Reset the picker and checkbox
            BankPicker.SelectedIndex = -1;
            QuestionCollectionView.IsVisible = false;
            //Hide create quiz form
            CreateQuizPopup.IsVisible = false;
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
            foreach (var question in QuestionTextList)
            {
                question.IsChecked = false;
            }
            BankPicker.SelectedIndex = -1;
            QuestionCollectionView.IsVisible = false;
            CreateQuizPopup.IsVisible = false;

        }
        private async Task LoadAllQuestionsForCourseAsync()
        {
            if (_courseID == null)
            {
                await DisplayAlert("Error", "Course ID is not provided.", "OK");
                return;
            }

            try
            {
                // Fetch all quiz banks for the current course
                var quizBanks = await _clientService.GetQuizBanksByCourseIdAsync((int)_courseID);

                if (quizBanks != null)
                {
                    AllQuestionsList.Clear();

                    foreach (var bank in quizBanks)
                    {
                        // Fetch questions for each bank
                        var questions = await _clientService.GetQuestionsByBankIdAsync(bank.QuestionBankID);

                        if (questions != null)
                        {
                            foreach (var question in questions)
                            {
                                AllQuestionsList.Add(new QuestionWithSelection
                                {
                                    QuestionText = question.QuestionText,
                                    IsChecked = false // Default value
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while loading questions: {ex.Message}", "OK");
            }
        }


    }
}
