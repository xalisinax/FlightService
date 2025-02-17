namespace FlightService.Core.Domain.Common.Exceptions;

public class BadRequestException : Exception
{
    private const string DefaultErrorKey = "Default";
    public Dictionary<string, string> Errors { get; init; }

    public BadRequestException(string message)
    {
        Errors = new Dictionary<string, string>
        {
            { DefaultErrorKey, message }
        };
    }

    public BadRequestException(Dictionary<string, string> errors)
    {
        Errors = errors;
    }
}


