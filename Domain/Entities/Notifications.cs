namespace NexusPM.Domain.Entities;
public class Notifications : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid RecipientUserId { get; set; }

    public AppUser? Recipient { get; set; }

    [MaxLength(80)]
    public string Type { get; set; } = default!;

    [MaxLength(8000)]
    public string PayloadJson { get; set; } = "{}";

    public bool IsRead { get; set; }

    public DateTime? ReadAtUtc { get; set; }
}
