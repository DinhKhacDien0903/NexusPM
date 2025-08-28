namespace NexusPM.Domain.Entities;
public class AppUser : AuditableEntity
{
    public string Email { get; set; } = default!;

    public string DisplayName { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    public ICollection<UserTenant> Tenants { get; set; } = [];
}
