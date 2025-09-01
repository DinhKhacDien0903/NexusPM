// <copyright file="Tag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a tag that can be applied to issues for categorization and organization.
/// </summary>
public class Tag : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the tag.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the hexadecimal color value for the tag display.
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Gets or sets the collection of issue-tag relationships.
    /// </summary>
    public ICollection<IssueTag> Issues { get; set; } = [];
}
