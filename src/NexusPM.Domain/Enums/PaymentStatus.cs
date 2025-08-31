namespace NexusPM.Domain.Enums;

/// <summary>
/// Represents the status of a payment in the system.
/// </summary>
public enum PaymentStatus
{
    /// <summary>
    /// The payment is pending and has not been processed yet.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The payment was processed successfully.
    /// </summary>
    Succeeded = 1,

    /// <summary>
    /// The payment failed during processing.
    /// </summary>
    Failed = 2,

    /// <summary>
    /// The payment was refunded to the payer.
    /// </summary>
    Refunded = 3,
}
