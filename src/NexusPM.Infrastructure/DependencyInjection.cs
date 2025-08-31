namespace NexusPM.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusPM.Infrastructure.Data.Interceptors;
using NexusPM.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cs = configuration.GetConnectionString("Default")
                    ?? Environment.GetEnvironmentVariable("ConnectionStrings__Default");

        services.AddDbContext<ApplicationIdentityDbContext>(
            option => option.UseNpgsql(cs));

        services.AddDbContext<NexusDbContext>(
            option => option.UseNpgsql(cs));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

        return services;
    }
}
