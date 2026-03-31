using API_PFR2.BLL.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides email sending functionality using SMTP.
/// </summary>
public class EmailService : IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;
    private readonly string _senderPassword;

    public EmailService(IConfiguration configuration)
    {
        // Lecture depuis la configuration — plus de credentials hardcodés
        _smtpServer = configuration["Email:SmtpServer"] ?? "smtp.gmail.com";
        _smtpPort = int.Parse(configuration["Email:SmtpPort"] ?? "587");
        _senderEmail = configuration["Email:SenderEmail"] ?? string.Empty;
        _senderPassword = configuration["Email:SenderPassword"] ?? string.Empty;
    }

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
        // Confirmation en console — utile en développement
        Console.WriteLine($"[EMAIL] Envoyé à {to} | Sujet : {subject}");
    }
}
