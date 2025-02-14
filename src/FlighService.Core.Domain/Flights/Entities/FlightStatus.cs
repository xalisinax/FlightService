namespace FlighService.Core.Domain.Flights.Entities;

public enum FlightStatus
{
    Registering = 0,
    InActive = 1,
    FulyRegistered = 2,
    Boarding = 4,
    TakingOff = 8,
    Landing = 16,
}
