// <copyright file="ICurrentTenant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Application.Abstractions;

/// <summary>
/// Represents the current tenant in the application context.
/// Provides access to the tenant's unique identifier and methods to retrieve it.
/// </summary>
public interface ICurrentTenant
{
    /// <summary>
    /// Gets the unique identifier of the current tenant.
    /// </summary>
    Guid TenantId { get; }

    /// <summary>
    /// Attempts to retrieve the unique identifier of the current tenant.
    /// </summary>
    /// <param name="tenantId">When this method returns, contains the unique identifier of the tenant if available; otherwise, <see cref="Guid.Empty"/>.</param>
    /// <returns><c>true</c> if the tenant identifier was successfully retrieved; otherwise, <c>false</c>.</returns>
    bool TryGet(out Guid tenantId);
}
