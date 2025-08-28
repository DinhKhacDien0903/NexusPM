namespace NexusPM.Domain.Entities;
public class ProjectMember : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }

    public Project? Project { get; set; }

    public AppUser? User { get; set; }

    public ProjectRole Role { get; set; } = ProjectRole.Contributor;
}
