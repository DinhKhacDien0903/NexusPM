namespace NexusPM.Domain.Entities;
public class Subscription : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid PlanId { get; set; }

    public Plan? Plan { get; set; }

    public int Seats { get; set; } = 1;

    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;

    public DateTime StartedOnUtc { get; set; } = DateTime.UtcNow;

    public DateTime CurrentPeriodStartUtc { get; set; } = DateTime.UtcNow;

    public DateTime CurrentPeriodEndUtc { get; set; } = DateTime.UtcNow.AddMonths(1);

    public DateTime? TrialEndsUtc { get; set; }
}
