using API_PFR2.BLL.Services.Interfaces;
using System.Net.Mail;

namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides email sending functionality using SMTP (compatible with MailPit for dev/test).
/// </summary>
public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;

    public EmailService(IConfiguration configuration)
    {
        _smtpServer = configuration["Email:SmtpServer"] ?? "localhost";
        _smtpPort = int.Parse(configuration["Email:SmtpPort"] ?? "1025");
        _senderEmail = configuration["Email:SenderEmail"] ?? "noreply@nivo.com";
    }

    public void Send(string to, string subject, string body)
    {
        try
        {
            using var message = new MailMessage();
            message.From = new MailAddress(_senderEmail);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            using var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                EnableSsl = false // MailPit n’utilise pas SSL
            };

            client.Send(message);

            // Confirmation en console — pratique pour le développement
            Console.WriteLine($"[EMAIL] Envoyé à {to} | Sujet : {subject}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EMAIL ERROR] Impossible d'envoyer le mail à {to}: {ex.Message}");
        }
    }
}