namespace NexusPM.Domain.Entities;
public class Tag : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public string Name { get; set; } = default!;

    public string? ColorHex { get; set; }

    public ICollection<IssueTag> Issues { get; set; } = [];
}
