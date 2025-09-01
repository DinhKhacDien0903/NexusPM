// <copyright file="Worklog.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a work log entry tracking time spent on an issue.
/// </summary>
public class Worklog : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the issue identifier this worklog is associated with.
    /// </summary>
    public Guid IssueId { get; set; }

    /// <summary>
    /// Gets or sets the issue navigation property.
    /// </summary>
    public Issue? Issue { get; set; }

    /// <summary>
    /// Gets or sets the user identifier who logged the work.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user navigation property.
    /// </summary>
    public AppUser? User { get; set; }

    /// <summary>
    /// Gets or sets the UTC date and time when work was started.
    /// </summary>
    public DateTime StartedAtUtc { get; set; }

    /// <summary>
    /// Gets or sets the duration of work in minutes.
    /// </summary>
    public int Minutes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this work is billable. Defaults to true.
    /// </summary>
    public bool IsBillable { get; set; } = true;

    /// <summary>
    /// Gets or sets the hourly rate for this work entry.
    /// </summary>
    public Money? HourlyRate { get; set; }

    /// <summary>
    /// Gets or sets the invoice line identifier if this worklog has been invoiced.
    /// </summary>
    public Guid? InvoiceLineId { get; set; }

    /// <summary>
    /// Gets or sets the invoice line navigation property.
    /// </summary>
    public InvoiceLine? InvoiceLine { get; set; }

    /// <summary>
    /// Gets or sets an optional note describing the work performed.
    /// </summary>
    public string? Note { get; set; }
}
