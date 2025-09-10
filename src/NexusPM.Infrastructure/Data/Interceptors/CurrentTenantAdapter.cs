using NexusPM.Application.Abstractions;

namespace NexusPM.Infrastructure.Data.Interceptors;

/// <summary>
/// Adapter that provides current tenant information by wrapping an ITenantProvider.
/// </summary>
/// <param name="tp">The tenant provider to wrap.</param>
public class CurrentTenantAdapter(ITenantProvider tp)
    : ICurrentTenant
{
    private readonly ITenantProvider tp = tp;

    /// <summary>
    /// Gets the current tenant identifier.
    /// </summary>
    public Guid TenantId => this.tp.TenantId;

    /// <summary>
    /// Tries to get the current tenant identifier.
    /// </summary>
    /// <param name="tenantId">When this method returns, contains the tenant identifier if available; otherwise, Guid.Empty.</param>
    /// <returns>true if a valid tenant identifier is available; otherwise, false.</returns>
    public bool TryGet(out Guid tenantId)
    {
        tenantId = this.tp.TenantId;
        return tenantId != Guid.Empty;
    }
}
