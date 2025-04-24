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

        // pop up when add course clicked
        private void OnAddCourse(object sender, EventArgs e)
        {
            AddCoursePopUp.IsVisible = !AddCoursePopUp.IsVisible;
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

        /*
        * Adding courses
        */

        // fires when add course button is clicked
        private async void AddCourse(object sender, EventArgs e)
        {
            // get fields
            string courseNumber = courseIdEntry.Text ?? "";
            string courseName = courseNameEntry.Text ?? "";
            string section = sectionEntry.Text ?? "";

            // check if any empty fields
            if (string.IsNullOrEmpty(courseNumber)
                || string.IsNullOrEmpty(courseName)
                || string.IsNullOrEmpty(section))
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Please enter all fields.";
                return;
            }


            var prof = _clientService.CurrentProfessor;
            if (prof != null)
            {
                CourseDTO courseDto = new CourseDTO
                {
                    CourseNumber = courseNumber,
                    CourseName = courseName,
                    Section = section,
                    ProfessorID = prof.ID
                };

                try
                {
                    var course = courseDto;

                    bool response = await _clientService.AddCourseAsync(course);
                    if (!response)
                    {
                        statusLabel.TextColor = Colors.Red;
                        statusLabel.Text = "Course section already exists.";
                        return;
                    }
                    statusLabel.TextColor = Colors.Green;
                    statusLabel.Text = "Created course!";
                    OnAppearing();
                }
                catch (Exception ex)
                {
                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = $"{ex.Message}";
                }
            } 
            else
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Unable to get session";
            }
        }
        private void GoToCourses(object sender, EventArgs e)
        {
            AddCoursePopUp.IsVisible = !AddCoursePopUp.IsVisible;
        }
    }
}