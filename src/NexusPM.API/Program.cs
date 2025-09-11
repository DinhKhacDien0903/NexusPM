// Copyright (c) YourCompany. All rights reserved.

using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NexusPM.API.Authorization;
using NexusPM.API.Middlewares;
using NexusPM.Domain.Enums;
using NexusPM.Infrastructure;
using NexusPM.Infrastructure.Identity.Configurations;
using NexusPM.Infrastructure.Identity.Interceptors;
using NexusPM.Infrastructure.Identity.Services.KeyMaterial;

namespace NexusPM.API;

/// <summary>
/// The entry point of the NexusPM.API application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main method that serves as the entry point for the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi();

        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.AddAPIService();

        builder.Services.AddIdentityCore<ApplicationUser>(o =>
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

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
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

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("TenantOwner", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Owner)))
            .AddPolicy("TenantAdmin", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Admin)))
            .AddPolicy("TenantMember", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Member)))
            .AddPolicy("TenantBilling", p => p.Requirements.Add(new TenantRoleRequirement(TenantRole.Billing)));

        builder.Services.AddHttpContextAccessor();

        Env.Load();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<MissingTenantMiddleware>();

        app.MapControllers();

        app.MapGet("/health", () => Results.Ok("OK"));

        await app.RunAsync();
    }
}
