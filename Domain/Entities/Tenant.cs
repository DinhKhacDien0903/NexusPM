namespace NexusPM.Domain.Entities;
public class Tenant : AuditableEntity
{
    public string Name { get; set; } = default!;

    public string Slug { get; set; } = default!;

    public string? Domain { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = [];

    public ICollection<UserTenant> UserTenants { get; set; } = [];
}
