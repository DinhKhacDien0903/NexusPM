// Copyright (c) YourCompany. All rights reserved.

namespace NexusPM.API.Authorization;
using Microsoft.AspNetCore.Authorization;
using NexusPM.Domain.Enums;

/// <summary>
/// Represents a requirement for a minimum tenant role in the authorization process.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TenantRoleRequirement"/> class with the specified minimum role.
/// </remarks>
/// <param name="minRole">The minimum tenant role required.</param>
public sealed class TenantRoleRequirement(TenantRole minRole)
    : IAuthorizationRequirement
{
    /// <summary>
    /// Gets the minimum tenant role required.
    /// </summary>
    public TenantRole MinimumRole { get; } = minRole;
}
