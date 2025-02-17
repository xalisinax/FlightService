using FlightService.Core.Domain.Common.Pipelines.Commands;

namespace FlightService.Core.Domain.Flights.Commands;

public class CreateFlightCommand(string origin, string destination, string provider, int capacity, DateTime takeOffAt) : ICommand<string>
{
    public string Origin { get; init; } = origin;
    public string Destination { get; init; } = destination;
    public string Provider { get; init; } = provider;
    public int Capacity { get; init; } = capacity;
    public DateTime TakeOffAt { get; init; } = takeOffAt;
}
