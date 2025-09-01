// <copyright file="Attachment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents an attachment entity that is associated with an issue and scoped to a tenant.
/// </summary>
public class Attachment : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the identifier of the tenant to which this attachment belongs.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the issue to which this attachment is linked.
    /// </summary>
    public Guid IssueId { get; set; }

    /// <summary>
    /// Gets or sets the issue entity associated with this attachment.
    /// </summary>
    public Issue? Issue { get; set; }

    /// <summary>
    /// Gets or sets the name of the file for this attachment.
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the MIME type of the file for this attachment.
    /// </summary>
    public string ContentType { get; set; } = default!;

    /// <summary>
    /// Gets or sets the size of the file in bytes.
    /// </summary>
    public long SizeBytes { get; set; }

    /// <summary>
    /// Gets or sets the storage key used to locate the file in the storage system.
    /// </summary>
    public string StorageKey { get; set; } = default!;
}
