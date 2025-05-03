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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var prof = _clientService.CurrentProfessor;
            if (prof != null)
                WelcomeLabel.Text = $"Welcome, Professor {prof.LastName}!";
            //await LoadSessionsAsync();
        }

        private async void GoToCoursePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursePage(_clientService));
        }

        /*
        private async Task LoadSessionsAsync()
        {
            // get list of sessions
            DateTime thisDay = DateTime.Today;
            var sessions = await _clientService.GetSessionBySessionDateTimeAsync(thisDay);
            var formattedSessions = new List<ClassSessionFormatDTO>();
            if (sessions != null)
            {
                // sort sessions in order of date (ascending)
                var sortedSessions = sessions.OrderBy(session => session.SessionDateTime).ToList();
                for (int i = 0; i < sortedSessions.Count; i++)
                {
                    ClassSessionDTO session = sortedSessions[i];
                    QuizQuestionBankDTO? quiz = await _clientService.GetQuizQuestionBankByIDAsync(session.QuestionBankID);

                    if (quiz != null)
                    {
                        // create formatted sessions
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
        */
    }
}