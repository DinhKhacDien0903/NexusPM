// <copyright file="IssueTag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a tag associated with an issue in a tenant-scoped context.
/// </summary>
public class IssueTag : ITenantScoped
{
    /// <summary>
    /// Gets or sets the identifier of the tenant.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the issue.
    /// </summary>
    public Guid IssueId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the tag.
    /// </summary>
    public Guid TagId { get; set; }

    /// <summary>
    /// Gets or sets the issue associated with this tag.
    /// </summary>
    public Issue? Issue { get; set; }

    /// <summary>
    /// Gets or sets the tag associated with this issue.
    /// </summary>
    public Tag? Tag { get; set; }
}
