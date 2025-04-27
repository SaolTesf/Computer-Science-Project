/*Diego: Added function to add questions to specific banks and add question banks*/
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;
using System.Linq;

namespace ProfessorApp.Pages
{
    public partial class QuizPage : ContentPage
    {
        private readonly ClientService _clientService;
        //Helper lists to save the names that show up on the picker
        public List<string> BankList { get; set; } = new List<string>();
        public string? SelectedBank { get; set; }
        // list and string for the question picker func
     
        public QuizPage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
            BindingContext = this;
            LoadBankNamesAsync();
        }
        //Method to load the Bank Names on the drop down menu
        private async void LoadBankNamesAsync()
        {
            var bankNames = await _clientService.GetAllQuizBankNamesAsync();

            BankList = bankNames ?? new List<string>();

            if (BankList.Count > 0)
            {
                SelectedBank = BankList[0];
            }
            OnPropertyChanged(nameof(BankList));
            OnPropertyChanged(nameof(SelectedBank));
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
            }
        }
        //Submit question data to database based on manual
        private async void OnSubmitQuestionClicked(object sender, EventArgs e)
        {
            var questionText = QuestionTextEntry.Text?.Trim();
            var option1 = Option1Entry.Text?.Trim();
            var option2 = Option2Entry.Text?.Trim();
            var option3 = Option3Entry.Text?.Trim();
            var option4 = Option4Entry.Text?.Trim();
            //Checking to see if the question, and at least 2 answer fields are filled in
            if (string.IsNullOrEmpty(questionText) ||
                string.IsNullOrEmpty(option1) || string.IsNullOrEmpty(option2))
            {
                await DisplayAlert("Input Error", "Please fill in all fields.", "OK");
                return;
            }

            //Getting BankID by using the Bank Name chosen from the picker
            int? questionBankID = SelectedBank != null
                ? await _clientService.GetQuestionBankIdByNameAsync(SelectedBank): (int?)null;

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
                    AddQuestionPopup.IsVisible = false;
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
            //Hide the form
            AddQuestionPopup.IsVisible = false;
        }

    }
}
