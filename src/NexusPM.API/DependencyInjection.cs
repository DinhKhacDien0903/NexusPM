// Copyright (c) YourCompany. All rights reserved.
namespace NexusPM.API;

using NexusPM.Infrastructure.Boot;

/// <summary>
/// Provides extension methods for configuring API services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds API services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the services will be added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddAPIService(this IServiceCollection services)
    {
        services.AddHostedService<MigrationHostedService>();

        return services;
    }
}
