// <copyright file="ApplicationIdentityDbContextFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Identity;

using DotNetEnv;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NexusPM.Infrastructure.Helpers;

/// <summary>
/// Factory for creating ApplicationIdentityDbContext instances at design time, used by EF Core tools for migrations.
/// </summary>
public class ApplicationIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
{
    /// <summary>
    /// Creates a new instance of ApplicationIdentityDbContext using configuration from environment and user secrets.
    /// </summary>
    /// <param name="args">Command line arguments passed to the factory.</param>
    /// <returns>A configured ApplicationIdentityDbContext instance.</returns>
    public ApplicationIdentityDbContext CreateDbContext(string[] args)
    {
        var cfg = new ConfigurationBuilder()
            .AddUserSecrets<ApplicationIdentityDbContextFactory>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var cs = cfg.GetConnectionString("Identity")
                 ?? cfg.GetConnectionString("Default")
                 ?? throw new InvalidOperationException("Missing ConnectionStrings:Identity (or Default).");

        var opts = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
        opts.UseNpgsql(cs);

        return new ApplicationIdentityDbContext(opts.Options);
    }
}
