using FlightService.Core.Domain.Common.Domain;

namespace FlightService.Core.Domain.Flights.Events;

public class FlightCreated(string id, int capacity, DateTime takeOffAt) : IDomainEvent
{
    public string Id { get; init; } = id;
    public int Capacity { get; init; } = capacity;
    public DateTime TakeOffAt { get; init; } = takeOffAt;
}
