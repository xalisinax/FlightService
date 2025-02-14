using FlighService.Core.Domain.Common.Domain;

namespace FlighService.Core.Domain.Flights.Events;

public class FlightCreated(string id, int capacity, DateTime takeOffAt) : IDomainEvent
{
    public string Id { get; init; } = id;
    public int Capacity { get; init; } = capacity;
    public DateTime TakeOffAt { get; init; } = takeOffAt;
}
