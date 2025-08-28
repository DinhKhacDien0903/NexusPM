namespace NexusPM.Domain.Entities;
public class Board : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid ProjectId { get; set; }

    public Project? Project { get; set; }

    [MaxLength(120)]
    public string Name { get; set; } = default!;

    public ICollection<BoardColumn> Columns { get; set; } = [];
}
