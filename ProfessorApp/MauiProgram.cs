using Microsoft.Extensions.Logging;
using ProfessorApp.Services;
using ProfessorApp.Views;

namespace ProfessorApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Register services for dependency injection
		builder.Services.AddHttpClient<ClientService>(client =>
		{
			client.BaseAddress = new Uri("http://localhost:5225/");
		});

		// Register pages for dependency injection
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddSingleton<App>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
