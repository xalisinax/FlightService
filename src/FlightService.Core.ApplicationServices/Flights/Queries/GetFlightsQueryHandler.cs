using FlightService.Core.Domain.Flights.Contracts;
using FlightService.Core.Domain.Flights.DTOs;
using FlightService.Core.Domain.Flights.Queries;
using MediatR;

namespace FlightService.Core.ApplicationServices.Flights.Queries;

internal class GetFlightsQueryHandler(IFlightsQueryRepository repository) : IRequestHandler<GetFlightsQuery, IList<FlightDto>>
{
    private readonly IFlightsQueryRepository _repository = repository;

    public async Task<IList<FlightDto>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetFlightsAync(cancellationToken);

        return result;
    }
}
