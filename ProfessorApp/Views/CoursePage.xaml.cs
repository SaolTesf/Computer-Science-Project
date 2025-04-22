// Sawyer Kamman
// course management page for independent courses
using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;

namespace ProfessorApp.Pages
{
    public partial class CoursePage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private string _id = "";
        public CoursePage(HttpClient httpClient)
        {
            InitializeComponent();
            _httpClient = httpClient;
        }

        private void GoToManagement(object sender, EventArgs e)
        {
            
        }
    }
}
