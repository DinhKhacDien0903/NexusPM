// <copyright file="Comment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a comment entity that is associated with an issue and scoped to a tenant.
/// </summary>
public class Comment : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the identifier of the tenant to which this comment belongs.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the issue to which this comment is associated.
    /// </summary>
    public Guid IssueId { get; set; }

    /// <summary>
    /// Gets or sets the issue entity associated with this comment.
    /// </summary>
    public Issue? Issue { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the author who created this comment.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Gets or sets the author entity who created this comment.
    /// </summary>
    public AppUser? Author { get; set; }

    /// <summary>
    /// Gets or sets the body content of the comment.
    /// </summary>
    public string Body { get; set; } = default!;
}
