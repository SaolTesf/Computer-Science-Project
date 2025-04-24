// Sawyer Kamman
// accesses CourseManagement API to allow creation and management of courses
using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;

namespace ProfessorApp.Pages
{
    public partial class CoursesPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private const string CourseApiBaseUrl = "http://localhost:5225/api/course";
        public CoursesPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        // pop up when add course clicked
        private void OnAddCourse(object sender, EventArgs e)
        {
            AddCoursePopUp.IsVisible = !AddCoursePopUp.IsVisible;
        }

        // course list on xaml
        private async Task LoadCourses()
        {
            try
            {
                // refresh buttons
                CourseButtonContainer.Children.Clear();

                // get all of the courses from the database then convert them to the correct format
                var courseResponse = await GetCoursesFromDatabase();
                var json = await courseResponse.Content.ReadAsStringAsync();
                var courses = JsonConvert.DeserializeObject<List<CourseDTO>>(json);
                if (courses != null && courseResponse.IsSuccessStatusCode)
                {
                    foreach (var course in courses)
                    {
                        // create a button for each course
                        var button = new Button
                        {
                            Text = $"{course.CourseNumber}.{course.Section} - {course.CourseName}",
                            TextColor = Colors.White,
                            BackgroundColor = Colors.Black,
                            BorderColor = Colors.White,
                            BorderWidth = 2,
                            CornerRadius = 10,
                            Margin = new Thickness(0, 5, 0, 5),
                        };

                        // attach function to the button to navigate to a respective management page
                        button.Clicked += async (sender, e) =>
                        {
                            await Navigation.PushAsync(new CoursePage(_httpClient));
                        };

                        CourseButtonContainer.Add(button);
                    }
                }
            } 
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load courses: {ex.Message}", "OK");
            }
        }

        // load courses on refresh
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadCourses();
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

            CourseDTO courseDto = new CourseDTO{ CourseNumber = courseNumber, 
                CourseName = courseName, 
                Section = section, 
                ProfessorID = "1111111111" 
            };

            try
            {
                var response = await AddCourseToDatabase(courseDto);
                if (!response.IsSuccessStatusCode)
                {
                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = "Course section already exists.";
                    return;
                }
                statusLabel.TextColor = Colors.Green;
                statusLabel.Text = "Created course!";
                await LoadCourses();
            }
            catch (Exception ex)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = $"{ex.Message}";
            }
        }
        private async Task<HttpResponseMessage> AddCourseToDatabase(CourseDTO course)
        {
            var courseJson = JsonConvert.SerializeObject(course);
            var content = new StringContent(courseJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(CourseApiBaseUrl, content);
            return response;
        }
        private async Task<HttpResponseMessage> GetCoursesFromDatabase()
        {
            var response = await _httpClient.GetAsync(CourseApiBaseUrl);
            return response;
        }

        private void GoToCourses(object sender, EventArgs e)
        {
            AddCoursePopUp.IsVisible = !AddCoursePopUp.IsVisible;
        }
    }
}
