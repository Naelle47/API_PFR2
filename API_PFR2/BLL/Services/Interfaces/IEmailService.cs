namespace API_PFR2.BLL.Services.Implementations;

/// <summary>
/// Provides functionality for sending emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email message.
    /// </summary>
    /// <param name="to">Recipient email address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="body">Email body content.</param>
    void Send(string to, string subject, string body);
}