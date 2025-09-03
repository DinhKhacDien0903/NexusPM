// <copyright file="MigrationHostedService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Boot;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NexusPM.Domain.Enums;
using NexusPM.Domain.ValueObjects;
using NexusPM.Infrastructure.Data.Interceptors;
using NexusPM.Infrastructure.Helpers;
using NexusPM.Infrastructure.Identity.Configurations;
using NexusPM.Infrastructure.Identity.Interceptors;

/// <summary>
/// Hosted service responsible for running database migrations and seeding initial data on application startup.
/// </summary>
public class MigrationHostedService : IHostedService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<MigrationHostedService> _logger;

    public MigrationHostedService(IServiceProvider services, ILogger<MigrationHostedService> logger)
    {
        _services = services;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this._logger.LogInformation("==> Running migrations & seeding...");

        using var scope = _services.CreateScope();

        // 1) Migrate Identity
        var identityDb = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
        await identityDb.Database.MigrateAsync(cancellationToken);

        // 2) Migrate Domain (không phụ thuộc tenant khi migrate)
        var domainOptions = scope.ServiceProvider.GetRequiredService<DbContextOptions<NexusDbContext>>();
        await using (var dbNoTenant = new NexusDbContext(domainOptions, new StaticTenantProvider(Guid.Empty)))
        {
            await dbNoTenant.Database.MigrateAsync(cancellationToken);

            // 3) Seed Plans (global) & Tenant nếu chưa có
            await SeedPlansAsync(dbNoTenant, cancellationToken);

            if (!await dbNoTenant.Tenants.AnyAsync(cancellationToken))
            {
                _logger.LogInformation("==> No tenants found. Seeding default tenant & admin...");
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var adminEmail = Environment.GetEnvironmentVariable("SEED_ADMIN_EMAIL") ?? "admin@nexus.local";
                var adminPass = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD") ?? "Admin@123456!";
                var adminUser = await EnsureIdentityUserAsync(userMgr, adminEmail, adminPass, cancellationToken);

                // 3.2 Tạo Tenant + Domain AppUser + liên kết UserTenant + Project mẫu
                var seeded = await SeedTenantAsync(
                    domainOptions,
                    tenantName: "Nexus Demo",
                    slug: "demo",
                    ownerIdentityUserId: adminUser.Id,
                    ownerDisplayName: adminUser.UserName ?? adminUser.Email ?? "Admin",
                    ct: cancellationToken);

                _logger.LogInformation("==> Seeded tenant {Tenant} ({TenantId})", seeded.TenantName, seeded.TenantId);
            }
        }

        _logger.LogInformation("==> Migrations & seeding completed.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static async Task<ApplicationUser> EnsureIdentityUserAsync(
        UserManager<ApplicationUser> userMgr, string email, string password, CancellationToken ct)
    {
        var user = await userMgr.FindByEmailAsync(email);
        if (user != null)
        {
            return user;
        }

        user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = email,
            EmailConfirmed = true,
        };
        var result = await userMgr.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Cannot create seed identity user: " +
                string.Join("; ", result.Errors.Select(e => $"{e.Code}:{e.Description}")));
        }

        return user;
    }

    private static async Task SeedPlansAsync(NexusDbContext db, CancellationToken ct)
    {
        if (!await db.Plans.AnyAsync(ct))
        {
            db.Plans.AddRange(
                new Plan { Code = "STARTER", Name = "Starter", PricePerSeatMonthly = new Money(5, "USD"), MaxSeats = 5, MaxProjects = 5, MaxActiveIssues = 100 },
                new Plan { Code = "PRO", Name = "Pro", PricePerSeatMonthly = new Money(12, "USD"), MaxSeats = 100, MaxProjects = 100, MaxActiveIssues = 10000 },
                new Plan { Code = "ENT", Name = "Enterprise", PricePerSeatMonthly = new Money(0, "USD") } // Custom pricing
            );

            await db.SaveChangesAsync(ct);
        }
    }

    private static async Task<(Guid TenantId, string TenantName)> SeedTenantAsync(
        DbContextOptions<NexusDbContext> domainOptions,
        string tenantName,
        string slug,
        Guid ownerIdentityUserId,
        string ownerDisplayName,
        CancellationToken ct)
    {
        // 1) Tạo Tenant + Plan + Subscription sơ bộ (không cần TenantProvider)
        Guid tenantId;

        await using (var db = new NexusDbContext(domainOptions, new StaticTenantProvider(Guid.Empty)))
        {
            var plan = await db.Plans.OrderBy(p => p.PricePerSeatMonthly.Amount).FirstAsync(ct);

            var tenant = new Tenant { Name = tenantName, Slug = slug };
            db.Tenants.Add(tenant);

            db.Subscriptions.Add(new Subscription
            {
                TenantId = tenant.Id,
                PlanId = plan.Id,
                Seats = 5,
                Status = SubscriptionStatus.Active,
                StartedOnUtc = DateTime.UtcNow,
                CurrentPeriodStartUtc = DateTime.UtcNow,
                CurrentPeriodEndUtc = DateTime.UtcNow.AddMonths(1),
            });

            await db.SaveChangesAsync(ct);
            tenantId = tenant.Id;
        }

        // 2) Dùng TenantProvider = tenant vừa tạo để seed dữ liệu tenant-scoped
        await using (var db = new NexusDbContext(domainOptions, new StaticTenantProvider(tenantId)))
        {
            // Domain AppUser có cùng GUID với Identity user (1-1)
            if (!await db.Users.AnyAsync(u => u.Id == ownerIdentityUserId, ct))
            {
                db.Users.Add(new AppUser
                {
                    Id = ownerIdentityUserId,
                    Email = ownerDisplayName,
                    DisplayName = ownerDisplayName,
                    IsActive = true,
                });
            }

            // Membership ở mức tenant
            db.UserTenants.Add(new UserTenant
            {
                TenantId = tenantId,
                UserId = ownerIdentityUserId,
                Role = TenantRole.Owner,
                IsSuspended = false,
            });

            // Project + Board + Columns mẫu
            var project = new Project { TenantId = tenantId, Key = "NEX", Name = "Nexus Core", Type = ProjectType.Scrum };
            db.Projects.Add(project);
            await db.SaveChangesAsync(ct);

            var board = new Board { TenantId = tenantId, ProjectId = project.Id, Name = "Default Board" };
            db.Boards.Add(board);
            await db.SaveChangesAsync(ct);

            db.BoardColumns.AddRange(
                new BoardColumn { TenantId = tenantId, BoardId = board.Id, Status = IssueStatus.Backlog, Order = 0 },
                new BoardColumn { TenantId = tenantId, BoardId = board.Id, Status = IssueStatus.Todo, Order = 1 },
                new BoardColumn { TenantId = tenantId, BoardId = board.Id, Status = IssueStatus.InProgress, Order = 2 },
                new BoardColumn { TenantId = tenantId, BoardId = board.Id, Status = IssueStatus.InReview, Order = 3 },
                new BoardColumn { TenantId = tenantId, BoardId = board.Id, Status = IssueStatus.Done, Order = 4 }
            );

            // Issue demo
            db.Issues.Add(new Issue
            {
                TenantId = tenantId,
                ProjectId = project.Id,
                Key = "NEX-1",
                Number = 1,
                Title = "Welcome to NexusPM!",
                Type = IssueType.Task,
                Priority = IssuePriority.Medium,
                Status = IssueStatus.Todo,
                ReporterId = ownerIdentityUserId,
                AssigneeId = ownerIdentityUserId,
            });

            await db.SaveChangesAsync(ct);
        }

        return (tenantId, tenantName);
    }
}
