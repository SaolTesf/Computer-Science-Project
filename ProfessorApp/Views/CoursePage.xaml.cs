// Sawyer Kamman
// course management page for independent courses
using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;

namespace ProfessorApp.Pages
{
    public partial class CoursePage : ContentPage
    {
        private readonly ClientService _clientService;

        public CoursePage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var courses = await _clientService.GetCoursesByProfessorAsync();
            CourseCollectionView.ItemsSource = courses;
        }

        private async void OnCourseSelected(object sender, SelectionChangedEventArgs e)
        {
            var course = e.CurrentSelection.FirstOrDefault() as CourseDTO;
            if (course == null) return;
            await Navigation.PushAsync(new StudentManagement(_clientService, course.CourseNumber));
            CourseCollectionView.SelectedItem = null; // clear selection
        }
    }
}
