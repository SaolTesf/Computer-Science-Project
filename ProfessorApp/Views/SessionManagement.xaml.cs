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
                    // create formatted sessions
                    // also, assign a session number based on date order
                    string sessionNumber = "Session " + (i + 1);
                    var format = new ClassSessionFormatDTO
                    {
                        Date = session.SessionDateTime.ToString("D"),
                        Duration = session.QuizStartTime.ToString("h:mm tt") + " - " + session.QuizEndTime.ToString("h:mm tt"),
                        Password = session.Password,
                        SessionID = session.SessionID,
                        SessionNumber = sessionNumber,
                        AccessCode = session.AccessCode
                    };
                    formattedSessions.Add(format);
                }
            }
            SessionCollectionView.ItemsSource = formattedSessions;
        }

        // load list of quizzes
        private async Task LoadQuizzesAsync()
        {
            var quizzes = await _clientService.GetAllQuizQuestionBanksAsync();
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

            if (string.IsNullOrEmpty(password))
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "You must set a password.";
                return;
            }

            DateTime quizStart = date + start;
            DateTime quizEnd = date + end;

            // make DTO out of info
            var session = new ClassSessionDTO
            {
                CourseID = _courseId,
                SessionDateTime = date,
                QuizStartTime = quizStart,
                QuizEndTime = quizEnd,
                Password = password,
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
                // reset fields and close window (disabled because reselecting them can be tedious)
                // StartTime.Time = default;
                // EndTime.Time = default;
                // AddSessionPopup.IsVisible = false;
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
            StartTime.Time = default;
            EndTime.Time = default;
            AddSessionPopup.IsVisible = false;
            OnAppearing();
        }

        // Open the "add session" pop up when the button is clicked
        private async void OnUpdateSessionClicked(object sender, EventArgs e)
        {
            // Toggle the add session form visibility
            UpdateSessionPopup.IsVisible = !UpdateSessionPopup.IsVisible;
            // tries to get associated sessionID
            if (sender is Button btn && btn.CommandParameter is int sessionID)
            {
                // get quizzes (for new picker)
                var quizzes = await _clientService.GetAllQuizQuestionBanksAsync();
                // get associated session from database
                var session = await _clientService.GetSessionByIDAsync(sessionID);
                if (session != null && quizzes != null)
                {
                    NewSessionDate.Date = session.SessionDateTime;
                    NewStartTime.Time = session.QuizStartTime.TimeOfDay;
                    NewEndTime.Time = session.QuizEndTime.TimeOfDay;
                    NewPassword.Text = session.Password;
                    _sessionId = sessionID;
                }
            }
        }

        // Submit updated session data to database, basically same logic as session creation
        private async void OnSubmitUpdateSessionClicked(object sender, EventArgs e)
        {
            var date = NewSessionDate.Date;
            var start = NewStartTime.Time;
            var end = NewEndTime.Time;
            var password = NewPassword.Text?.Trim();

            if (string.IsNullOrEmpty(password))
            {
                updateStatusLabel.TextColor = Colors.Red;
                updateStatusLabel.Text = "You must set a password.";
                return;
            }

            DateTime quizStart = date + start;
            DateTime quizEnd = date + end;
            // get original session
            var oldSession = await _clientService.GetSessionByIDAsync(_sessionId);

            if (oldSession != null)
            {
                // make new DTO using old session ID and access code
                var session = new ClassSessionDTO
                {
                    SessionID = _sessionId,
                    CourseID = _courseId,
                    SessionDateTime = date,
                    QuizStartTime = quizStart,
                    QuizEndTime = quizEnd,
                    Password = password,
                    AccessCode = oldSession.AccessCode
                };

                try
                {
                    var response = await _clientService.UpdateClassSessionAsync(session);
                    if (!response)
                    {
                        updateStatusLabel.TextColor = Colors.Red;
                        updateStatusLabel.Text = "Failed to update session.";
                        return;
                    }
                    statusLabel.TextColor = Colors.Green;
                    await DisplayAlert("Success", "Session updated successfully.", "OK");
                    UpdateSessionPopup.IsVisible = false;
                    _sessionId = -1;
                    OnAppearing();
                }
                catch (Exception ex)
                {
                    updateStatusLabel.TextColor = Colors.Red;
                    updateStatusLabel.Text = $"{ex.Message}";
                }
            }
        }

        // Cancel adding student button
        private void OnCancelUpdateSessionClicked(object sender, EventArgs e)
        {
            // Hide the form
            UpdateSessionPopup.IsVisible = false;
            _sessionId = -1;
            OnAppearing();
        }

        // attempt to remove a course session, via _sessionID (which should be the one being edited by update pop up)
        private async void OnRemoveSessionClicked(object sender, EventArgs e) {

            // Hide the form
            UpdateSessionPopup.IsVisible = false;
            var success = await _clientService.RemoveClassSessionAsync(_sessionId);
            if (success)
                OnAppearing();
            else
                await DisplayAlert("Error", "Failed to remove session.", "OK");
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
