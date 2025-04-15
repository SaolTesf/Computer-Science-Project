using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;


namespace ProfessorApp.Pages
{
    public partial class HomePage : ContentPage
    {
        private async void GoToSMPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(StudentManagement));
        }

    }
}