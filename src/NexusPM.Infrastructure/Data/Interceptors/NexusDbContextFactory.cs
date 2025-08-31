namespace NexusPM.Infrastructure.Data.Interceptors;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NexusPM.Infrastructure.Identity;

internal class NexusDbContextFactory : IDesignTimeDbContextFactory<NexusDbContext>
{
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
