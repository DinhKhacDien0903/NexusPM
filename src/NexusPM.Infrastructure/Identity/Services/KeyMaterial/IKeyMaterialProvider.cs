// Copyright (c) YourCompany. All rights reserved.

using Microsoft.IdentityModel.Tokens;

namespace NexusPM.Infrastructure.Identity.Services.KeyMaterial;

/// <summary>
/// Provides key material for signing and validation operations.
/// </summary>
public interface IKeyMaterialProvider
{
    /// <summary>
    /// Gets the signing credentials for token signing operations.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the signing credentials.</returns>
    Task<SigningCredentials> GetSigningCredentialsAsync();

    /// <summary>
    /// Gets the validation keys for token validation operations.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of security keys.</returns>
    Task<IReadOnlyList<SecurityKey>> GetValidationKeysAsync();

    /// <summary>
    /// Sets the active key identifier.
    /// </summary>
    /// <param name="kid">The key identifier to set as active.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SetActiveKidAsync(string kid);
}
