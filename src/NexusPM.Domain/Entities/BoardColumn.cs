namespace NexusPM.Domain.Entities;
public class BoardColumn : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid BoardId { get; set; }

    public Board? Board { get; set; }

    public IssueStatus Status { get; set; } = IssueStatus.Todo;

    public int Order { get; set; }

    public string? WipLimitNote { get; set; }
}
