using FlightService.Core.Domain.Common.Pipelines.Commands;

namespace FlightService.Core.Domain.Flights.Commands;

public class ReserveFlightCommand : ICommand<string>
{
    public ReserveFlightCommand(string id, string userId, string seat)
    {
        Id = id;
        UserId = userId;
        Seat = seat;
    }

    public string Id { get; init; }
    public string UserId { get; init; }
    public string Seat { get; init; }
}
