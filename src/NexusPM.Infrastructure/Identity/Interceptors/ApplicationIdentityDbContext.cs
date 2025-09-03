// <copyright file="ApplicationIdentityDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Identity.Interceptors;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusPM.Infrastructure.Identity.Configurations;

/// <summary>
/// Database context for ASP.NET Core Identity, managing users and roles for authentication and authorization.
/// </summary>
public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationIdentityDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
        : base(options)
    {
    }
}
