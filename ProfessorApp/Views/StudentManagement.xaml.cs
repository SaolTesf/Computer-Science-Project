/*Diego Cabanas
 Functions to Manage students, Add through file, Add manually, or Delete
Delete also deletes any attendance statistics/facts that are associated with the student*/
using System.Text;
using Newtonsoft.Json;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using System.IO;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace ProfessorApp.Pages
{
    public partial class StudentManagement : ContentPage
    {
        private readonly ClientService _clientService;
        private readonly string _courseNumber;
        private readonly int _courseId;
        private List<FileResult> _selectedFiles;

        public StudentManagement(ClientService clientService, string courseNumber, int courseId)
        {
            InitializeComponent();
            _clientService = clientService;
            _courseNumber = courseNumber;
            _courseId = courseId;
            _selectedFiles = new List<FileResult>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadEnrollmentsAsync();
        }

        private async Task LoadEnrollmentsAsync()
        {
            var enrollments = await _clientService.GetEnrollmentsAsync(_courseNumber);
            EnrollmentCollectionView.ItemsSource = enrollments;
        }

        private async void OnUnenrollClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is int enrollmentId)
            {
                var success = await _clientService.UnenrollStudentAsync(enrollmentId);
                if (success)
                    await LoadEnrollmentsAsync();
                else
                    await DisplayAlert("Error", "Failed to remove student.", "OK");
            }
        }

        //Event handler for selecting file
        private async void OnSelectFileClicked(object sender, EventArgs e)
        {
            //Open file picker to select file
            var file = await FilePicker.PickAsync();
            if (file != null)
            {
                try
                {
                    //Validate file content to see if it is the correct format
                    var isValidFile = await ValidateFileHeader(file.FullPath);
                    if (isValidFile)
                    {
                        //If valid, proceed with the file upload
                        _selectedFiles.Add(file);
                        //Update the label to show file names
                        UpdateSelectedFilesLabel(); 
                    }
                    else
                    {
                        //If invalid, show error and cancel further processing
                        await DisplayAlert("Invalid File", "Make sure the file is in the correct format", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Error reading the file: {ex.Message}", "OK");
                }
            }
            else
            {
                Console.WriteLine("No file selected.");
            }
        }

        //Mathod to see if the file has the correct header
        private async Task<bool> ValidateFileHeader(string filePath)
        {
            try
            {
                var lines = await File.ReadAllLinesAsync(filePath);

                //Check if the first line matches the required header
                var header = lines[0].Trim();
                if (header == "Last Name\tFirst Name\tUsername\tStudent ID")
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                //Handle errors 
                Console.WriteLine($"Error reading file header: {ex.Message}");
                return false;
            }
        }

        //Update the label below the upload files button to show files that are going to be uploaded
        private void UpdateSelectedFilesLabel()
        {
            if (_selectedFiles.Count == 0)
            {
                //Default Message
                SelectedFilesLabel.Text = "No files selected.";
            }
            else
            {
                var fileNames = new List<string>();
                foreach (var file in _selectedFiles)
                {
                    fileNames.Add(file.FileName);
                }

                SelectedFilesLabel.Text = "Selected file(s):\n" + string.Join("\n", fileNames);
            }
        }

        //Event handler for submitting the selected files (Submit button)
        private async void OnSubmitFilesClicked(object sender, EventArgs e)
        {
            try
            {
                if (_selectedFiles.Count == 0)
                {
                    await DisplayAlert("No Files", "Please select at least one file before submitting.", "OK");
                    return;
                }

                //Loop through the selected files and process them
                foreach (var file in _selectedFiles)
                {
                    await UploadStudentData(file.FullPath);
                }

                //Clear the files selected label after submission
                SelectedFilesLabel.Text = "No files selected.";

                //Clear the selected files list
                _selectedFiles.Clear();

                await DisplayAlert("Success", "Files submitted successfully.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred while submitting the files: {ex.Message}", "OK");
            }
        }

        //Method called to read and upload the txt file
        public async Task UploadStudentData(string filePath)
        {
            var students = ParseStudentFile(filePath);
            foreach (var student in students)
            {
                if (await _clientService.AddStudentAsync(student))
                {
                    var dto = new CourseEnrollmentDTO { CourseNumber = _courseNumber, UTDID = student.UTDID };
                    await _clientService.EnrollStudentToCourseAsync(dto);
                }
            }
            await LoadEnrollmentsAsync();
        }

        //Parse the txt file into student object
        private List<StudentDTO> ParseStudentFile(string filePath)
        {
            var students = new List<StudentDTO>();

            var lines = File.ReadAllLines(filePath);
            //Skip first line as it is header
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                //Skip empty lines
                if (string.IsNullOrEmpty(line)) continue;
                var parts = line.Split('\t');

                if (parts.Length == 4)
                {
                    var student = new StudentDTO
                    {
                        LastName = parts[0],
                        FirstName = parts[1],
                        Username = parts[2],
                        UTDID = parts[3]
                    };
                    students.Add(student);
                }
            }

            return students;
        }

        //Event handler for adding student through form (Add Student button)
        private void OnAddStudentClicked(object sender, EventArgs e)
        {
            // Toggle the Add Student form visibility
            AddStudentPopup.IsVisible = !AddStudentPopup.IsVisible;

            if (AddStudentPopup.IsVisible)
            {
                FirstNameEntry.Text = string.Empty;
                LastNameEntry.Text = string.Empty;
                UsernameEntry.Text = string.Empty;
                AddStudentUTDIDEntry.Text = string.Empty;
            }
        }

        //Submit student data to database based on manual
        private async void OnSubmitStudentClicked(object sender, EventArgs e)
        {
            var firstName = FirstNameEntry.Text?.Trim();
            var lastName = LastNameEntry.Text?.Trim();
            var username = UsernameEntry.Text?.Trim();
            var utdid = AddStudentUTDIDEntry.Text?.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(utdid))
            {
                await DisplayAlert("Input Error", "Please fill in all fields.", "OK");
                return;
            }

            if (username.Length != 9)
            {
                await DisplayAlert("Validation Error", "Invalid NETID", "OK");
                return;
            }
            else if (utdid.Length != 10)
            {
                await DisplayAlert("Validation Error", "Invalid UTDID", "OK");
                return;
            }

            var student = new StudentDTO
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                UTDID = utdid
            };

            if (await _clientService.AddStudentAsync(student))
            {
                var dto = new CourseEnrollmentDTO { CourseNumber = _courseNumber, UTDID = student.UTDID };
                await _clientService.EnrollStudentToCourseAsync(dto);
                await DisplayAlert("Success", "Student added and enrolled.", "OK");
                AddStudentPopup.IsVisible = false;
                await LoadEnrollmentsAsync();
            }
            else
                await DisplayAlert("Error", "Failed to add student.", "OK");
        }

        //Cancel adding student button
        private void OnCancelClicked(object sender, EventArgs e)
        {
            //Clear all fields
            FirstNameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            UsernameEntry.Text = string.Empty;
            AddStudentUTDIDEntry.Text = string.Empty;

            //Hide the form
            AddStudentPopup.IsVisible = false;
        }

        //Method to delete a student by UTDID
        private async void OnDeleteStudentClicked(object sender, EventArgs e)
        {
            //Toggle the Delete Student form visibility
            DeleteStudentPopup.IsVisible = !DeleteStudentPopup.IsVisible;

            await DisplayAlert("Error", "Deletion unsuccessful", "OK");

            if (DeleteStudentPopup.IsVisible)
            {
                DeleteUTDIDEntry.Text = string.Empty;
            }
        }

        //Submit Deletion form
        private async void OnSubmitDeleteClicked(object sender, EventArgs e)
        {
            var utdid = DeleteUTDIDEntry.Text?.Trim();

            if (string.IsNullOrEmpty(utdid))
            {
                await DisplayAlert("Input Error", "Please enter a UTDID.", "OK");
                return;
            }

            try
            {
                var student = await _clientService.GetStudentByUTDIdAsync(utdid);
                if (student != null)
                {
                    var deleteStudentResponse = await _clientService.DeleteStudentByUTDIdAsync(utdid);
                    var deleteAttendanceResponse = await _clientService.DeleteStudentByUTDIdAsync(utdid);

                    if (!string.IsNullOrEmpty(deleteStudentResponse))
                    {
                        string resultMessage = deleteStudentResponse;
                        await DisplayAlert("Success", resultMessage, "OK");

                        DeleteUTDIDEntry.Text = string.Empty;
                        DeleteStudentPopup.IsVisible = false;
                    }
                    else
                    {
                        await DisplayAlert("Error", $"Failed to delete student {utdid}.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Not Found", $"No student found with UTDID {utdid}.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        //Cancel Deleting a student
        private void OnCancelDeleteClicked(object sender, EventArgs e)
        {
            //Clear the text field
            DeleteUTDIDEntry.Text = string.Empty;

            //Hide delete student form
            DeleteStudentPopup.IsVisible = false;
        }
    }
}
