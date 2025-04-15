using Microsoft.Maui.Controls;
using ProfessorApp.Services;
using ProfessorApp.Views;

namespace ProfessorApp
{
    public partial class App : Application
    {
        public App(ClientService clientService)
        {
            InitializeComponent();
            
            // Set LoginPage as the initial page
            MainPage = new NavigationPage(new LoginPage(clientService));
        }
    }
}
