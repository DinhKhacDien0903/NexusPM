// Copyright (c) YourCompany. All rights reserved.

using Microsoft.EntityFrameworkCore;
using NexusPM.Infrastructure.Identity.Interceptors;

namespace NexusPM.API.Hosting;

/// <summary>
/// Provides extension methods for applying database migrations to the host.
/// </summary>
public static class HostMigrationExtention
{
    /// <summary>
    /// Applies pending migrations to the database associated with the application's host.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task ApplyMigrationAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        await db.Database.MigrateAsync();
    }
}
