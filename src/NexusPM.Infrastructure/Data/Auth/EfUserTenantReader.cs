// Copyright (c) YourCompany. All rights reserved.

using NexusPM.Application.Abstractions.Security;
using NexusPM.Infrastructure.Data.Interceptors;

namespace NexusPM.Infrastructure.Data.Auth;

/// <summary>
/// Entity Framework implementation of the user tenant reader service.
/// </summary>
/// <param name="db">The database context.</param>
public class EfUserTenantReader(NexusDbContext db)
    : IUserTenantReader
{
    private readonly NexusDbContext db = db;

    /// <summary>
    /// Gets the user tenant information for the specified user and tenant.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <returns>The user tenant information if found; otherwise, null.</returns>
    public async Task<UserTenantInfo?> GetAsync(Guid userId, Guid tenantId)
    {
        UserTenant? ut = await this.db.Set<UserTenant>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.TenantId == tenantId);
        return ut is null ? null : new UserTenantInfo(ut.UserId, ut.TenantId, ut.Role, ut.IsSuspended);
    }

    /// <summary>
    /// Gets the default user tenant information for the specified user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The default user tenant information if found; otherwise, null.</returns>
    public async Task<UserTenantInfo?> GetDefaultAsync(Guid userId)
    {
        UserTenant? ut = await this.db.Set<UserTenant>().AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsSuspended)
            .OrderByDescending(x => x.UpdatedAtUtc)
            .ThenByDescending(x => x.CreatedAtUtc)
            .FirstOrDefaultAsync();

        return ut is null ? null : new UserTenantInfo(ut.UserId, ut.TenantId, ut.Role, ut.IsSuspended);
    }
}
