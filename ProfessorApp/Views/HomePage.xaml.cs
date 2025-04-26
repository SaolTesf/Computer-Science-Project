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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var prof = _clientService.CurrentProfessor;
            if (prof != null)
                WelcomeLabel.Text = $"Welcome, Professor {prof.LastName}!";
        }

        private async void GoToCoursePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursePage(_clientService));
        }

        private async void GoToAttPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendancePage());
        }
    }
}