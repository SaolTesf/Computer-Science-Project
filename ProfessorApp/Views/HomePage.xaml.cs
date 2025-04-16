using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;


namespace ProfessorApp.Pages
{
    public partial class HomePage : ContentPage
    {

        public HomePage(object bindingContext)
        {
            InitializeComponent();
            BindingContext = bindingContext;
        }
        private async void GoToSMPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StudentManagement());
        }

        private async void GoToAttPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AttendancePage());
        }

    }
}