namespace FlightService.Core.Domain.Flights.Entities;

public record FlightPassenger(string UserId, string Seat, DateTime ReservedAt);
