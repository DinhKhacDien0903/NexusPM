namespace NexusPM.Application.Abstractions.Notifications;

/// <summary>
/// Defines a contract for sending email notifications.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="toEmail">The recipient's email address.</param>
    /// <param name="otp">The OTP of the email.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    Task SendOTPAsync(string toEmail, string otp, string subject, CancellationToken ct = default);
}