/*
Saol Tesfaghebriel
This file is part of the ProfessorApp project, which is a mobile application for managing attendance and courses.
*/

using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;

namespace ProfessorApp.Pages
{
    public partial class HomePage : ContentPage
    {
        private readonly ClientService _clientService;

        public HomePage(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var prof = _clientService.CurrentProfessor;
            if (prof != null)
                WelcomeLabel.Text = $"Welcome, Professor {prof.LastName}!";
            await LoadSessionsAsync();
        }

        private async void GoToCoursePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursePage(_clientService));
        }

        private async Task LoadSessionsAsync()
        {
            DateTime today = DateTime.Today;
            var currentProfessor = _clientService.CurrentProfessor;

            if (currentProfessor == null)
                return;

            //Get all courses taught by the current professor
            var professorCourses = await _clientService.GetCoursesByProfessorAsync();
            if (professorCourses == null)
                return;

            //Extract the CourseIDs of the professor's courses
            var professorCourseIds = professorCourses.Select(course => course.CourseID).ToHashSet();

            //Get all sessions for today
            var sessions = await _clientService.GetSessionBySessionDateTimeAsync(today);
            var formattedSessions = new List<ClassSessionFormatDTO>();

            if (sessions != null)
            {
                //Filter sessions to include only those belonging to the professor's courses
                var filteredSessions = sessions
                    .Where(session => session.CourseID.HasValue && professorCourseIds.Contains(session.CourseID.Value))
                    .OrderBy(session => session.SessionDateTime)
                    .ToList();

                for (int i = 0; i < filteredSessions.Count; i++)
                {
                    ClassSessionDTO session = filteredSessions[i];
                    QuizQuestionBankDTO? quiz = await _clientService.GetQuizQuestionBankByIDAsync(session.QuestionBankID);
                    CourseDTO? course = professorCourses.FirstOrDefault(c => c.CourseID == session.CourseID);

                    if (quiz != null && course != null)
                    {
                        //Create formatted sessions
                        string sessionNumber = "Session " + (i + 1);
                        var format = new ClassSessionFormatDTO
                        {
                            Date = session.SessionDateTime.ToString("D"),
                            Duration = session.QuizStartTime.ToString("h:mm tt") + " - " + session.QuizEndTime.ToString("h:mm tt"),
                            Password = session.Password,
                            Quiz = quiz.BankName,
                            SessionID = session.SessionID,
                            SessionNumber = sessionNumber
                        };
                        formattedSessions.Add(format);
                    }
                }
            }

            SessionCollectionView.ItemsSource = formattedSessions;
        }
    }
}