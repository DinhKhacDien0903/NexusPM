namespace NexusPM.Domain.Entities;
public class Project : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public string Key { get; set; } = default!;

    public string Name { get; set; } = default!;

    public ProjectType Type { get; set; }

    public string? Description { get; set; }

    public ICollection<ProjectMember> Members { get; set; } = [];

    public ICollection<Board> Boards { get; set; } = [];

    public ICollection<Sprint> Sprints { get; set; } = [];

    public ICollection<Issue> Issues { get; set; } = [];
}
