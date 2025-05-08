/*
 Sawyer Kamman
Add or remove class sessions
 */
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using System.Diagnostics;
using Microsoft.Maui.Storage;

namespace ProfessorApp.Pages
{
    public partial class SessionManagement : ContentPage
    {
        private readonly ClientService _clientService;
        private readonly int? _courseId;
        private bool isStartTimeChangingProgrammatically = false;
        private bool isEndTimeChangingProgrammatically = false;

        public SessionManagement(ClientService clientService, int? courseId)
        {
            InitializeComponent();
            _clientService = clientService;
            _courseId = courseId;
            StartTime.PropertyChanged += StartTime_PropertyChanged;
            EndTime.PropertyChanged += EndTime_PropertyChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CourseDTO? course = await _clientService.GetCourseByIdAsync(_courseId);
            if (course != null)
                CourseLabel.Text = $"{course.CourseNumber}.{course.Section} - {course.CourseName}";
            await LoadSessionsAsync();
            await LoadQuizzesAsync();
            statusLabel.Text = null;
        }

        // load list of sessions
        private async Task LoadSessionsAsync()
        {
            // get list of sessions
            var sessions = await _clientService.GetSessionsByCourseIDAsync(_courseId);
            var formattedSessions = new List<ClassSessionFormatDTO>();
            if (sessions != null)
            {
                // sort sessions in order of date (ascending)
                var sortedSessions = sessions.OrderBy(session => session.SessionDateTime).ToList();
                for (int i = 0; i < sortedSessions.Count; i++) {
                    ClassSessionDTO session = sortedSessions[i];
                    QuizQuestionBankDTO? quiz = await _clientService.GetQuizQuestionBankByIDAsync(session.QuestionBankID);
                    
                    if (quiz != null) {
                        // create formatted sessions
                        string sessionNumber = "Session " + (i + 1);
                        var format = new ClassSessionFormatDTO
                        {
                            Date = session.SessionDateTime.ToString("D"),
                            Duration = session.QuizStartTime.ToString("h:mm tt") + " - " + session.QuizEndTime.ToString("h:mm tt"),
                            Password = session.Password,
                            Quiz = quiz.BankName,
                            SessionID = session.SessionID,
                            SessionNumber = sessionNumber,
                            AccessCode = session.AccessCode
                        };
                        formattedSessions.Add(format);
                    }
                }
            }
            SessionCollectionView.ItemsSource = formattedSessions;
        }

        // load list of quizzes
        private async Task LoadQuizzesAsync()
        {
            var quizzes = await _clientService.GetAllQuizQuestionBanksAsync();
            QuizPicker.ItemsSource = quizzes;
        }

        private async void OnRemoveSessionClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is int sessionID)
            {
                var success = await _clientService.RemoveClassSessionAsync(sessionID);
                if (success)
                    OnAppearing();
                else
                    await DisplayAlert("Error", "Failed to remove session.", "OK");
            }
        }

        // Open the "add session" pop up when the button is clicked
        private void OnAddSessionClicked(object sender, EventArgs e)
        {
            // Toggle the add session form visibility
            AddSessionPopup.IsVisible = !AddSessionPopup.IsVisible;
        }

        // Submit session data to database
        private async void OnSubmitSessionClicked(object sender, EventArgs e)
        {
            var date = SessionDate.Date;
            var start = StartTime.Time;
            var end = EndTime.Time;
            var password = Password.Text?.Trim();
            var quiz = QuizPicker.SelectedItem as QuizQuestionBankDTO;

            if (quiz == null)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "You must select a quiz.";
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "You must set a password.";
                return;
            }

            DateTime quizStart = date + start;
            DateTime quizEnd = date + end;
            int quizID = quiz.QuestionBankID;

            var session = new ClassSessionDTO
            {
                CourseID = _courseId,
                SessionDateTime = date,
                QuizStartTime = quizStart,
                QuizEndTime = quizEnd,
                Password = password,
                QuestionBankID = quizID,
                AccessCode = GenerateUniqueAccessCode()
            };

            try
            {
                var response = await _clientService.AddClassSessionAsync(session);
                if (!response)
                {
                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = "Failed to add session.";
                    return;
                }
                statusLabel.TextColor = Colors.Green;
                await DisplayAlert("Success", "Session added successfully.", "OK");
                Password.Text = string.Empty;
                QuizPicker.SelectedIndex = -1;
                StartTime.Time = default;
                EndTime.Time = default;
                AddSessionPopup.IsVisible = false;
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
            //Hide the form and reset fields
            Password.Text = string.Empty;
            QuizPicker.SelectedIndex = -1;
            StartTime.Time = default;
            EndTime.Time = default;
            AddSessionPopup.IsVisible = false;
            OnAppearing();
        }

        private void StartTime_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StartTime.Time) && !isStartTimeChangingProgrammatically)
            {
                var newStartTime = StartTime.Time;
                var newEndTime = newStartTime.Add(new TimeSpan(0, 20, 0));

       
                if (newEndTime >= new TimeSpan(24, 0, 0))
                {
                    newEndTime = newEndTime.Subtract(new TimeSpan(24, 0, 0));
                }

                isStartTimeChangingProgrammatically = true;
                EndTime.Time = newEndTime;
                isStartTimeChangingProgrammatically = false;
            }
        }
        private void EndTime_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EndTime.Time) && !isEndTimeChangingProgrammatically)
            {
                if (EndTime.Time < StartTime.Time)
                {
                   
                    var newEndTime = EndTime.Time;
                    var newStartTime = newEndTime.Subtract(new TimeSpan(0, 20, 0));

              
                    if (newStartTime < TimeSpan.Zero)
                    {
                        newStartTime = newStartTime.Add(new TimeSpan(24, 0, 0));
                    }

                    isEndTimeChangingProgrammatically = true;
                    StartTime.Time = newStartTime;
                    isEndTimeChangingProgrammatically = false;
                }
            }
        }

        private async void OnCopyAccessCodeClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string code)
            {
                await Clipboard.Default.SetTextAsync(code);
                await DisplayAlert("Copied", "Access code copied to clipboard", "OK");
            }
        }

        public string GenerateUniqueAccessCode()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
