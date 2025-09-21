using Microsoft.Extensions.Options;
using MimeKit;
using NexusPM.Application.Abstractions.Notifications;

namespace NexusPM.Infrastructure.Notifications.Email;

/// <summary>
/// Sends email messages using the SMTP protocol.
/// </summary>
public class SmtpEmailOTPSender(IOptions<SmtpOptions> opt)
    : IEmailSender
{
    /// <summary>
    /// Sends an email asynchronously to the specified recipient.
    /// </summary>
    /// <param name="toEmail">The recipient's email address.</param>
    /// <param name="otp">The otp of the email.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendOTPAsync(string toEmail, string otp, string subject, CancellationToken ct = default)
    {
        using var msg = new MimeMessage();

        msg.From.Add(new MailboxAddress("NexusPM", opt.Value.FromUser));
        msg.To.Add(MailboxAddress.Parse(toEmail));
        msg.Subject = subject;
        msg.Body = new TextPart("html")
        {
            Text = GenerateOtpEmailMessage(opt.Value.FromUser, otp),
        };

        using var client = new MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync(opt.Value.Host, opt.Value.Port, opt.Value.EnableSsl, ct);
        if (!string.IsNullOrWhiteSpace(opt.Value.FromUser))
        {
            await client.AuthenticateAsync(opt.Value.FromUser, opt.Value.Password, ct);
        }

        await client.SendAsync(msg, ct);
        await client.DisconnectAsync(true, ct);
    }

    private static string GenerateOtpEmailMessage(string userName, string otpCode)
    {
        return $@"
                <html>
                    <head>
                        <style>
                            .otp-container {{
                                font-family: Arial, sans-serif;
                                max-width: 600px;
                                margin: auto;
                                border: 1px solid #eee;
                                padding: 20px;
                                border-radius: 10px;
                                background-color: #f9f9f9;
                            }}
                            .otp-code {{
                                font-size: 24px;
                                font-weight: bold;
                                color: #2c3e50;
                            }}
                            .footer {{
                                margin-top: 20px;
                                font-size: 12px;
                                color: #888;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='otp-container'>
                            <h2>Xác minh tài khoản NexusPM</h2>
                            <p>Xin chào <strong>{userName}</strong>,</p>
                            <p>Cảm ơn bạn đã sử dụng nexuspm. Mã xác thực OTP của bạn là:</p>
                            <p class='otp-code'>{otpCode}</p>
                            <p>Vui lòng nhập mã này vào ứng dụng để hoàn tất quá trình xác minh.</p>
                            <p class='footer'>Mã OTP sẽ hết hạn sau 5 phút. Nếu bạn không yêu cầu mã này, vui lòng bỏ qua email.</p>
                        </div>
                    </body>
                </html>";
    }
}