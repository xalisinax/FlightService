using FlightService.Core.Domain.Flights.Commands;
using FlightService.Core.Domain.Flights.Contracts;
using FlightService.Core.Domain.Flights.Entities;
using MediatR;

namespace FlightService.Core.ApplicationServices.Flights.Commands;

public class CreateFlighCommandHandler(IFlightsCommandRepository commandRepository) : IRequestHandler<CreateFlightCommand, string>
{
    private readonly IFlightsCommandRepository _commandRepository = commandRepository;

    public async Task<string> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var newId = await _commandRepository.GenerateNextId(cancellationToken);

        var flight = new Flight(newId, request.Origin, request.Destination, request.Provider, request.Capacity, request.TakeOffAt);

        await _commandRepository.AddAsync(flight, cancellationToken);

        return newId;
    }
}
