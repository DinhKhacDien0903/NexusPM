// <copyright file="Project.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a project entity that contains project information including members, boards, sprints, and issues.
/// </summary>
public class Project : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the unique key identifier for the project.
    /// </summary>
    public string Key { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name of the project.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the project.
    /// </summary>
    public ProjectType Type { get; set; }

    /// <summary>
    /// Gets or sets the optional description of the project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the collection of project members.
    /// </summary>
    public ICollection<ProjectMember> Members { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of boards associated with the project.
    /// </summary>
    public ICollection<Board> Boards { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of sprints associated with the project.
    /// </summary>
    public ICollection<Sprint> Sprints { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of issues associated with the project.
    /// </summary>
    public ICollection<Issue> Issues { get; set; } = [];
}
