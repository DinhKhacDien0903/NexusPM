namespace NexusPM.Domain.Entities;
public class AuditLog : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    [MaxLength(60)]
    public string ResourceType { get; set; } = default!;

    public Guid ResourceId { get; set; }

    [MaxLength(40)]
    public string Action { get; set; } = default!;

    public string? OldValuesJson { get; set; }

    public string? NewValuesJson { get; set; }
}
