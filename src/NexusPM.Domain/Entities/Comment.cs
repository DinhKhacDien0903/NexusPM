namespace NexusPM.Domain.Entities;
public class Comment : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid IssueId { get; set; }

    public Issue? Issue { get; set; }

    public Guid AuthorId { get; set; }

    public AppUser? Author { get; set; }

    public string Body { get; set; } = default!;
}
