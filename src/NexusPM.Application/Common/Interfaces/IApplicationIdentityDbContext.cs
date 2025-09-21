using Microsoft.EntityFrameworkCore;

namespace NexusPM.Application.Common.Interfaces;

/// <summary>
/// Represents the application identity database context, providing access to identity-related entities.
/// </summary>
public interface IApplicationIdentityDbContext
{
    /// <summary>
    /// Gets <see cref="RefreshToken"/> entities.
    /// </summary>
    DbSet<RefreshToken> RefreshTokens { get; }

    /// <summary>
    /// Gets <see cref="UserSession"/> entities.
    /// </summary>
    DbSet<UserSession> UserSessions { get; }
}