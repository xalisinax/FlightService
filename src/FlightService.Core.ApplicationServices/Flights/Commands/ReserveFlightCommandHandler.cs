using FlightService.Core.Domain.Common.Exceptions;
using FlightService.Core.Domain.Flights.Commands;
using FlightService.Core.Domain.Flights.Contracts;
using MediatR;

namespace FlightService.Core.ApplicationServices.Flights.Commands;

internal class ReserveFlightCommandHandler : IRequestHandler<ReserveFlightCommand, string>
{
    private readonly IFlightsCommandRepository _repository;

    public ReserveFlightCommandHandler(IFlightsCommandRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(ReserveFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await _repository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException();

        flight.AddPassenger(request.UserId, request.Seat);

        return flight.Id;
    }
}
