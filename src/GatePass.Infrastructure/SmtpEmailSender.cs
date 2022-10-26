using System.Net.Mail;
using GatePass.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GatePass.Infrastructure;
public class SmtpEmailSender : IEmailSender
{
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(ILogger<SmtpEmailSender> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(string to, string from, string subject, string body)
    {
        var emailClient = new SmtpClient("localhost");
        var message = new MailMessage
        {
            From = new MailAddress(from),
            Subject = subject,
            Body = body
        };

        message.To.Add(new MailAddress(to));
        try
        {
            await emailClient.SendMailAsync(message);
            _logger.LogWarning("Sending email to {to} from {from} with subject {subject}.", to, from, subject);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occured while sending mail: {0}", ex.Message);
            return false;
        }
    }
}
