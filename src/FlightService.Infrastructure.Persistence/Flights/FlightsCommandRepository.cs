using FlightService.Core.Domain.Common.DI;
using FlightService.Core.Domain.Flights.Contracts;
using FlightService.Core.Domain.Flights.Entities;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;

namespace FlightService.Infrastructure.Persistence.Flights;

internal class FlightsCommandRepository(FlightDbContext commandContext) : IFlightsCommandRepository, IScoped
{
    private const string IdAlphabet = "abcdefghijklmnopqrstuvwxyz";
    private readonly FlightDbContext _commandContext = commandContext;

    public async Task AddAsync(Flight flight, CancellationToken cancellationToken)
    {
        await _commandContext.Flights.AddAsync(flight, cancellationToken);
    }

    public Task<string> GenerateNextId(CancellationToken cancellationToken)
    {
        var id = Nanoid.GenerateAsync(alphabet: IdAlphabet, 32);

        return id;
    }

    public async Task<Flight> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _commandContext.Flights.FirstOrDefaultAsync(x => x.Id == id);

        return result;
    }
}
