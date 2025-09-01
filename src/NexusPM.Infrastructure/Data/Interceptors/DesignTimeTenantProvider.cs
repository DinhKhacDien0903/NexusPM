// <copyright file="DesignTimeTenantProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Data.Interceptors;

/// <summary>
/// Provides the tenant ID for design-time operations.
/// </summary>
public class DesignTimeTenantProvider : ITenantProvider
{
    /// <summary>
    /// Gets the singleton instance of the <see cref="DesignTimeTenantProvider"/> class.
    /// </summary>
    public static readonly DesignTimeTenantProvider Instance = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="DesignTimeTenantProvider"/> class.
    /// Sets the TenantId based on the TENANT_ID environment variable, or defaults to Guid.Empty if parsing fails.
    /// </summary>
    private DesignTimeTenantProvider()
        => this.TenantId = Guid.TryParse(Environment.GetEnvironmentVariable("TENANT_ID"), out var id) ? id : Guid.Empty;

    /// <summary>
    /// Gets the tenant ID.
    /// </summary>
    public Guid TenantId { get; }
}
