// Copyright (c) YourCompany. All rights reserved.

namespace NexusPM.Infrastructure.Identity.Services.KeyMaterial;

/// <summary>
/// Configuration options for JWT token generation and validation.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Gets or sets the JWT token issuer.
    /// </summary>
    public string Issuer { get; set; } = "nexuspm";

    /// <summary>
    /// Gets or sets the JWT token audience.
    /// </summary>
    public string Audience { get; set; } = "nexuspm.spa";

    /// <summary>
    /// Gets or sets the access token lifetime in minutes.
    /// </summary>
    public int AccessTokenMinutes { get; set; } = 15;

    /// <summary>
    /// Gets or sets the refresh token lifetime in days.
    /// </summary>
    public int RefreshTokenDays { get; set; } = 30;

    /// <summary>
    /// Gets or sets the current key identifier.
    /// </summary>
    public string CurrentKid { get; set; } = "kid-1";

    /// <summary>
    /// Gets or sets the private key in PEM format.
    /// </summary>
    public string? PrivateKeyPem { get; set; } // load từ secrets/KeyVault
}
