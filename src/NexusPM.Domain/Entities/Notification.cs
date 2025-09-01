// <copyright file="Notification.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a notification entity that is scoped to a tenant.
/// </summary>
public class Notification
   : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the identifier of the tenant to which this notification belongs.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who is the recipient of this notification.
    /// </summary>
    public Guid RecipientUserId { get; set; }

    /// <summary>
    /// Gets or sets the recipient user associated with this notification.
    /// </summary>
    public AppUser? Recipient { get; set; }

    /// <summary>
    /// Gets or sets the type of the notification.
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// Gets or sets the payload of the notification in JSON format.
    /// </summary>
    public string PayloadJson { get; set; } = "{}";

    /// <summary>
    /// Gets or sets a value indicating whether the notification has been read.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the notification was read.
    /// </summary>
    public DateTime? ReadAtUtc { get; set; }
}
