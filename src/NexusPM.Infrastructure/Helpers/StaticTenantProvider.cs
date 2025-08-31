namespace NexusPM.Infrastructure.Helpers;
using NexusPM.Infrastructure.Data.Interceptors;

public sealed class StaticTenantProvider(Guid tenantId)
    : ITenantProvider
{
    public Guid TenantId { get; } = tenantId;
}
