using GatePass.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GatePass.Infrastructure;
public class FakeEmailSender : IEmailSender
{
    private readonly ILogger<FakeEmailSender> _logger;

    public FakeEmailSender(ILogger<FakeEmailSender> logger)
    {
        _logger = logger;
    }

    public Task<bool> SendEmailAsync(string to, string from, string subject, string body)
    {
        _logger.LogWarning("Sending email to {to} from {from} with subject {subject}.", to, from, subject);
        _logger.LogWarning("Email content: {body}", body);

        return Task.FromResult(true);
    }
}
