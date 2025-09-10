using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NexusPM.Application.Abstractions.Security;
using NexusPM.Domain.Enums;
using NexusPM.Infrastructure.Identity.Interceptors;
using NexusPM.Infrastructure.Identity.Services.KeyMaterial;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace NexusPM.Infrastructure.Identity.Services;

/// <summary>
/// Provides functionality for issuing, refreshing, and revoking tokens.
/// </summary>
public class TokenService(ApplicationIdentityDbContext db, IUserTenantReader tenantReader, IKeyMaterialProvider keys, IOptions<JwtOptions> options)
    : ITokenService
{
    private readonly ApplicationIdentityDbContext db = db;
    private readonly IUserTenantReader tenantReader = tenantReader;
    private readonly JwtOptions opt = options.Value;
    private readonly IKeyMaterialProvider keys = keys;
    private readonly JwtSecurityTokenHandler handler = new ();

    /// <summary>
    /// Issues a new token pair (access and refresh tokens) for the specified user and tenant.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="tenantId">The ID of the tenant.</param>
    /// <param name="requestedRole">The role requested by the user.</param>
    /// <param name="email">The email of the user.</param>
    /// <param name="device">The device information (optional).</param>
    /// <param name="ip">The IP address (optional).</param>
    /// <returns>A <see cref="TokenPair"/> containing the access and refresh tokens.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the user is not part of the tenant, is suspended, or does not have the requested role.</exception>
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

        var sid = Guid.NewGuid().ToString("N");
        this.db.UserSessions.Add(new UserSession
        {
            UserId = userId,
            SessionId = sid,
            Device = device,
            Ip = ip,
            CreatedAtUtc = DateTime.UtcNow,
        });

        var (access, exp, jti, family) = await this.CreateAccessTokenAsync(userId, tenantId, requestedRole, email, sid);
        var refresh = await this.CreateRefreshTokenAsync(userId, ut.TenantId, ut.Role.ToString(), jti, family);
        await this.db.SaveChangesAsync();

        return new TokenPair(access, exp, refresh);
    }

    /// <summary>
    /// Refreshes the token pair (access and refresh tokens) using the provided refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to use for refreshing.</param>
    /// <param name="device">The device information (optional).</param>
    /// <param name="ip">The IP address (optional).</param>
    /// <returns>A <see cref="TokenPair"/> containing the new access and refresh tokens.</returns>
    /// <exception cref="SecurityTokenException">Thrown if the refresh token is invalid, reused, or if the user's membership is revoked or suspended.</exception>
    public async Task<TokenPair> RefreshAsync(string refreshToken, string? device = null, string? ip = null)
    {
        var hash = Sha256(refreshToken);
        var rt = await this.db.RefreshTokens.FirstOrDefaultAsync(x => x.TokenHash == hash);
        if (rt == null || !rt.IsActive)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (rt.ReplacedByTokenHash != null)
        {
            await this.RevokeFamily(rt.Family);
            await this.db.SaveChangesAsync();
            throw new SecurityTokenException("Refresh token reused");
        }

        var ut = await this.tenantReader.GetAsync(rt.UserId, rt.TenantId)
                 ?? throw new SecurityTokenException("Membership revoked");
        if (ut.IsSuspended)
        {
            throw new SecurityTokenException("Membership suspended");
        }

        if (!string.Equals(ut.Role.ToString(), rt.Role.ToString(), StringComparison.OrdinalIgnoreCase))
        {
            throw new SecurityTokenException("Role changed");
        }

        rt.RevokedAtUtc = DateTime.UtcNow;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var (access, exp, jti, family) = await this.CreateAccessTokenAsync(rt.UserId, rt.TenantId, rt.Role.ToString(), email: null, sid: null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        var newRefresh = await this.CreateRefreshTokenAsync(rt.UserId, rt.TenantId, rt.Role.ToString(), jti, family);
        rt.ReplacedByTokenHash = Sha256(newRefresh);

        await this.db.SaveChangesAsync();
        return new TokenPair(access, exp, newRefresh);
    }

    /// <summary>
    /// Revokes all refresh tokens in the specified family for the given user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="family">The family ID of the refresh tokens to revoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RevokeRefreshFamilyAsync(Guid userId, string family)
    {
        await this.db.RefreshTokens
            .Where(x => x.UserId == userId && x.Family == family && x.RevokedAtUtc == null)
            .ExecuteUpdateAsync(s => s.SetProperty(r => r.RevokedAtUtc, _ => DateTime.UtcNow));
    }

    /// <summary>
    /// Revokes a specific user session by marking it as revoked.
    /// </summary>
    /// <param name="userId">The ID of the user whose session is to be revoked.</param>
    /// <param name="sessionId">The ID of the session to revoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RevokeSessionAsync(Guid userId, string sessionId)
    {
        var s = await this.db.UserSessions.FirstOrDefaultAsync(x => x.UserId == userId && x.SessionId == sessionId);
        if (s != null)
        {
            s.RevokedAtUtc = DateTime.UtcNow;
        }

        await this.db.SaveChangesAsync();
    }

    /// <summary>
    /// Switches the user's active tenant and issues a new token pair (access and refresh tokens) for the specified tenant.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="newTenantId">The ID of the new tenant to switch to.</param>
    /// <param name="requestedRole">The role requested by the user in the new tenant.</param>
    /// <returns>A <see cref="TokenPair"/> containing the access and refresh tokens for the new tenant.</returns>
    /// <exception cref="SecurityTokenException">Thrown if the user does not have membership in the target tenant, the membership is suspended, or the role does not match.</exception>
    public async Task<TokenPair> SwitchTenantAsync(Guid userId, Guid newTenantId, string requestedRole)
    {
        var ut = await this.tenantReader.GetAsync(userId, newTenantId)

                 ?? throw new SecurityTokenException("No membership in target tenant");

        if (ut.IsSuspended)
        {
            throw new SecurityTokenException("Membership suspended");
        }

        if (!string.Equals(ut.Role.ToString(), requestedRole, StringComparison.OrdinalIgnoreCase))
        {
            throw new SecurityTokenException("Role mismatch");
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

        var (access, exp, jti, family) = await this.CreateAccessTokenAsync(userId, ut.TenantId, ut.Role.ToString(), email: null, sid: null);

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        var refresh = await this.CreateRefreshTokenAsync(userId, ut.TenantId, ut.Role.ToString(), jti, family);

        await this.db.SaveChangesAsync();

        return new TokenPair(access, exp, refresh);
    }

    /// <summary>
    /// Revokes all refresh tokens in a specific family.
    /// </summary>
    /// <param name="family">The family ID of the refresh tokens to revoke.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task RevokeFamily(string family)
    {
        await this.db.RefreshTokens.Where(x => x.Family == family && x.RevokedAtUtc == null)
            .ExecuteUpdateAsync(s => s.SetProperty(r => r.RevokedAtUtc, _ => DateTime.UtcNow));
    }

    /// <summary>
    /// Creates a refresh token for the specified user and tenant.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="tenantId">The ID of the tenant.</param>
    /// <param name="tenantRole">The role of the user in the tenant.</param>
    /// <param name="jti">The JWT ID.</param>
    /// <param name="family">The family ID.</param>
    /// <returns>The raw refresh token.</returns>
    private async Task<string> CreateRefreshTokenAsync(Guid userId, Guid tenantId, string tenantRole, string jti, string family)
    {
        _ = Enum.TryParse<TenantRole>(tenantRole, out TenantRole parsedRole);
        var raw = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        this.db.RefreshTokens.Add(new RefreshToken
        {
            UserId = userId,
            TenantId = tenantId,
            Role = parsedRole,
            JwtId = jti,
            Family = family,
            TokenHash = Sha256(raw),
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(this.opt.RefreshTokenDays),
        });

        await this.db.SaveChangesAsync();
        return raw;
    }

    /// <summary>
    /// Creates an access token for the specified user and tenant.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="tenantId">The ID of the tenant.</param>
    /// <param name="requestedRole">The role requested by the user.</param>
    /// <param name="email">The email of the user.</param>
    /// <param name="sid">The session ID.</param>
    /// <returns>A tuple containing the access token, expiration time, JWT ID, and family ID.</returns>
    private async Task<(string access, DateTime exp, string jti, string family)> CreateAccessTokenAsync(Guid userId, Guid tenantId, string requestedRole, string email, string sid)
    {
        var now = DateTime.UtcNow;
        var exp = now.AddMinutes(this.opt.AccessTokenMinutes);
        var jti = Guid.NewGuid().ToString("N");
        var family = Guid.NewGuid().ToString("N");

        var user = await this.db.Users.AsNoTracking().FirstAsync(u => u.Id == userId);
        var securityStamp = user.SecurityStamp ?? string.Empty;

        var claims = new[]
        {
          new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
          new Claim(JwtRegisteredClaimNames.Jti, jti),
          new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)now).ToUnixTimeSeconds().ToString(System.Globalization.CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
          new Claim(JwtRegisteredClaimNames.Email, email),
          new Claim("tid", tenantId.ToString()),
          new Claim("trole", requestedRole),
          new Claim("sid", sid ?? Guid.NewGuid().ToString("N")),
          new Claim("ver", securityStamp),
        };

        var signing = await this.keys.GetSigningCredentialsAsync();
        var jwt = new JwtSecurityToken(this.opt.Issuer, this.opt.Audience, claims, notBefore: now, expires: exp, signingCredentials: signing);
        var token = this.handler.WriteToken(jwt);
        return (token, exp, jti, family);
    }

    /// <summary>
    /// Computes the SHA-256 hash of the specified string.
    /// </summary>
    /// <param name="s">The input string.</param>
    /// <returns>The SHA-256 hash as a hexadecimal string.</returns>
    private static string Sha256(string s) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(s)));
}
