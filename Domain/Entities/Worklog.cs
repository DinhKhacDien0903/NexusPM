namespace NexusPM.Domain.Entities;
public class Worklog : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid IssueId { get; set; }

    public Issue? Issue { get; set; }

    public Guid UserId { get; set; }

    public AppUser? User { get; set; }

    public DateTime StartedAtUtc { get; set; }

    public int Minutes { get; set; }

    public bool IsBillable { get; set; } = true;

    public Money? HourlyRate { get; set; }

    public Guid? InvoiceLineId { get; set; }

    public InvoiceLine? InvoiceLine { get; set; }

    [MaxLength(240)]
    public string? Note { get; set; }
}
