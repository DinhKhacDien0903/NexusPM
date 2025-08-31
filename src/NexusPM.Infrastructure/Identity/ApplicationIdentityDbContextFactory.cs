namespace NexusPM.Infrastructure.Identity;

using DotNetEnv;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NexusPM.Infrastructure.Helpers;

public class ApplicationIdentityDbContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
{
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
