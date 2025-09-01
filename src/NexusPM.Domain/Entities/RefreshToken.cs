// <copyright file="RefreshToken.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a refresh token used for authentication and authorization purposes.
/// </summary>
public class RefreshToken : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the identifier of the tenant to which this token belongs.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user associated with this token.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the role of the user within the tenant.
    /// </summary>
    public TenantRole Role { get; set; } = TenantRole.Member;

    /// <summary>
    /// Gets or sets the hashed value of the token.
    /// </summary>
    public string TokenHash { get; set; } = default!;

    /// <summary>
    /// Gets or sets the identifier of the JWT associated with this token.
    /// </summary>
    public string JwtId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the family identifier for grouping related tokens.
    /// </summary>
    public string Family { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration date and time of the token in UTC.
    /// </summary>
    public DateTime ExpiresAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the token was revoked, if applicable.
    /// </summary>
    public DateTime? RevokedAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the hash of the token that replaced this token, if applicable.
    /// </summary>
    public string? ReplacedByTokenHash { get; set; }

    /// <summary>
    /// Gets a value indicating whether the token is currently active.
    /// </summary>
    public bool IsActive => this.RevokedAtUtc == null && DateTime.UtcNow < this.ExpiresAtUtc;
}
