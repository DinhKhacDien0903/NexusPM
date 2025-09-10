// Copyright (c) YourCompany. All rights reserved.

using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace NexusPM.Infrastructure.Identity.Services.KeyMaterial;

/// <summary>
/// Provides RSA key material for JWT token signing and validation using in-memory storage.
/// </summary>
public class InMemoryRsaKeyMaterialProvider : IKeyMaterialProvider
{
    private readonly JwtOptions options;
    private readonly RSA rsa;

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryRsaKeyMaterialProvider"/> class.
    /// </summary>
    /// <param name="options">The JWT configuration options.</param>
    public InMemoryRsaKeyMaterialProvider(IOptions<JwtOptions> options)
    {
        this.options = options.Value;
        this.rsa = RSA.Create();
        var pem = this.options.PrivateKeyPem;
        if (!string.IsNullOrWhiteSpace(pem))
        {
            this.rsa.ImportFromPem(pem);
        }
        else
        {
            throw new InvalidOperationException("Jwt.PrivateKeyPem is required");
        }
    }

    /// <summary>
    /// Gets the signing credentials for JWT token signing.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the signing credentials.</returns>
    public Task<SigningCredentials> GetSigningCredentialsAsync()
    {
        RsaSecurityKey key = new
        (this.rsa)
        {
            KeyId = this.options.CurrentKid,
        };
        return Task.FromResult(new SigningCredentials(key, SecurityAlgorithms.RsaSha256));
    }

    /// <summary>
    /// Gets the validation keys for JWT token validation.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of validation keys.</returns>
    public Task<IReadOnlyList<SecurityKey>> GetValidationKeysAsync()
    {
        RSAParameters pub = this.rsa.ExportParameters(false);
        return Task.FromResult<IReadOnlyList<SecurityKey>>(new List<SecurityKey>
        {
            new RsaSecurityKey(pub) { KeyId = this.options.CurrentKid },
        });
    }

    /// <summary>
    /// Sets the active key identifier (kid) for the current JWT configuration.
    /// </summary>
    /// <param name="kid">The key identifier to set as active.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task SetActiveKidAsync(string kid)
    {
        this.options.CurrentKid = kid;
        return Task.CompletedTask;
    }
}
