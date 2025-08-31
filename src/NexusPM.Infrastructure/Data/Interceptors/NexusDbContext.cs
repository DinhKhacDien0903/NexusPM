namespace NexusPM.Infrastructure.Data.Interceptors;
using System.Reflection;

public class NexusDbContext(DbContextOptions<NexusDbContext> options, ITenantProvider tenantProvider) : DbContext(options)
{
    private readonly ITenantProvider _tenantProvider = tenantProvider;

    public DbSet<Tenant> Tenants => this.Set<Tenant>();
    public DbSet<AppUser> Users => this.Set<AppUser>();
    public DbSet<UserTenant> UserTenants => this.Set<UserTenant>();
    public DbSet<Project> Projects => this.Set<Project>();
    public DbSet<ProjectMember> ProjectMembers => this.Set<ProjectMember>();
    public DbSet<Board> Boards => this.Set<Board>();
    public DbSet<BoardColumn> BoardColumns => this.Set<BoardColumn>();
    public DbSet<Sprint> Sprints => this.Set<Sprint>();
    public DbSet<Issue> Issues => this.Set<Issue>();
    public DbSet<Tag> Tags => this.Set<Tag>();
    public DbSet<IssueTag> IssueTags => this.Set<IssueTag>();
    public DbSet<Comment> Comments => this.Set<Comment>();
    public DbSet<Attachment> Attachments => this.Set<Attachment>();
    public DbSet<Worklog> Worklogs => this.Set<Worklog>();
    public DbSet<Plan> Plans => this.Set<Plan>();
    public DbSet<Subscription> Subscriptions => this.Set<Subscription>();
    public DbSet<Invoice> Invoices => this.Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => this.Set<InvoiceLine>();
    public DbSet<Payment> Payments => this.Set<Payment>();
    public DbSet<Notification> Notifications => this.Set<Notification>();
    public DbSet<AuditLog> AuditLogs => this.Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Tenant>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<AppUser>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Plan>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<UserTenant>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<Project>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<ProjectMember>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<Board>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<BoardColumn>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<Sprint>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<Issue>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<Tag>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<IssueTag>().HasQueryFilter(x => x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
        modelBuilder.Entity<Attachment>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == this._tenantProvider.TenantId);
    }
}
