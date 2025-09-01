// <copyright file="ICurrentUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Application.Abstractions.Author
{
    /// <summary>
    /// Represents the current user in the application context.
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// Gets a value indicating whether the user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the unique identifier of the user.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Gets the unique identifier of the tenant associated with the user.
        /// </summary>
        Guid TenantId { get; }

        /// <summary>
        /// Gets the email address of the user, if available.
        /// </summary>
        string? Email { get; }

        /// <summary>
        /// Gets the role of the user within the tenant, if available.
        /// </summary>
        string? TenantRole { get; }

        /// <summary>
        /// Gets the session identifier for the current user session, if available.
        /// </summary>
        string? SessionId { get; }
    }
}
