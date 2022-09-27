using DataBaseClient.Gateways.Users;
using DataBaseClient.Gateways.Users.Repositories;
using DataBaseClient.ViewModels;

namespace DataBaseClient;

public static class Bootstraps
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<DataContext>();
        services.AddScoped<UsersViewModel>();

        return services;
    }
}
