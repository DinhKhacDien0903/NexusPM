// Copyright (c) YourCompany. All rights reserved.

using NexusPM.Domain.Enums;

namespace NexusPM.Application.Abstractions.Security;

/// <summary>
/// Represents information about a user's tenant association.
/// </summary>
/// <param name="UserId">The user identifier.</param>
/// <param name="TenantId">The tenant identifier.</param>
/// <param name="Role">The user's role within the tenant.</param>
/// <param name="IsSuspended">Indicates whether the user is suspended in the tenant.</param>
public record UserTenantInfo(Guid UserId, Guid TenantId, TenantRole Role, bool IsSuspended);

/// <summary>
/// Provides read access to user-tenant relationship information.
/// </summary>
public interface IUserTenantReader
{
    /// <summary>
    /// Gets the user-tenant information for the specified user and tenant.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <returns>The user-tenant information if found; otherwise, null.</returns>
    Task<UserTenantInfo?> GetAsync(Guid userId, Guid tenantId);

    /// <summary>
    /// Gets the default user-tenant information for the specified user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>The default user-tenant information if found; otherwise, null.</returns>
    Task<UserTenantInfo?> GetDefaultAsync(Guid userId);
}
