using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;


namespace ProfessorApp.Pages
{
    public partial class HomePage : ContentPage
    {
        private readonly ClientService _clientService;

        public HomePage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
        }
        private async void GoToSMPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(StudentManagement));
        }

    }
}