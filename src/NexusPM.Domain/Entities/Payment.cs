// <copyright file="Payment.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Domain.Entities;

/// <summary>
/// Represents a payment made against an invoice, tracking payment details and status.
/// </summary>
public class Payment : AuditableEntity, ITenantScoped
{
    /// <summary>
    /// Gets or sets the tenant identifier.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the invoice identifier this payment is for.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the invoice navigation property.
    /// </summary>
    public Invoice? Invoice { get; set; }

    /// <summary>
    /// Gets or sets the payment amount. Defaults to $0 USD.
    /// </summary>
    public Money Amount { get; set; } = new Money(0, "USD");

    /// <summary>
    /// Gets or sets the payment provider name. Defaults to "manual".
    /// </summary>
    public string Provider { get; set; } = "manual";

    /// <summary>
    /// Gets or sets the provider's reference identifier for this payment.
    /// </summary>
    public string ProviderRef { get; set; } = default!;

    /// <summary>
    /// Gets or sets the current status of the payment. Defaults to Pending.
    /// </summary>
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    /// <summary>
    /// Gets or sets the UTC date when the payment was completed, if applicable.
    /// </summary>
    public DateTime? PaidOnUtc { get; set; }
}
