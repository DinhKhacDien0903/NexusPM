// <copyright file="EfTenantStore.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Tenancy;
using NexusPM.Infrastructure.Data.Interceptors;

/// <summary>
/// Represents an implementation of the <see cref="ITenantStore"/> interface using Entity Framework.
/// </summary>
internal sealed class EfTenantStore : ITenantStore
{
    private readonly NexusDbContext db;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfTenantStore"/> class.
    /// </summary>
    /// <param name="db">The database context used to access tenant data.</param>
    public EfTenantStore(NexusDbContext db) => this.db = db;

    /// <summary>
    /// Checks if a tenant with the specified identifier exists and is not marked as deleted.
    /// </summary>
    /// <param name="id">The unique identifier of the tenant.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a value indicating whether the tenant exists.</returns>
    public Task<bool> ExistsAsync(Guid id, CancellationToken ct) =>
        this.db.Tenants.AnyAsync(t => t.Id == id && !t.IsDeleted, ct);

    /// <summary>
    /// Resolves the unique identifier of a tenant based on its slug.
    /// </summary>
    /// <param name="slug">The slug of the tenant.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the unique identifier of the tenant, or null if no matching tenant is found.</returns>
    public Task<Guid?> ResolveBySlugAsync(string slug, CancellationToken ct) =>
        this.db.Tenants.Where(t => t.Slug == slug && !t.IsDeleted)
                   .Select(t => (Guid?)t.Id)
                   .FirstOrDefaultAsync(ct);
}
