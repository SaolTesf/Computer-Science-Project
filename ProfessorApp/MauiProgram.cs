using Microsoft.Extensions.Logging;
using ProfessorApp.Pages;
using ProfessorApp.Services;
using ProfessorApp.Views;
using CommunityToolkit.Maui;

namespace ProfessorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("ProjectFonts.ttf", "ProjectFonts");
            });

        // Register services for dependency injection
        builder.Services.AddHttpClient<ClientService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5225/");
        });

        // Register pages for dependency injection
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<CoursePage>();
        builder.Services.AddTransient<StudentManagement>();
        builder.Services.AddTransient<AttendancePage>();
        builder.Services.AddTransient<QuizPage>();

        builder.Services.AddSingleton<App>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

}