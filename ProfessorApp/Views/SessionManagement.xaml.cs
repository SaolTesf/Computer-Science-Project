/*
 Sawyer Kamman
Add or remove class sessions
 */
using AttendanceShared.DTOs;
using ProfessorApp.Services;

namespace ProfessorApp.Pages
{
    public partial class SessionManagement : ContentPage
    {
        private readonly ClientService _clientService;
        private readonly int? _courseId;

        public SessionManagement(ClientService clientService, int? courseId)
        {
            InitializeComponent();
            _clientService = clientService;
            _courseId = courseId;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CourseDTO? course = await _clientService.GetCourseByIdAsync(_courseId);
            if (course != null)
                CourseLabel.Text = $"{course.CourseNumber}.{course.Section} - {course.CourseName}";
            await LoadSessionsAsync();
        }

        // load list of sessions
        private async Task LoadSessionsAsync()
        {
            var sessions = await _clientService.GetSessionsAsync(_courseId);
            SessionCollectionView.ItemsSource = sessions;
        }

        // load list of quizzes
        private async Task LoadQuizzesAsync()
        {
            var quizzes = await _clientService.GetQuizBanksAsync(_courseId);
            QuizPicker.ItemsSource = quizzes;
        }

        // occurs when a quiz is picked
        private void OnQuizSelected(object sender, EventArgs e)
        {
            var selectedQuiz = QuizPicker.SelectedItem as QuizDTO;
        }


        private async void OnRemoveSessionClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is int sessionID)
            {
                var success = await _clientService.RemoveSessionAsync(sessionID);
                if (success)
                    await LoadSessionsAsync();
                else
                    await DisplayAlert("Error", "Failed to remove session.", "OK");
            }
        }

        // Event handler for adding session through form (Add session button)
        private void OnAddSessionClicked(object sender, EventArgs e)
        {
            // Toggle the add session form visibility
            AddSessionPopup.IsVisible = !AddSessionPopup.IsVisible;
        }

        //Submit session data to database
        private async void OnSubmitSessionClicked(object sender, EventArgs e)
        {
            var date = SessionDate.Date;
            var start = StartTime.Time;
            var end = EndTime.Time;
            var password = Password.Text?.Trim();

            if (string.IsNullOrEmpty(password))
            {
                password = "";
            }

            DateTime quizStart = date + start;
            DateTime quizEnd = date + end;


            var session = new ClassSessionDTO
            {
                SessionDate = date,
                StartTime = quizStart,
                EndTime = quizEnd,
                Password = password,
                QuestionBankID = _questionBankID
            };

            try
            {
                var response = await _clientService.AddCourseSessionAsync(session);
                if (!response)
                {
                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = "Failed to add session.";
                    return;
                }
                statusLabel.TextColor = Colors.Green;
                statusLabel.Text = "Added session!";
                OnAppearing();
            }
            catch (Exception ex)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = $"{ex.Message}";
            }
        }

        //Cancel adding student button
        private void OnCancelSessionClicked(object sender, EventArgs e)
        {
            //Hide the form
            AddSessionPopup.IsVisible = false;
        }
    }
}
