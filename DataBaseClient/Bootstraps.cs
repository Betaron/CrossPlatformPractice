using DataBaseClient.Gateways.Users;
using DataBaseClient.Gateways.Users.Repositories;

namespace DataBaseClient;

internal static class Bootstraps
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<DataContext>();

        return services;
    }
}
