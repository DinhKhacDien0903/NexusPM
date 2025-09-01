// <copyright file="Sprint.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a sprint within a project, containing time-boxed work iterations.
/// </summary>
public class Sprint : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the project identifier this sprint belongs to.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the project navigation property.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the name of the sprint.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the UTC start date of the sprint.
    /// </summary>
    public DateTime StartDateUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC end date of the sprint.
    /// </summary>
    public DateTime EndDateUtc { get; set; }

    /// <summary>
    /// Gets or sets the current state of the sprint. Defaults to Planned.
    /// </summary>
    public SprintState State { get; set; } = SprintState.Planned;

    /// <summary>
    /// Gets or sets the optional goal or objective for the sprint.
    /// </summary>
    public string? Goal { get; set; }
}
