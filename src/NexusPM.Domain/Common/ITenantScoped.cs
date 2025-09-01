// <copyright file="ITenantScoped.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Common;

/// <summary>
/// Represents an entity that is scoped to a specific tenant.
/// </summary>
public interface ITenantScoped
{
    /// <summary>
    /// Gets or sets the unique identifier of the tenant.
    /// </summary>
    Guid TenantId { get; set; }
}
