using ProfessorApp.Services;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ProfessorApp.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly ClientService _clientService;

        public RegisterPage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
        }

        // fires when ID field changes, forces non-digits to be removed
        public void IDTextChanged(object sender, TextChangedEventArgs e)
        {
            string regex = e.NewTextValue;
            if (String.IsNullOrEmpty(regex))
                return;
            if (!Regex.Match(regex, "^[0-9]+$").Success)
            {
                var entry = sender as Entry;
                if (entry != null)
                {
                    entry.Text = (string.IsNullOrEmpty(e.OldTextValue)) ?
                    string.Empty : e.OldTextValue;
                }
            }
        }

        // fires when register button is clicked
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // get fields
            string id = idEntry.Text ?? "";
            string firstName = firstNameEntry.Text ?? "";
            string lastName = lastNameEntry.Text ?? "";
            string username = usernameEntry.Text ?? "";
            string email = emailEntry.Text ?? "";
            string password = passwordEntry.Text ?? "";
            
            // check if any empty fields
            if (string.IsNullOrEmpty(id)
                || string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(lastName) 
                || string.IsNullOrEmpty(username) 
                || string.IsNullOrEmpty(email) 
                || string.IsNullOrEmpty(password))
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Please enter all fields.";
                return;
            }

            // validate ID (10 digits)
            if (id.Length != 10 || !id.All(char.IsDigit))
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "ID must be a sequence of 10 digits.";
                return;
            }

            // validate email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email,emailPattern))
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = "Email is invalid.";
                return;
            }

            try
            { 
                var response = await _clientService.RegisterAsync(id, firstName, lastName, username, email, password);

                if (response == null)
                {
                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = "ID, Email, or Username already in use.";
                    return;
                }
                statusLabel.TextColor = Colors.Green;
                statusLabel.Text = "Registered Account!";
                // Close page
                // await Navigation.PopAsync();
            } 
            catch (Exception ex)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = $"{ex.Message}";
            }
        }
        private async void GoToLogin(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}