namespace FlighService.Core.Domain.Common.Exceptions;

public class BadRequestException(string message) : Exception(message)
{
}
