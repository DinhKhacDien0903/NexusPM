namespace NexusPM.Domain.Entities;

public class Sprint : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid ProjectId { get; set; }

    public Project? Project { get; set; }

    public string Name { get; set; } = default!;

    public DateTime StartDateUtc { get; set; }

    public DateTime EndDateUtc { get; set; }

    public SprintState State { get; set; } = SprintState.Planned;

    public string? Goal { get; set; }
}
