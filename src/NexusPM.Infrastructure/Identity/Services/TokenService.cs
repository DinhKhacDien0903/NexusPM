// Copyright (c) YourCompany. All rights reserved.

using Microsoft.Extensions.Options;
using NexusPM.Application.Abstractions.Security;
using NexusPM.Infrastructure.Identity.Interceptors;
using NexusPM.Infrastructure.Identity.Services.KeyMaterial;

namespace NexusPM.Infrastructure.Identity.Services;

public class TokenService(ApplicationIdentityDbContext idDb, IUserTenantReader tenantReader, IKeyMaterialProvider keys, IOptions<JwtOptions> options) : ITokenService
{
    private readonly ApplicationIdentityDbContext idDb = idDb;
    private readonly IUserTenantReader tenantReader = tenantReader;
    private readonly JwtOptions opt = options.Value;
    private readonly IKeyMaterialProvider keys = keys;

    public async Task<TokenPair> IssueAsync(Guid userId, Guid tenantId, string requestedRole, string email, string? device = null, string? ip = null)
    {
        UserTenantInfo? ut = await this.tenantReader.GetAsync(userId, tenantId) ?? throw new InvalidOperationException("User is not part of the tenant");
        if (ut.IsSuspended)
        {
            throw new InvalidOperationException("User is suspended");
        }

        if (!string.Equals(ut.Role.ToString(), requestedRole, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("User does not have the requested role");
        }

        string sid = Guid.NewGuid().ToString("N");
    }

    public Task<TokenPair> RefreshAsync(string refreshToken, string? device = null, string? ip = null)
    {
        throw new NotImplementedException();
    }

    public Task RevokeRefreshFamilyAsync(Guid userId, string family)
    {
        throw new NotImplementedException();
    }

    public Task RevokeSessionAsync(Guid userId, string sessionId)
    {
        throw new NotImplementedException();
    }

    public Task<TokenPair> SwitchTenantAsync(Guid userId, Guid newTenantId, string requestedRole)
    {
        throw new NotImplementedException();
    }
}
