namespace NexusPM.Domain.Entities;
public class AppUser : AuditableEntity
{

    [MaxLength(250)] public string Email { get; set; } = default!;

    [MaxLength(120)] public string DisplayName { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    //public ICollection<UserTenant> Tenants { get; set; } = new HashSet<UserTenant>();
}
