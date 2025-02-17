using FlightService.Core.Domain.Common.Exceptions;
using FlightService.Core.Domain.Flights.Commands;
using FlightService.Core.Domain.Flights.Contracts;
using MediatR;

namespace FlightService.Core.ApplicationServices.Flights.Commands;

internal class UnReserveFlightCommandHandler(IFlightsCommandRepository flightsCommandRepository) : IRequestHandler<UnReserveFlightCommand, string>
{
    private readonly IFlightsCommandRepository _flightsCommandRepository = flightsCommandRepository;

    public async Task<string> Handle(UnReserveFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await _flightsCommandRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException();

        flight.RemovePassenger(request.UserId);

        return flight.Id;
    }
}
