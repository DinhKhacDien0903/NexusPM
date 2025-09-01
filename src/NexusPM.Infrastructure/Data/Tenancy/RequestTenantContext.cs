// <copyright file="RequestTenantContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Tenancy;

using NexusPM.Application.Abstractions;
using InfraTenantProvider = Interceptors.ITenantProvider;

/// <summary>
/// Provides the current tenant context for the request.
/// </summary>
public sealed class RequestTenantContext : ICurrentTenant, InfraTenantProvider
{
    private static readonly AsyncLocal<Guid?> Current = new ();

    /// <summary>
    /// Gets the tenant ID for the current request.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the tenant is not set for the current request.</exception>
    public Guid TenantId => Current.Value
        ?? throw new InvalidOperationException("Tenant is not set for the current request.");

    /// <summary>
    /// Attempts to retrieve the tenant ID for the current request.
    /// </summary>
    /// <param name="tenantId">When this method returns, contains the tenant ID if it was set; otherwise, the default value.</param>
    /// <returns><c>true</c> if the tenant ID was set; otherwise, <c>false</c>.</returns>
    public bool TryGet(out Guid tenantId)
    {
        if (Current.Value is Guid v)
        {
            tenantId = v;
            return true;
        }

#pragma warning disable S4581
        tenantId = default;
#pragma warning restore S4581

        return false;
    }

    /// <summary>
    /// Sets the tenant ID for the current request.
    /// </summary>
    /// <param name="tenantId">The tenant ID to set.</param>
    /// <exception cref="ArgumentException">Thrown when the provided tenant ID is invalid.</exception>
    public void Set(Guid tenantId)
    {
        if (tenantId == Guid.Empty)
        {
            throw new ArgumentException("Invalid tenant id.", nameof(tenantId));
        }

        Current.Value = tenantId;
    }

    /// <summary>
    /// Clears the tenant ID for the current request.
    /// </summary>
    public void Clear() => Current.Value = null;
}
