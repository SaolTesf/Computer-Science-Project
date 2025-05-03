/*
Intermediate page for a course; navigate to and from attendance and session manager.
Also, allows for the professor to delete the course itself (with confirmation).
 */
using AttendanceShared.DTOs;
using ProfessorApp.Services;

namespace ProfessorApp.Pages
{
    public partial class CourseNavigation : ContentPage
    {

        private readonly ClientService _clientService;
        private readonly int? _courseId;
        public CourseNavigation(ClientService clientService, int? courseId)
        {
            InitializeComponent();
            _clientService = clientService;
            _courseId = courseId;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CourseDTO? course = await _clientService.GetCourseByIdAsync(_courseId);
            if (course != null)
                CourseLabel.Text = $"{course.CourseNumber}.{course.Section} - {course.CourseName}";
        }

        private async void GoToSessionPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SessionManagement(_clientService, _courseId));
        }

        private async void GoToAttPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AttendancePage(_clientService, _courseId));
        }
        private async void GoToStudentManagmentPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentManagement(_clientService, _courseId));
        }
        private async void GoToQuizPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QuizPage(_clientService, _courseId));
        }

        // course deletion stuff

        // Open deletion pop up
        private void ConfirmDelete(object sender, EventArgs e)
        {
            // Show the form
            ConfirmDeletePopUp.IsVisible = true;
        }

        // Cancel deletion
        private void OnConfirmCancelClicked(object sender, EventArgs e)
        {
            // Hide the form
            ConfirmDeletePopUp.IsVisible = false;
        }

        // deletes course
        private async void OnDeleteCourseClicked(object sender, EventArgs e)
        {
            string? message = await _clientService.DeleteCourseByIDAsync(_courseId);
            if (message != null)
            {
                await DisplayAlert("Success", "The course has been deleted.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Course deletion failed.", "OK");
            }
        }
    }
}