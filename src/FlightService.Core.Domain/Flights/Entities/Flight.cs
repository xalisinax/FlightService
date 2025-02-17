using FlightService.Core.Domain.Common.Domain;
using FlightService.Core.Domain.Common.Exceptions;
using FlightService.Core.Domain.Flights.Events;

namespace FlightService.Core.Domain.Flights.Entities;

public class Flight : Entity
{
    public string Id { get; private set; }
    public string Origin { get; private set; }
    public string Destination { get; private set; }
    public string Provider { get; private set; }
    public int Capacity { get; private set; }
    public FlightStatus Status { get; private set; }
    public DateTime TakeOffAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private readonly List<FlightPassenger> _passengers = [];

    public Flight(string id, string origin, string destination, string provider, int capacity, DateTime takeOffAt)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        Provider = provider;
        Capacity = capacity;
        Status = FlightStatus.Registering;
        TakeOffAt = takeOffAt;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new FlightCreated(Id, capacity, TakeOffAt));
    }

    public IReadOnlyList<FlightPassenger> Passengers => _passengers;


    public void AddPassenger(string userId, string seat)
    {
        if (Status != FlightStatus.Registering)
            throw new DomainStateException("Can't Register now!");

        if (_passengers.Count > Capacity)
            throw new DomainStateException("Maximum seats has been reached");

        if (_passengers.Any(x => x.UserId == userId))
            throw new DomainStateException("You have already reserved");

        _passengers.Add(new FlightPassenger(userId, seat, DateTime.UtcNow));

        if (_passengers.Count == Capacity)
        {
            Status = FlightStatus.FulyRegistered;
            UpdatedAt = DateTime.UtcNow;
        }

        AddEvent(new FlightPassengerAdded(userId, seat, Id));
    }

    public void InActivate()
    {
        Status = FlightStatus.InActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
