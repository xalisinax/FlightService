using FlighService.Core.Domain.Common.Domain;

namespace FlighService.Core.Domain.Flights.Events;

public class FlightPassengerAdded : IDomainEvent
{
    public FlightPassengerAdded(string id, string flightId)
    {
        Id = id;
        FlightId = flightId;
    }

    public string Id { get; init; }
    public string FlightId { get; init; }
}
