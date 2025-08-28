namespace NexusPM.Domain.Entities;
public class Invoice : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public string Number { get; set; } = default!;

    public DateTime IssuedOnUtc { get; set; } = DateTime.UtcNow;

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

    public Money Subtotal { get; set; } = new Money(0, "USD");

    public Money Tax { get; set; } = new Money(0, "USD");

    public Money Total { get; set; } = new Money(0, "USD");

    public ICollection<InvoiceLine> Lines { get; set; } = [];

    public ICollection<Payment> Payments { get; set; } = [];
}
