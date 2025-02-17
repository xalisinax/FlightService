using FlightService.Core.Domain.Flights.Contracts;
using FlightService.Core.Domain.Flights.DTOs;
using FlightService.Core.Domain.Flights.Queries;
using MediatR;

namespace FlightService.Core.ApplicationServices.Flights.Queries;

internal class GetMyFlightReservationsQueryHandler(IFlightsQueryRepository queryRepository) : IRequestHandler<GetMyFlightReservationsQuery, IList<FlightReservationDto>>
{
    private readonly IFlightsQueryRepository _queryRepository = queryRepository;

    public async Task<IList<FlightReservationDto>> Handle(GetMyFlightReservationsQuery request, CancellationToken cancellationToken)
    {
        var result = await _queryRepository.GetReservationsByUserIdAsync(request.UserId, cancellationToken);

        return result;
    }
}
