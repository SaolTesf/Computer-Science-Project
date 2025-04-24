// Sawyer Kamman
// accesses CourseManagement API to allow creation and management of courses
using AttendanceShared.DTOs;
using ProfessorApp.Services;

namespace ProfessorApp.Pages
{
    public partial class CoursesPage : ContentPage
    {
        private readonly ClientService _clientService;
        public CoursesPage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
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
                var courses = await _clientService.GetCoursesAsync();
                if (courses != null)
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
                            await Navigation.PushAsync(new CoursePage(_clientService));
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
                await LoadCourses();
            }
            catch (Exception ex)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = $"{ex.Message}";
            }
        }

        private void GoToCourses(object sender, EventArgs e)
        {
            AddCoursePopUp.IsVisible = !AddCoursePopUp.IsVisible;
        }
    }
}
