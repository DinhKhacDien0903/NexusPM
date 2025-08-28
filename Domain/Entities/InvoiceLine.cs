namespace NexusPM.Domain.Entities;
public class InvoiceLine : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid InvoiceId { get; set; }

    public Invoice? Invoice { get; set; }

    public string Description { get; set; } = default!;

    public int QuantityMinutes { get; set; }

    public Money UnitPricePerHour { get; set; } = new Money(0, "USD");

    public Money LineTotal { get; set; } = new Money(0, "USD");

    public Guid? IssueId { get; set; }

    public Issue? Issue { get; set; }
}
