namespace NexusPM.Domain.Entities;
public class Plan : AuditableEntity
{
    [MaxLength(60)]
    public string Code { get; set; } = default!;

    [MaxLength(160)]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public Money PricePerSeatMonthly { get; set; } = new Money(0, "USD");

    public Money? PricePerSeatYearly { get; set; }

    public int? MaxSeats { get; set; }

    public int? MaxProjects { get; set; }

    public int? MaxActiveIssues { get; set; }
}
