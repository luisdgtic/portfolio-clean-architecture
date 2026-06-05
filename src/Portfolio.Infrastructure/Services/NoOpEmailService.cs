using Microsoft.Extensions.Logging;
using Portfolio.Application.Common.Interfaces;

namespace Portfolio.Infrastructure.Services;

public class NoOpEmailService : IEmailService
{
    private readonly ILogger<NoOpEmailService> _logger;

    public NoOpEmailService(ILogger<NoOpEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "[EMAIL NO-OP] To: {To}, Subject: {Subject}, Body: {Body}",
            to, subject, body);

        return Task.CompletedTask;
    }
}
