namespace FlightService.Core.Domain.Common.Exceptions;

public class DomainStateException(string message) : Exception(message)
{
}
