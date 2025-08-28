namespace NexusPM.Domain.Entities;
public class Tag : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = default!;

    [MaxLength(7)]
    public string? ColorHex { get; set; }

    public ICollection<IssueTag> Issues { get; set; } = [];
}
