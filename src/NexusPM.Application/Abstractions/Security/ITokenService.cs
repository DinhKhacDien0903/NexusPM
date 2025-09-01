// <copyright file="ITokenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Application.Abstractions.Author;
using NexusPM.Application.Abstractions.Security;

/// <summary>
/// Defines methods for issuing, refreshing, switching, and revoking tokens for user authentication and authorization.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Issues a new token pair for the specified user, tenant, and role.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="tenantId">The unique identifier of the tenant.</param>
    /// <param name="requestedRole">The role requested by the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="device">The device information (optional).</param>
    /// <param name="ip">The IP address of the request (optional).</param>
    /// <returns>A <see cref="TokenPair"/> containing the access and refresh tokens.</returns>
    Task<TokenPair> IssueAsync(Guid userId, Guid tenantId, string requestedRole, string email, string? device = null, string? ip = null);

    /// <summary>
    /// Refreshes the token pair using the provided refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to use for generating a new token pair.</param>
    /// <param name="device">The device information (optional).</param>
    /// <param name="ip">The IP address of the request (optional).</param>
    /// <returns>A <see cref="TokenPair"/> containing the new access and refresh tokens.</returns>
    Task<TokenPair> RefreshAsync(string refreshToken, string? device = null, string? ip = null);

    /// <summary>
    /// Switches the tenant for the specified user and issues a new token pair.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="newTenantId">The unique identifier of the new tenant.</param>
    /// <param name="requestedRole">The role requested by the user.</param>
    /// <returns>A <see cref="TokenPair"/> containing the new access and refresh tokens.</returns>
    Task<TokenPair> SwitchTenantAsync(Guid userId, Guid newTenantId, string requestedRole);

    /// <summary>
    /// Revokes a specific session for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="sessionId">The identifier of the session to revoke.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RevokeSessionAsync(Guid userId, string sessionId);

    /// <summary>
    /// Revokes all tokens in a refresh token family for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="family">The identifier of the refresh token family to revoke.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RevokeRefreshFamilyAsync(Guid userId, string family);
}
