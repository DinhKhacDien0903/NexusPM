using NexusPM.Domain.Enums;

namespace NexusPM.API.Authorization;

/// <summary>
/// Provides hierarchy ranking for tenant roles.
/// </summary>
public static class RoleHierarchy
{
    /// <summary>
    /// Gets the hierarchical rank of a tenant role, where lower numbers indicate higher authority.
    /// </summary>
    /// <param name="r">The tenant role to rank.</param>
    /// <returns>An integer representing the role's rank in the hierarchy.</returns>
    public static int Rank(TenantRole r) => r switch
    {
        TenantRole.Owner => 0,
        TenantRole.Admin => 1,
        TenantRole.Member => 2,
        TenantRole.Billing => 3,
        _ => 0
    };
}
