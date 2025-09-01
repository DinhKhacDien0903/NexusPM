// <copyright file="StaticTenantProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Helpers;

using NexusPM.Infrastructure.Data.Interceptors;

/// <summary>
/// A static implementation of ITenantProvider that always returns the same tenant ID.
/// Used primarily for seeding data or testing scenarios where a fixed tenant context is needed.
/// </summary>
/// <param name="tenantId">The tenant ID to be provided by this instance.</param>
public sealed class StaticTenantProvider(Guid tenantId)
    : ITenantProvider
{
    /// <summary>
    /// Gets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; } = tenantId;
}
