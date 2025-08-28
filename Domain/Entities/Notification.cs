namespace NexusPM.Domain.Entities;
public class Notification
    : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid RecipientUserId { get; set; }

    public AppUser? Recipient { get; set; }

    public string Type { get; set; } = default!;

    public string PayloadJson { get; set; } = "{}";

    public bool IsRead { get; set; }

    public DateTime? ReadAtUtc { get; set; }
}
