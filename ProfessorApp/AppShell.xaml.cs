using ProfessorApp.Pages;
namespace ProfessorApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(StudentManagement), typeof(StudentManagement));
    }
}
