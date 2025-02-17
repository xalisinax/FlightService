using FlightService.Core.Domain.Flights.DTOs;

namespace FlightService.Core.Domain.Flights.Contracts;

public interface IFlightsQueryRepository
{
    Task<IList<FlightDto>> GetFlightsAync(CancellationToken cancellationToken);
    Task<IList<FlightReservationDto>> GetReservationsByUserIdAsync(string userId,CancellationToken cancellationToken);
}
