// <copyright file="AuditLog.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents an audit log entry that tracks changes to resources within a tenant's scope.
/// </summary>
public class AuditLog : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the identifier of the tenant associated with this audit log entry.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the type of the resource being audited.
    /// </summary>
    public string ResourceType { get; set; } = default!;

    /// <summary>
    /// Gets or sets the identifier of the resource being audited.
    /// </summary>
    public Guid ResourceId { get; set; }

    /// <summary>
    /// Gets or sets the action performed on the resource.
    /// </summary>
    public string Action { get; set; } = default!;

    /// <summary>
    /// Gets or sets the JSON representation of the resource's old values before the action.
    /// </summary>
    public string? OldValuesJson { get; set; }

    /// <summary>
    /// Gets or sets the JSON representation of the resource's new values after the action.
    /// </summary>
    public string? NewValuesJson { get; set; }
}
