// <copyright file="Invoice.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents an invoice entity that contains details about a financial transaction.
/// </summary>
public class Invoice : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the unique identifier of the tenant associated with the invoice.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the invoice number.
    /// </summary>
    public string Number { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date and time when the invoice was issued, in UTC.
    /// </summary>
    public DateTime IssuedOnUtc { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the current status of the invoice.
    /// </summary>
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

    /// <summary>
    /// Gets or sets the subtotal amount of the invoice.
    /// </summary>
    public Money Subtotal { get; set; } = new Money(0, "USD");

    /// <summary>
    /// Gets or sets the tax amount applied to the invoice.
    /// </summary>
    public Money Tax { get; set; } = new Money(0, "USD");

    /// <summary>
    /// Gets or sets the total amount of the invoice, including tax.
    /// </summary>
    public Money Total { get; set; } = new Money(0, "USD");

    /// <summary>
    /// Gets or sets the collection of line items associated with the invoice.
    /// </summary>
    public ICollection<InvoiceLine> Lines { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of payments applied to the invoice.
    /// </summary>
    public ICollection<Payment> Payments { get; set; } = [];
}
