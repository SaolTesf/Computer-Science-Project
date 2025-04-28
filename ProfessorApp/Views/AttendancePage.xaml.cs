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
        private List<AttendanceViewModel> _allRecords = new();
        private List<ClassSessionDTO> _sessions = new();
        private List<int> _sessionFilterIds = new();

        public AttendancePage(ClientService clientService, int? courseId)
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _clientService = clientService;
            _courseId = courseId;
        }

        private readonly HttpClient _httpClient;
        private const string AttendanceApiBaseUrl = "http://localhost:5225/api/attendance";
        private async void OngetAttendanceClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Success", "Student added successfully.", "OK");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAttendanceAsync();
        }

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
                    AttendanceType = dto.AttendanceType.ToString()
                });
            }
            TypeFilterPicker.ItemsSource = new List<string> { "All", "Present", "Excused", "Unexcused" };
            TypeFilterPicker.SelectedItem = "All";
            ApplyFilters();
        }

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
            AttendanceCollectionView.ItemsSource = filtered.ToList();
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
        private void OnFilterChanged(object sender, EventArgs e) => ApplyFilters();
        private void OnClearFiltersClicked(object sender, EventArgs e)
        {
            FirstNameFilterEntry.Text = string.Empty;
            LastNameFilterEntry.Text = string.Empty;
            UTDIDFilterEntry.Text = string.Empty;
            TypeFilterPicker.SelectedItem = "All";
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
            public string AttendanceType { get; set; } = string.Empty;
        }
    }
}
