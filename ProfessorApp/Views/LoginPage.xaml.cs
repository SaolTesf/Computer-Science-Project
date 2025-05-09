/*
 * Sawyer Kamman, Saol Tesfaghebriel
 * Accesses the database to let the user enter the app if they input valid login information
 */

using ProfessorApp.Pages;
using ProfessorApp.Services;

namespace ProfessorApp.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly ClientService _clientService;

        public LoginPage(ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            statusLabel.Text = null;
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string identifier = identifierEntry.Text ?? "";
            string password = passwordEntry.Text ?? "";
            
            if (string.IsNullOrEmpty(identifier) || string.IsNullOrEmpty(password))
            {
                statusLabel.Text = "Please enter both identifier and password";
                statusLabel.TextColor = Colors.Red;
                statusLabel.IsVisible = true;
                return;
            }     
            try 
            {
                var response = await _clientService.LoginAsync(identifier, password);
                if (response != null)
                {
                    statusLabel.TextColor = Colors.Green;
                    statusLabel.Text = "Login successful!";
                    identifierEntry.Text = string.Empty;
                    passwordEntry.Text = string.Empty;
                    await Navigation.PushAsync(new HomePage(_clientService));
                }
                else
                {
                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = "Invalid credentials";
                }
                statusLabel.IsVisible = true;
            }
            catch (Exception ex)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = $"{ex.Message}";
                statusLabel.IsVisible = true;
            }
        }
        private async void GoToRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(_clientService));
        }
    }
}