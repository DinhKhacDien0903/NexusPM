// <copyright file="Tenant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a tenant entity that contains information about a tenant, including its name, slug, domain, and associated subscriptions and user-tenants.
/// </summary>
public class Tenant : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the tenant.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique slug identifier for the tenant.
    /// </summary>
    public string Slug { get; set; } = default!;

    /// <summary>
    /// Gets or sets the optional domain associated with the tenant.
    /// </summary>
    public string? Domain { get; set; }

    /// <summary>
    /// Gets or sets the collection of subscriptions associated with the tenant.
    /// </summary>
    public ICollection<Subscription> Subscriptions { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of user-tenant relationships associated with the tenant.
    /// </summary>
    public ICollection<UserTenant> UserTenants { get; set; } = [];
}
