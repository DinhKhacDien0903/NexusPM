// <copyright file="DependencyInjection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusPM.Application.Abstractions;
using NexusPM.Infrastructure.Data.Interceptors;
using NexusPM.Infrastructure.Data.Tenancy;
using NexusPM.Infrastructure.Identity.Configurations;
using NexusPM.Infrastructure.Identity.Interceptors;

/// <summary>
/// Provides extension methods for configuring infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the services will be added.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated service collection.</returns>
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

        services.AddScoped<RequestTenantContext>();

        services.AddScoped<ICurrentTenant>(
            sp => sp.GetRequiredService<RequestTenantContext>());

        services.AddScoped<ITenantProvider>(
            sp => sp.GetRequiredService<RequestTenantContext>());

        services.AddScoped<ITenantStore, EfTenantStore>();

        return services;
    }
}
