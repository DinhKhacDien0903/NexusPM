using Microsoft.EntityFrameworkCore;
using NexusPM.Infrastructure.Identity;

namespace NexusPM.API.Hosting;

public static class HostMigrationExtention
{
    public static async Task ApplyMigrationAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        await db.Database.MigrateAsync();
    }
}
