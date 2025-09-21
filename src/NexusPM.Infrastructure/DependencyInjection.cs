// <copyright file="DependencyInjection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NexusPM.Application.Abstractions;
using NexusPM.Application.Abstractions.Notifications;
using NexusPM.Application.Abstractions.Security;
using NexusPM.Application.Common.Interfaces;
using NexusPM.Infrastructure.Data.Auth;
using NexusPM.Infrastructure.Data.Interceptors;
using NexusPM.Infrastructure.Data.Tenancy;
using NexusPM.Infrastructure.Identity.Configurations;
using NexusPM.Infrastructure.Identity.Interceptors;
using NexusPM.Infrastructure.Identity.Services;
using NexusPM.Infrastructure.Identity.Services.KeyMaterial;
using NexusPM.Infrastructure.Notifications.Email;

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
        var defaultCs = configuration.GetConnectionString("Default")
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__Default");

        var identityCs = configuration.GetConnectionString("Identity")
                       ?? Environment.GetEnvironmentVariable("ConnectionStrings__Identity");

        services.AddDbContext<ApplicationIdentityDbContext>(
            option => option.UseNpgsql(identityCs));

        services.AddDbContext<NexusDbContext>(
            option => option.UseNpgsql(defaultCs));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

        services.AddScoped<INexusDbContext, NexusDbContext>();

        services.AddScoped<IApplicationIdentityDbContext, ApplicationIdentityDbContext>();

        services.AddScoped<RequestTenantContext>();

        services.AddScoped<ICurrentTenant>(
            sp => sp.GetRequiredService<RequestTenantContext>());

        services.AddScoped<ITenantProvider>(
            sp => sp.GetRequiredService<RequestTenantContext>());

        services.AddScoped<ITenantStore, EfTenantStore>();

        services.AddScoped<IUserTenantReader, EfUserTenantReader>();

        services.AddSingleton<IKeyMaterialProvider, InMemoryRsaKeyMaterialProvider>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IEmailSender, SmtpEmailOTPSender>();

        services.AddOptions<JwtOptions>()
            .Configure<IConfiguration>((opt, cfg) =>
            {
                opt.PrivateKeyPem = cfg["PrivateKeyPem"];
            })
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.PrivateKeyPem),
                "Provide PrivateKeyPem")
            .ValidateOnStart();

        services.AddOptions<SmtpOptions>()
            .Configure<IConfiguration>((opt, cfg) =>
            {
                cfg.GetSection("Smtp").Bind(opt);
            })
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.Host),
                "SMTP Host is required")
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.User),
                "SMTP User is required")
            .Validate(
                o => !string.IsNullOrWhiteSpace(o.Password),
                "SMTP Password is required")
            .ValidateOnStart();

        return services;
    }
}
