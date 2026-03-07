using API_PFR2.BLL.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides email sending functionality using SMTP.
/// </summary>
public class EmailService : IEmailService
{
    private readonly string _smtpServer = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _senderEmail = "noreply@nivo.com";
    private readonly string _senderPassword = "password";

    /// <inheritdoc/>
    public void Send(string to, string subject, string body)
    {
        var message = new MailMessage();
        message.From = new MailAddress(_senderEmail);
        message.To.Add(to);
        message.Subject = subject;
        message.Body = body;

        using var client = new SmtpClient(_smtpServer, _smtpPort)
        {
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            EnableSsl = true
        };

        client.Send(message);
    }
}
