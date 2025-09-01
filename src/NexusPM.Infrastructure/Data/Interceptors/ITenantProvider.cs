// <copyright file="ITenantProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Interceptors;

/// <summary>
/// Provides the current tenant context for multi-tenant operations.
/// </summary>
public interface ITenantProvider
{
    /// <summary>
    /// Gets the current tenant identifier.
    /// </summary>
    Guid TenantId { get; }
}
