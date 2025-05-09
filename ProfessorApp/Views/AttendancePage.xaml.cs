/*
This file takes care of the attendance page logic in the Professor App.
It handles loading attendance records, applying filters, and displaying the data in a CollectionView.
*/

using AttendanceShared.DTOs;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using ProfessorApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProfessorApp.Pages
{
    public partial class AttendancePage : ContentPage
    {
        private readonly ClientService _clientService;
        private readonly int? _courseId;
        private AttendanceViewModel? _selectedRecord { get; set; } = null;
        private AttendanceType _selectedAttendanceType;

        private List<AttendanceViewModel> _allRecords = new();
        private List<ClassSessionDTO> _sessions = new();
        private List<int> _sessionFilterIds = new();

        public AttendancePage(ClientService clientService, int? courseId)
        {
            InitializeComponent();
            _clientService = clientService;
            _courseId = courseId;
        }

        private async void OngetAttendanceClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Student added successfully.", "OK");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ChangeAttendancePopUp.IsVisible = false;
            await LoadAttendanceAsync();
        }

        // This method loads attendance records and populates the UI elements
        private async Task LoadAttendanceAsync()
        {
            var all = await _clientService.GetAttendancesByCourseIDAsync(_courseId) ?? new List<AttendanceDTO>();
            var sessions = await _clientService.GetSessionsByCourseIDAsync(_courseId) ?? new List<ClassSessionDTO>();
            _sessions = sessions;
            // populate session filter dropdown
            var sessionLabels = new List<string> { "All" };
            _sessionFilterIds.Clear();
            foreach (var s in _sessions)
            {
                sessionLabels.Add(s.SessionDateTime.ToString("M/d/yyyy"));
                _sessionFilterIds.Add(s.SessionID);
            }
            SessionFilterPicker.ItemsSource = sessionLabels;
            SessionFilterPicker.SelectedIndex = 0;
            _allRecords = new List<AttendanceViewModel>();
            foreach (var dto in all)
            {
                var student = await _clientService.GetStudentByUTDIdAsync(dto.UTDID);
                _allRecords.Add(new AttendanceViewModel
                {
                    SessionID = dto.SessionID,
                    FirstName = student?.FirstName ?? string.Empty,
                    LastName = student?.LastName ?? string.Empty,
                    UTDID = dto.UTDID,
                    SubmissionTime = dto.SubmissionTime.ToString("g"),
                    SubmissionDate = dto.SubmissionTime.Date,
                    AttendanceType = dto.AttendanceType.ToString()
                });
            }
            TypeFilterPicker.ItemsSource = new List<string> { "All", "Present", "Excused", "Unexcused" };
            TypeFilterPicker.SelectedIndex = 0;
            FromDatePicker.Date = FromDatePicker.MinimumDate;
            ToDatePicker.Date = ToDatePicker.MaximumDate;
            ApplyFilters();
        }

        // This method applies the filters based on user input
        private void ApplyFilters()
        {
            var filtered = _allRecords.AsEnumerable();
            // session filter
            if (SessionFilterPicker.SelectedIndex > 0)
            {
                var selId = _sessionFilterIds[SessionFilterPicker.SelectedIndex - 1];
                filtered = filtered.Where(r => r.SessionID == selId);
            }
            var first = FirstNameFilterEntry.Text?.Trim();
            var last = LastNameFilterEntry.Text?.Trim();
            var id = UTDIDFilterEntry.Text?.Trim();
            if (!string.IsNullOrEmpty(first)) filtered = filtered.Where(r => r.FirstName.Contains(first, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(last)) filtered = filtered.Where(r => r.LastName.Contains(last, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(id)) filtered = filtered.Where(r => r.UTDID.Contains(id, StringComparison.OrdinalIgnoreCase));
            // type filter
            var type = TypeFilterPicker.SelectedItem as string;
            if (!string.IsNullOrEmpty(type) && type != "All")
                filtered = filtered.Where(r => r.AttendanceType == type);
            var from = FromDatePicker.Date;
            var to = ToDatePicker.Date;
            if (from > FromDatePicker.MinimumDate || to < ToDatePicker.MaximumDate)
                filtered = filtered.Where(r => r.SubmissionDate.Date >= from && r.SubmissionDate.Date <= to);
            AttendanceCollectionView.ItemsSource = filtered.ToList();
        }

        // Event handlers for filter changes
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
        private void OnFilterChanged(object sender, EventArgs e) => ApplyFilters();
        private void OnDateFilterChanged(object sender, DateChangedEventArgs e) => ApplyFilters();

        // Event handler for clear filters button
        // This resets all filters to their default values
        private void OnClearFiltersClicked(object sender, EventArgs e)
        {
            FirstNameFilterEntry.Text = string.Empty;
            LastNameFilterEntry.Text = string.Empty;
            UTDIDFilterEntry.Text = string.Empty;
            TypeFilterPicker.SelectedItem = "All";
            SessionFilterPicker.SelectedIndex = 0;
            FromDatePicker.Date = FromDatePicker.MinimumDate;
            ToDatePicker.Date = ToDatePicker.MaximumDate;
            ApplyFilters();
        }

        // View model to display in CollectionView
        private class AttendanceViewModel
        {
            public int SessionID { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string UTDID { get; set; } = string.Empty;
            public string SubmissionTime { get; set; } = string.Empty;
            public DateTime SubmissionDate { get; set; }
            public string AttendanceType { get; set; } = string.Empty;
        }
        /*Diego Cabanas: Code to change the picker and save selection for using the button to change attendance type for a student*/
        private void OnChangeAttendanceClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is AttendanceViewModel record)
            {
                _selectedRecord = record;
                ChangeAttendancePopUp.IsVisible = true;
                AttendancePicker.SelectedItem = _selectedRecord.AttendanceType;
            }
        }

        // This method is called when the user clicks the confirm button to change attendance type
        private async void OnConfirmChangeAttendanceClicked(object sender, EventArgs e)   
        {
            if (_selectedRecord == null) return;
            int attendanceId = await _clientService.GetAttendanceIdBySessionAndUtdIdAsync(_selectedRecord.SessionID, _selectedRecord.UTDID);
            try
            {
                var updated = new AttendanceDTO
                {
                    AttendanceID = attendanceId,
                    SessionID = _selectedRecord.SessionID,
                    UTDID = _selectedRecord.UTDID,
                    AttendanceType = _selectedAttendanceType,
                    SubmissionTime = DateTime.Parse(_selectedRecord.SubmissionTime),
                    IPAddress = "N/A"
                };

                var success = await _clientService.UpdateAttendanceAsync(attendanceId,updated);

                if (success)
                {
                    await DisplayAlert("Success", "Attendance updated successfully", "OK");
                    await LoadAttendanceAsync(); 
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update attendance", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to update attendance: {ex.Message}", "OK");
            }
            finally
            {
                ChangeAttendancePopUp.IsVisible = false;
                _selectedRecord = null;
            }
        }

        private void OnCancelChangeAttendanceClicked(object sender, EventArgs e)
        {
            ChangeAttendancePopUp.IsVisible = false;
            _selectedRecord = null;
        }

        private void OnAttendanceTypeChanged(object sender, EventArgs e)
        {
            if (AttendancePicker.SelectedItem is string selectedType)
            {
                if (Enum.TryParse(selectedType, out AttendanceType attendanceType))
                {
                    _selectedAttendanceType = attendanceType;
                }
            }
        }
    }
}