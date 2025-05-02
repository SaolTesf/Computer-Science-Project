using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
            await Navigation.PushAsync(new CourseNavigation(_clientService, course.CourseID));
            CourseCollectionView.SelectedItem = null; // clear selection
        }

        /*
        * Adding courses
        */

        // fires when subject field changes, forces alphanumeric
        public void SubjectTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            string regex = e.NewTextValue;
            if (String.IsNullOrEmpty(regex))
                return;
            if (!Regex.Match(regex, "^[a-zA-Z]+$").Success)
            {
                if (entry != null)
                {
                    entry.Text = (string.IsNullOrEmpty(e.OldTextValue)) ?
                    string.Empty : e.OldTextValue;
                }
            }
            if (entry != null && entry.Text != entry.Text.ToUpper())
            {
                entry.Text = entry.Text.ToUpper();
            }
        }

        // fires when number or section field changes, forces alphanumeric
        public void NumberTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            string regex = e.NewTextValue;
            if (String.IsNullOrEmpty(regex))
                return;
            if (!Regex.Match(regex, "^[a-zA-Z0-9]+$").Success)
            {
                if (entry != null)
                {
                    entry.Text = (string.IsNullOrEmpty(e.OldTextValue)) ?
                    string.Empty : e.OldTextValue;
                }
            }
            if (entry != null && entry.Text != entry.Text.ToUpper())
            {
                entry.Text = entry.Text.ToUpper();
            }
        }

        // fires when add course button is clicked
        private async void AddCourse(object sender, EventArgs e)
        {
            // get fields
            string courseSubject = courseSubjectEntry.Text ?? "";
            string courseNumber = courseNumberEntry.Text ?? "";
            string courseName = courseNameEntry.Text ?? "";
            string section = sectionEntry.Text ?? "";

            // check if any empty fields
            if (string.IsNullOrEmpty(courseSubject)
                || string.IsNullOrEmpty(courseNumber)
                || string.IsNullOrEmpty(courseName)
                || string.IsNullOrEmpty(section))
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Please enter all fields.";
                return;
            }

            // validate course (length of 4)
            if (courseNumber.Length != 4)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Course must be 4 characters.";
                return;
            }

            // validate section (length of 3)
            if (section.Length != 3)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Section must be 3 characters.";
                return;
            }

            var prof = _clientService.CurrentProfessor;
            if (prof != null)
            {
                string courseCode = courseSubject + " " + courseNumber;
                CourseDTO courseDto = new CourseDTO
                {
                    CourseNumber = courseCode,
                    CourseName = courseName,
                    Section = section,
                    ProfessorID = prof.ID
                };

                try
                {
                    var course = courseDto;

                    var courses = await _clientService.GetAllCoursesAsync();
                    if (courses != null)
                    {
                        foreach (CourseDTO c in courses)
                        {
                            if (c.CourseNumber.Equals(courseCode) && c.Section.Equals(section))
                            {
                                statusLabel.TextColor = Colors.Red;
                                statusLabel.Text = "Course section already exists.";
                                return;
                            }
                        }
                    }

                    bool response = await _clientService.AddCourseAsync(course);
                    if (!response)
                    {
                        statusLabel.TextColor = Colors.Red;
                        statusLabel.Text = "Failed to add course.";
                        return;
                    }
                    statusLabel.TextColor = Colors.Green;
                    await DisplayAlert("Success", "Course Created successfully.", "OK");
                    OnAppearing();
                    courseSubjectEntry.Text = string.Empty;
                    courseNumberEntry.Text = string.Empty;
                    courseNameEntry.Text = string.Empty;
                    sectionEntry.Text = string.Empty;
                    AddCoursePopUp.IsVisible = false;
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
            OnAppearing();
            courseSubjectEntry.Text = string.Empty;
            courseNumberEntry.Text = string.Empty;
            courseNameEntry.Text = string.Empty;
            sectionEntry.Text = string.Empty;
            AddCoursePopUp.IsVisible = false;
        }
    }
}