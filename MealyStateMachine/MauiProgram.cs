using CommunityToolkit.Maui;
using MealyStateMachine.ViewModels;
using MealyStateMachine.Views;

namespace MealyStateMachine
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiCommunityToolkit()
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			builder.Services.AddScoped<GraphPage>();
			builder.Services.AddScoped<GraphViewModel>();

			return builder.Build();
		}
	}
}