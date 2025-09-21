using MediatR;
using Microsoft.Extensions.Logging;
using NexusPM.Application.Abstractions.Notifications;
using NexusPM.Domain.Events;

namespace NexusPM.Application.Features.Auth.Signup.EventHandlers;

/// <summary>
/// Handles the <see cref="SignedUpEvent"/> by logging the signup and sending an OTP email.
/// </summary>
public partial class SignedUpEventProcessor(ILogger<SignedUpEvent> logger, IEmailSender emailSender)
    : INotificationHandler<SignedUpEvent>
{
    /// <summary>
    /// The logger action for handle signedup event called.
    /// </summary>
    private static readonly Action<ILogger, string, Exception?> SignedUpLog =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(SignedUpEvent)),
            "User signed up with email: {Email}");

    /// <summary>
    /// Handles the <see cref="SignedUpEvent"/> notification.
    /// </summary>
    /// <param name="notification">The event notification containing user signup details.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task Handle(SignedUpEvent notification, CancellationToken cancellationToken)
    {
        SignedUpLog(logger, notification.User.Email, null);

        await emailSender.SendOTPAsync(
            notification.User.Email,
            GenerateOtp(),
            "Welcome to NexusPM",
            cancellationToken);
    }

    private static string GenerateOtp() => new Random().Next(100000, 999999).ToString(System.Globalization.CultureInfo.InvariantCulture);
}