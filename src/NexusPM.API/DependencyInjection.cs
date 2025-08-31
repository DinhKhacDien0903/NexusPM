namespace NexusPM.API;
using NexusPM.Infrastructure.Boot;

public static class DependencyInjection
{
    public static IServiceCollection AddAPIService(this IServiceCollection services)
    {
        services.AddHostedService<MigrationHostedService>();

        return services;
    }
}
