using FlighService.Core.Domain.Common.Exceptions;

namespace FlighService.Core.Domain.Flights.Entities;

public class Flight
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

    public Flight(string id, string origin, string destination, string provider, int maxPassenger, DateTime takeOffAt)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        Provider = provider;
        Capacity = maxPassenger;
        Status = FlightStatus.Registering;
        TakeOffAt = takeOffAt;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public IReadOnlyList<FlightPassenger> Passengers => _passengers;


    public void AddPassenger(string id, string seat)
    {
        if (_passengers.Count > Capacity)
            throw new DomainStateException("Maximum seats has been reached");

        _passengers.Add(new FlightPassenger(id, seat, DateTime.UtcNow));

        if (_passengers.Count == Capacity)
        {
            Status = FlightStatus.FulyRegistered;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void InActivate()
    {
        Status = FlightStatus.InActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
