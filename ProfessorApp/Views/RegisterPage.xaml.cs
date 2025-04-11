using ProfessorApp.Services;
using System.Diagnostics;

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

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string id = idEntry.Text ?? "";
            string firstName = firstNameEntry.Text ?? "";
            string lastName = lastNameEntry.Text ?? "";
            string username = usernameEntry.Text ?? "";
            string email = emailEntry.Text ?? "";
            string password = passwordEntry.Text ?? "";
            
            if (string.IsNullOrEmpty(id)
                || string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(lastName) 
                || string.IsNullOrEmpty(username) 
                || string.IsNullOrEmpty(email) 
                || string.IsNullOrEmpty(password))
            {
                statusLabel.Text = "Please enter all fields";
                return;
            }    

            try 
            { 
                var response = await _clientService.RegisterAsync(id, firstName, lastName, username, email, password);

                Debug.WriteLine("here");
                if (response == null)
                {

                    statusLabel.TextColor = Colors.Red;
                    statusLabel.Text = "Could not register";
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