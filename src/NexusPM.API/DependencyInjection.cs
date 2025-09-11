// Copyright (c) YourCompany. All rights reserved.
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NexusPM.API.Authorization;
using NexusPM.Application.Abstractions.Security;
using NexusPM.Domain.Enums;
using NexusPM.Infrastructure.Boot;
using NexusPM.Infrastructure.Identity.Configurations;
using NexusPM.Infrastructure.Identity.Interceptors;
using NexusPM.Infrastructure.Identity.Services.KeyMaterial;

namespace NexusPM.API;

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

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<IAuthorizationHandler, TenantRoleHandler>();

        return services;
    }

    /// <summary>
    /// Adds JWT authentication services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the authentication services will be added.</param>
    /// <param name="configuration">The configuration containing JWT settings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<ApplicationUser>(o =>
                {
                    o.Password.RequiredLength = 8;
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = true;
                    o.Password.RequireUppercase = true;
                    o.Password.RequireNonAlphanumeric = true;
                    o.User.RequireUniqueEmail = true;
                    o.SignIn.RequireConfirmedEmail = true;
                    o.Lockout.MaxFailedAccessAttempts = 5;
                    o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                })
                    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                    .AddSignInManager<SignInManager<ApplicationUser>>()
                    .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                    NameClaimType = JwtRegisteredClaimNames.Sub,
                };

                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var keyProvider = context.HttpContext.RequestServices.GetRequiredService<IKeyMaterialProvider>();
                        var validationKeys = await keyProvider.GetValidationKeysAsync();
                        o.TokenValidationParameters.IssuerSigningKeys = validationKeys;
                    },
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("TenantOwner", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Owner)))
            .AddPolicy("TenantAdmin", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Admin)))
            .AddPolicy("TenantMember", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Member)))
            .AddPolicy("TenantBilling", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Billing)));

        return services;
    }
}
