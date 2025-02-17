using FlightService.Core.Domain.Common.Domain;

namespace FlightService.Core.Domain.Flights.Events;

public class FlightPassengerAdded : IDomainEvent
{
    public FlightPassengerAdded(string id, string seat, string flightId)
    {
        Id = id;
        Seat = seat;
        FlightId = flightId;
    }

    public string Id { get; init; }
    public string FlightId { get; init; }
    public string Seat { get; init; }
}
