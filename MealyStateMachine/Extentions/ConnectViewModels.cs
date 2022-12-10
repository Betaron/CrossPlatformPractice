using MealyStateMachine.ViewModels;
using MealyStateMachine.Views;

namespace MealyStateMachine.Extentions;

public static class ConnectViewModels
{
	public static IServiceCollection AddViewModels(this IServiceCollection services)
	{
		services.AddScoped<JsonViewModel>();
		services.AddScoped<GraphViewModel>();

		return services;
	}

	public static IServiceCollection AddViews(this IServiceCollection services)
	{
		services.AddScoped<JsonPage>();
		services.AddScoped<GraphPage>();

		return services;
	}
}
