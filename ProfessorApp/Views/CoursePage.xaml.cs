using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;


namespace ProfessorApp.Pages
{
    public partial class CoursePage : ContentPage
    {
        ClientService _clientService;
        public CoursePage(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
        }

        private void GoToC1Page(object sender, EventArgs e)
        {
            // Add your navigation logic here
            Navigation.PushAsync(new StudentManagement(_clientService));
        }
    }
}
