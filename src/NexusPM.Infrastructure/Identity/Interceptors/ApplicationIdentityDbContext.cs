// <copyright file="ApplicationIdentityDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Identity.Interceptors;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusPM.Infrastructure.Identity.Configurations;
using System.Reflection.Emit;
using System.Reflection;

/// <summary>
/// Database context for ASP.NET Core Identity, managing users and roles for authentication and authorization.
/// </summary>
public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationIdentityDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the refresh tokens.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens => this.Set<RefreshToken>();

    /// <summary>
    /// Gets the user sessions.
    /// </summary>
    public DbSet<UserSession> UserSessions => this.Set<UserSession>();

    /// <summary>
    /// Configures the schema needed for the identity framework.
    /// </summary>
    /// <param name="builder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace?.Contains("Identity.Configurations") == true);
    }
}
