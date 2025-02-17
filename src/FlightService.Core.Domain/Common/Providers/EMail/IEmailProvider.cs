namespace FlightService.Core.Domain.Common.Providers.EMail;

public interface IEmailProvider
{
    Task Send(string to, string subject, string body);
}
