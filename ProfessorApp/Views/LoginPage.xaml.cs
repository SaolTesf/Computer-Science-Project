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

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text ?? "";
            string password = passwordEntry.Text ?? "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                statusLabel.Text = "Please enter both your username and password.";
                return;
            }
            try
            {
                var response = await _clientService.LoginAsync(username, password);
                if (response != null)
                {
                    statusLabel.TextColor = Colors.Green;
                    statusLabel.Text = "Login successful!";
                    // Navigate to main page or dashboard
                }
                else
                {
                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = "Invalid credentials";
                }
            }
            catch (Exception ex)
            {
                statusLabel.TextColor = Colors.Red;
                statusLabel.Text = $"{ex.Message}";
            }
        }
        private async void GoToRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(_clientService));
        }
    }
}