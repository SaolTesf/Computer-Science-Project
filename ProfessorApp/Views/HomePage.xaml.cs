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
        private async void GoToCoursePage(object sender, EventArgs e)
        {
            var bindingContext = BindingContext;  
            Navigation.PushAsync(new CoursePage());
        }

        private async void GoToAttPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AttendancePage());
        }

    }
}