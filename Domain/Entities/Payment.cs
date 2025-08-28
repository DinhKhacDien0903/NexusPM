namespace NexusPM.Domain.Entities;
public class Payment : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid InvoiceId { get; set; }

    public Invoice? Invoice { get; set; }

    public Money Amount { get; set; } = new Money(0, "USD");

    [MaxLength(40)]
    public string Provider { get; set; } = "manual";

    [MaxLength(120)]
    public string ProviderRef { get; set; } = default!;

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public DateTime? PaidOnUtc { get; set; }
}
