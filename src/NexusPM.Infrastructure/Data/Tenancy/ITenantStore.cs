// <copyright file="ITenantStore.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Tenancy;

/// <summary>
/// Represents a store for managing tenant-related data.
/// </summary>
public interface ITenantStore
{
    /// <summary>
    /// Checks if a tenant with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the tenant.</param>
    /// <param name="ct">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the tenant exists.</returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Resolves a tenant identifier by its slug.
    /// </summary>
    /// <param name="slug">The slug associated with the tenant.</param>
    /// <param name="ct">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the tenant identifier if found; otherwise, null.</returns>
    Task<Guid?> ResolveBySlugAsync(string slug, CancellationToken ct);
}