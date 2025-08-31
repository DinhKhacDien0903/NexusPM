namespace NexusPM.Domain.Entities;

public class UserTenant : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid UserId { get; set; }

    public Tenant? Tenant { get; set; }

    public AppUser? User { get; set; }

    public TenantRole Role { get; set; } = TenantRole.Member;

    public bool IsSuspended { get; set; }
}
