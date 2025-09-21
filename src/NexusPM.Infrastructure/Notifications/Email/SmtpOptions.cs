namespace NexusPM.Infrastructure.Notifications.Email;

/// <summary>
/// Represents the SMTP configuration options.
/// </summary>
public class SmtpOptions
{
    /// <summary>
    /// Gets or sets the SMTP server host.
    /// </summary>
    public string Host { get; set; } = default!;

    /// <summary>
    /// Gets or sets the SMTP server port.
    /// </summary>
    public int Port { get; set; } = 587;

    /// <summary>
    /// Gets or sets the SMTP user name.
    /// </summary>
    public string FromUser { get; set; } = default!;

    /// <summary>
    /// Gets or sets the SMTP password.
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Gets or sets a value indicating whether SSL is enabled for SMTP.
    /// </summary>
    public bool EnableSsl { get; set; } = true;
}