// <copyright file="NexusDbContextFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Interceptors;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NexusPM.Infrastructure.Identity;

/// <summary>
/// A factory for creating instances of <see cref="NexusDbContext"/> at design time.
/// </summary>
internal class NexusDbContextFactory : IDesignTimeDbContextFactory<NexusDbContext>
{
    /// <summary>
    /// Creates a new instance of <see cref="NexusDbContext"/>.
    /// </summary>
    /// <param name="args">The arguments passed to the factory.</param>
    /// <returns>A new instance of <see cref="NexusDbContext"/>.</returns>
    public NexusDbContext CreateDbContext(string[] args)
    {
        var cfg = new ConfigurationBuilder()
            .AddUserSecrets<ApplicationIdentityDbContextFactory>(optional: true)
            .AddEnvironmentVariables()
            .Build();

        var cs = cfg.GetConnectionString("Default")
                 ?? cfg.GetConnectionString("Identity")
                 ?? throw new InvalidOperationException("Missing ConnectionStrings:Identity (or Default).");

        var opts = new DbContextOptionsBuilder<NexusDbContext>();
        opts.UseNpgsql(cs);

        return new NexusDbContext(opts.Options);
    }
}
