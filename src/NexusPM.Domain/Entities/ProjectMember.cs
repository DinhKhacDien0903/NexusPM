// <copyright file="ProjectMember.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents the relationship between a user and a project, including their role within the project.
/// </summary>
public class ProjectMember : AuditableEntity, ITenantScoped
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
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the project navigation property.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Gets or sets the user navigation property.
    /// </summary>
    public AppUser? User { get; set; }

    /// <summary>
    /// Gets or sets the role of the user within the project. Defaults to Contributor.
    /// </summary>
    public ProjectRole Role { get; set; } = ProjectRole.Contributor;
}
