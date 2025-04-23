using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceShared.DTOs;
using ProfessorApp.Services;
using Microsoft.Maui.Controls;

namespace ProfessorApp.Pages
{
    public partial class QuizPage : ContentPage
    {
        private readonly ClientService _clientService;
        public List<string> BankList { get; set; } = new List<string>();
        public string? SelectedBank { get; set; }

        public QuizPage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
            BindingContext = this;
            LoadBankNamesAsync();
        }

        private async void LoadBankNamesAsync()
        {
            var bankNames = await _clientService.GetAllQuizBankNamesAsync();

            BankList = bankNames ?? new List<string>();

            if (BankList.Count > 0)
            {
                SelectedBank = BankList[0]; 
            }
            OnPropertyChanged(nameof(BankList));
            OnPropertyChanged(nameof(SelectedBank));
        }
    }
}
