using Microsoft.EntityFrameworkCore;

namespace NexusPM.Application.Common.Interfaces
{
    /// <summary>
    /// Represents the contract for the Nexus database context, providing access to all entity sets.
    /// </summary>
    public interface INexusDbContext
    {
        /// <summary>
        /// Gets the DbSet for the Tenant entity, which represents tenants in the system.
        /// </summary>
        DbSet<Tenant> Tenants { get; }

        /// <summary>
        /// Gets the DbSet for the AppUser entity, which represents users in the system.
        /// </summary>
        DbSet<AppUser> Users { get; }

        /// <summary>
        /// Gets the DbSet for the UserTenant entity, which represents the relationship between users and tenants.
        /// </summary>
        DbSet<UserTenant> UserTenants { get; }

        /// <summary>
        /// Gets the DbSet for the Project entity, which represents projects in the system.
        /// </summary>
        DbSet<Project> Projects { get; }

        /// <summary>
        /// Gets the DbSet for the ProjectMember entity, which represents the relationship between users and projects.
        /// </summary>
        DbSet<ProjectMember> ProjectMembers { get; }

        /// <summary>
        /// Gets the DbSet for the Board entity, which represents boards in the system.
        /// </summary>
        DbSet<Board> Boards { get; }

        /// <summary>
        /// Gets the DbSet for the BoardColumn entity, which represents columns on boards.
        /// </summary>
        DbSet<BoardColumn> BoardColumns { get; }

        /// <summary>
        /// Gets the DbSet for the Sprint entity, which represents sprints in the system.
        /// </summary>
        DbSet<Sprint> Sprints { get; }

        /// <summary>
        /// Gets the DbSet for the Issue entity, which represents issues in the system.
        /// </summary>
        DbSet<Issue> Issues { get; }

        /// <summary>
        /// Gets the DbSet for the Tag entity, which represents tags that can be applied to issues.
        /// </summary>
        DbSet<Tag> Tags { get; }

        /// <summary>
        /// Gets the DbSet for the IssueTag entity, which represents the relationship between issues and tags.
        /// </summary>
        DbSet<IssueTag> IssueTags { get; }

        /// <summary>
        /// Gets the DbSet for the Comment entity, which represents comments on issues.
        /// </summary>
        DbSet<Comment> Comments { get; }

        /// <summary>
        /// Gets the DbSet for the Attachment entity, which represents attachments linked to issues.
        /// </summary>
        DbSet<Attachment> Attachments { get; }

        /// <summary>
        /// Gets the DbSet for the Worklog entity, which represents work log entries for issues.
        /// </summary>
        DbSet<Worklog> Worklogs { get; }

        /// <summary>
        /// Gets the DbSet for the Plan entity, which represents subscription plans.
        /// </summary>
        DbSet<Plan> Plans { get; }

        /// <summary>
        /// Gets the DbSet for the Subscription entity, which represents subscriptions to plans.
        /// </summary>
        DbSet<Subscription> Subscriptions { get; }

        /// <summary>
        /// Gets the DbSet for the Invoice entity, which represents invoices in the system.
        /// </summary>
        DbSet<Invoice> Invoices { get; }

        /// <summary>
        /// Gets the DbSet for the InvoiceLine entity, which represents line items in invoices.
        /// </summary>
        DbSet<InvoiceLine> InvoiceLines { get; }

        /// <summary>
        /// Gets the DbSet for the Payment entity, which represents payments made against invoices.
        /// </summary>
        DbSet<Payment> Payments { get; }

        /// <summary>
        /// Gets the DbSet for the Notification entity, which represents notifications in the system.
        /// </summary>
        DbSet<Notification> Notifications { get; }

        /// <summary>
        /// Gets the DbSet for the AuditLog entity, which represents audit logs for tracking changes.
        /// </summary>
        DbSet<AuditLog> AuditLogs { get; }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
