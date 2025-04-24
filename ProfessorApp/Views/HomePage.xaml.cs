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

        private async void GoToCoursePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursesPage(_clientService));
        }

        private async void GoToAttPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendancePage());
        }
    }
}