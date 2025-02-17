using FlightService.Core.Domain.Common.DI;
using FlightService.Core.Domain.Common.Providers.EMail;
using Microsoft.Extensions.Logging;

namespace FlightService.Infrastructure.Provision.Emails;

internal class FakeEmailProvider(ILogger<FakeEmailProvider> logger) : IEmailProvider, ISingleton
{
    private readonly ILogger<FakeEmailProvider> _logger = logger;

    public Task Send(string to, string subject, string body)
    {
        _logger.LogCritical("Email sent to : {to} with subject : {subject} and body of {body} \r", to, subject, body);

        return Task.CompletedTask;
    }
}
