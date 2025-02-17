using FlightService.Core.Domain.Flights.Entities;

namespace FlightService.Core.Domain.Flights.Contracts;

public interface IFlightsCommandRepository
{
    Task<string> GenerateNextId(CancellationToken cancellationToken);
    Task AddAsync(Flight flight, CancellationToken cancellationToken);
    Task<Flight> GetByIdAsync(string id, CancellationToken cancellationToken);
}
