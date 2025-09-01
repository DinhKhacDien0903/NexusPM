// <copyright file="InvoiceLine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a line item in an invoice, including details such as description, quantity, unit price, and associated issue.
/// </summary>
public class InvoiceLine : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the identifier of the tenant to which this invoice line belongs.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the invoice to which this line belongs.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the invoice associated with this line.
    /// </summary>
    public Invoice? Invoice { get; set; }

    /// <summary>
    /// Gets or sets the description of the invoice line.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets or sets the quantity of time in minutes for this invoice line.
    /// </summary>
    public int QuantityMinutes { get; set; }

    /// <summary>
    /// Gets or sets the unit price per hour for this invoice line.
    /// </summary>
    public Money UnitPricePerHour { get; set; } = new Money(0, "USD");

    /// <summary>
    /// Gets or sets the total amount for this invoice line.
    /// </summary>
    public Money LineTotal { get; set; } = new Money(0, "USD");

    /// <summary>
    /// Gets or sets the identifier of the issue associated with this invoice line, if any.
    /// </summary>
    public Guid? IssueId { get; set; }

    /// <summary>
    /// Gets or sets the issue associated with this invoice line, if any.
    /// </summary>
    public Issue? Issue { get; set; }
}
