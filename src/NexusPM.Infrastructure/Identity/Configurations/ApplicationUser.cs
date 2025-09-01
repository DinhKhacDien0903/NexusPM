// <copyright file="ApplicationUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Identity.Configurations;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// Represents an application user in the Identity system, extending the base IdentityUser with Guid as the primary key.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
}

/// <summary>
/// Represents an application role in the Identity system, extending the base IdentityRole with Guid as the primary key.
/// </summary>
public class ApplicationRole : IdentityRole<Guid>
{
}
