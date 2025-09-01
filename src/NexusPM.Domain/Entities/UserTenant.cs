// <copyright file="UserTenant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents the relationship between a user and a tenant, including their role and suspension status.
/// </summary>
public class UserTenant : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant navigation property.
    /// </summary>
    public Tenant? Tenant { get; set; }

    /// <summary>
    /// Gets or sets the user navigation property.
    /// </summary>
    public AppUser? User { get; set; }

    /// <summary>
    /// Gets or sets the user's role within the tenant. Defaults to Member.
    /// </summary>
    public TenantRole Role { get; set; } = TenantRole.Member;

    /// <summary>
    /// Gets or sets a value indicating whether the user is suspended from the tenant.
    /// </summary>
    public bool IsSuspended { get; set; }
}
