// <copyright file="Issue.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents an issue within a project management system.
/// </summary>
public class Issue : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the project identifier.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the project associated with the issue.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the unique key of the issue.
    /// </summary>
    public string Key { get; set; } = default!;

    /// <summary>
    /// Gets or sets the issue number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the title of the issue.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets or sets the description of the issue.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the type of the issue.
    /// </summary>
    public IssueType Type { get; set; } = IssueType.Task;

    /// <summary>
    /// Gets or sets the priority of the issue.
    /// </summary>
    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    /// <summary>
    /// Gets or sets the status of the issue.
    /// </summary>
    public IssueStatus Status { get; set; } = IssueStatus.Todo;

    /// <summary>
    /// Gets or sets the identifier of the assignee.
    /// </summary>
    public Guid? AssigneeId { get; set; }

    /// <summary>
    /// Gets or sets the user assigned to the issue.
    /// </summary>
    public AppUser? Assignee { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the reporter.
    /// </summary>
    public Guid? ReporterId { get; set; }

    /// <summary>
    /// Gets or sets the user who reported the issue.
    /// </summary>
    public AppUser? Reporter { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the sprint.
    /// </summary>
    public Guid? SprintId { get; set; }

    /// <summary>
    /// Gets or sets the sprint associated with the issue.
    /// </summary>
    public Sprint? Sprint { get; set; }

    /// <summary>
    /// Gets or sets the story points for the issue.
    /// </summary>
    public double? StoryPoints { get; set; }

    /// <summary>
    /// Gets or sets the due date of the issue in UTC.
    /// </summary>
    public DateTime? DueDateUtc { get; set; }

    /// <summary>
    /// Gets or sets the collection of comments associated with the issue.
    /// </summary>
    public ICollection<Comment> Comments { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of attachments associated with the issue.
    /// </summary>
    public ICollection<Attachment> Attachments { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of tags associated with the issue.
    /// </summary>
    public ICollection<IssueTag> Tags { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of worklogs associated with the issue.
    /// </summary>
    public ICollection<Worklog> Worklogs { get; set; } = [];
}
