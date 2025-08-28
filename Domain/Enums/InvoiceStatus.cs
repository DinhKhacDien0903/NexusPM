namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the status of an invoice.
/// </summary>
public enum InvoiceStatus
{
    /// <summary>
    /// The invoice is in draft state and not yet finalized.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// The invoice is open and awaiting payment.
    /// </summary>
    Open = 1,

    /// <summary>
    /// The invoice has been paid in full.
    /// </summary>
    Paid = 2,

    /// <summary>
    /// The invoice has been voided and is no longer valid.
    /// </summary>
    Void = 3,

    /// <summary>
    /// The invoice is deemed uncollectible.
    /// </summary>
    Uncollectible = 4,
}
