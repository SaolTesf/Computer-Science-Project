using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProfessorApp.Pages
{
    public partial class AttendancePage : ContentPage
    {
        public AttendancePage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private readonly HttpClient _httpClient;
        private const string AttendanceApiBaseUrl = "http://localhost:5225/api/attendance";
        private async void OngetAttendanceClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Student added successfully.", "OK");
        }
    }
}
