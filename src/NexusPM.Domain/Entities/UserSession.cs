// <copyright file="UserSession.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities
{
    /// <summary>
    /// Represents a user session, including details such as the user ID, session ID, device, IP address, and revocation status.
    /// </summary>
    public class UserSession : AuditableEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user associated with this session.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for this session.
        /// </summary>
        public string SessionId { get; set; } = default!;

        /// <summary>
        /// Gets or sets the device information associated with this session, if available.
        /// </summary>
        public string? Device { get; set; }

        /// <summary>
        /// Gets or sets the IP address associated with this session, if available.
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// Gets or sets the UTC timestamp when this session was revoked, if applicable.
        /// </summary>
        public DateTime? RevokedAtUtc { get; set; }

        /// <summary>
        /// Gets a value indicating whether this session is currently active.
        /// </summary>
        public bool IsActive => this.RevokedAtUtc == null;
    }
}
