namespace NexusPM.Domain.Entities;
public class Tenant : AuditableEntity
{
    [MaxLength(160)]
    public string Name { get; set; } = default!;

    [MaxLength(80)]
    public string Slug { get; set; } = default!;

    [MaxLength(200)]
    public string? Domain { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = [];

    public ICollection<UserTenant> UserTenants { get; set; } = [];
}
