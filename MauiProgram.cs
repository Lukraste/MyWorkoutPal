using MyWorkoutPal.Services;
using MyWorkoutPal.Views;

namespace MyWorkoutPal;

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

		builder.Services.AddHttpClient<IDataService, DataService>();
        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<MyExercicesPage>();
		
		return builder.Build();
	}
}
