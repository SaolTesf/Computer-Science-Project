// Sawyer Kamman
// course management page for independent courses
using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;

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

        private void GoToManagement(object sender, EventArgs e)
        {
            // Add your navigation logic here
            Navigation.PushAsync(new StudentManagement(_clientService));
        }
    }
}
