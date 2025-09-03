// <copyright file="NexusDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Interceptors;

using System.Reflection;

/// <summary>
/// The main database context for the NexusPM application, providing access to all entities with tenant-aware functionality.
/// </summary>
/// <param name="options">The database context options.</param>
/// <param name="tenantProvider">The tenant provider for multi-tenant support.</param>
public class NexusDbContext(DbContextOptions<NexusDbContext> options, ITenantProvider tenantProvider)
    : DbContext(options)
{
    private readonly ITenantProvider tenantProvider = tenantProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="NexusDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public NexusDbContext(DbContextOptions<NexusDbContext> options)
    : this(options, DesignTimeTenantProvider.Instance)
    {
    }

    /// <summary>
    /// Gets the DbSet for the Tenant entity, which represents tenants in the system.
    /// </summary>
    public DbSet<Tenant> Tenants => this.Set<Tenant>();

    /// <summary>
    /// Gets the DbSet for the AppUser entity, which represents users in the system.
    /// </summary>
    public DbSet<AppUser> Users => this.Set<AppUser>();

    /// <summary>
    /// Gets the DbSet for the UserTenant entity, which represents the relationship between users and tenants.
    /// </summary>
    public DbSet<UserTenant> UserTenants => this.Set<UserTenant>();

    /// <summary>
    /// Gets the DbSet for the Project entity, which represents projects in the system.
    /// </summary>
    public DbSet<Project> Projects => this.Set<Project>();

    /// <summary>
    /// Gets the DbSet for the ProjectMember entity, which represents the relationship between users and projects.
    /// </summary>
    public DbSet<ProjectMember> ProjectMembers => this.Set<ProjectMember>();

    /// <summary>
    /// Gets the DbSet for the Board entity, which represents boards in the system.
    /// </summary>
    public DbSet<Board> Boards => this.Set<Board>();

    /// <summary>
    /// Gets the DbSet for the BoardColumn entity, which represents columns on boards.
    /// </summary>
    public DbSet<BoardColumn> BoardColumns => this.Set<BoardColumn>();

    /// <summary>
    /// Gets the DbSet for the Sprint entity, which represents sprints in the system.
    /// </summary>
    public DbSet<Sprint> Sprints => this.Set<Sprint>();

    /// <summary>
    /// Gets the DbSet for the Issue entity, which represents issues in the system.
    /// </summary>
    public DbSet<Issue> Issues => this.Set<Issue>();

    /// <summary>
    /// Gets the DbSet for the Tag entity, which represents tags that can be applied to issues.
    /// </summary>
    public DbSet<Tag> Tags => this.Set<Tag>();

    /// <summary>
    /// Gets the DbSet for the IssueTag entity, which represents the relationship between issues and tags.
    /// </summary>
    public DbSet<IssueTag> IssueTags => this.Set<IssueTag>();

    /// <summary>
    /// Gets the DbSet for the Comment entity, which represents comments on issues.
    /// </summary>
    public DbSet<Comment> Comments => this.Set<Comment>();

    /// <summary>
    /// Gets the DbSet for the Attachment entity, which represents attachments linked to issues.
    /// </summary>
    public DbSet<Attachment> Attachments => this.Set<Attachment>();

    /// <summary>
    /// Gets the DbSet for the Worklog entity, which represents work log entries for issues.
    /// </summary>
    public DbSet<Worklog> Worklogs => this.Set<Worklog>();

    /// <summary>
    /// Gets the DbSet for the Plan entity, which represents subscription plans.
    /// </summary>
    public DbSet<Plan> Plans => this.Set<Plan>();

    /// <summary>
    /// Gets the DbSet for the Subscription entity, which represents subscriptions to plans.
    /// </summary>
    public DbSet<Subscription> Subscriptions => this.Set<Subscription>();

    /// <summary>
    /// Gets the DbSet for the Invoice entity, which represents invoices in the system.
    /// </summary>
    public DbSet<Invoice> Invoices => this.Set<Invoice>();

    /// <summary>
    /// Gets the DbSet for the InvoiceLine entity, which represents line items in invoices.
    /// </summary>
    public DbSet<InvoiceLine> InvoiceLines => this.Set<InvoiceLine>();

    /// <summary>
    /// Gets the DbSet for the Payment entity, which represents payments made against invoices.
    /// </summary>
    public DbSet<Payment> Payments => this.Set<Payment>();

    /// <summary>
    /// Gets the DbSet for the Notification entity, which represents notifications in the system.
    /// </summary>
    public DbSet<Notification> Notifications => this.Set<Notification>();

    /// <summary>
    /// Gets the DbSet for the AuditLog entity, which represents audit logs for tracking changes.
    /// </summary>
    public DbSet<AuditLog> AuditLogs => this.Set<AuditLog>();

    /// <summary>
    /// Gets the refresh tokens.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens => this.Set<RefreshToken>();

    /// <summary>
    /// Gets the user sessions.
    /// </summary>
    public DbSet<UserSession> UserSessions => this.Set<UserSession>();

    /// <summary>
    /// Configures the model for the context by applying entity configurations and query filters.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Tenant>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<AppUser>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Plan>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<UserTenant>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<Project>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<ProjectMember>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<Board>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<BoardColumn>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<Sprint>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<Issue>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<Tag>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<IssueTag>().HasQueryFilter(x => x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
        modelBuilder.Entity<Attachment>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this.tenantProvider.TenantId);
    }
}
