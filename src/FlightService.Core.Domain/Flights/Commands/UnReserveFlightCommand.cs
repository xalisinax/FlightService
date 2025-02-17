using FlightService.Core.Domain.Common.Pipelines.Commands;

namespace FlightService.Core.Domain.Flights.Commands;

public class UnReserveFlightCommand : ICommand<string>
{
    public UnReserveFlightCommand(string id, string userId)
    {
        Id = id;
        UserId = userId;
    }

    public string Id { get; init; }
    public string UserId { get; init; }
}
