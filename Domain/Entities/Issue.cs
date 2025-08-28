namespace NexusPM.Domain.Entities;
public class Issue : AuditableEntity, ITenantScoped
{
    public Guid TenantId { get; set; }

    public Guid ProjectId { get; set; }

    public Project? Project { get; set; }

    public string Key { get; set; } = default!;

    public int Number { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public IssueType Type { get; set; } = IssueType.Task;

    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    public IssueStatus Status { get; set; } = IssueStatus.Todo;

    public Guid? AssigneeId { get; set; }

    public AppUser? Assignee { get; set; }

    public Guid? ReporterId { get; set; }

    public AppUser? Reporter { get; set; }

    public Guid? SprintId { get; set; }

    public Sprint? Sprint { get; set; }

    public double? StoryPoints { get; set; }

    public DateTime? DueDateUtc { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];

    public ICollection<Attachment> Attachments { get; set; } = [];

    public ICollection<IssueTag> Tags { get; set; } = [];

    public ICollection<Worklog> Worklogs { get; set; } = [];

}
