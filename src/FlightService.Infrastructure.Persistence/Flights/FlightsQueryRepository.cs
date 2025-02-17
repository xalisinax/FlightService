using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlightService.Core.Domain.Common.DI;
using FlightService.Core.Domain.Flights.Contracts;
using FlightService.Core.Domain.Flights.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Infrastructure.Persistence.Flights;

internal class FlightsQueryRepository(IMapper mapper, FlightDbContext dbContext) : IFlightsQueryRepository, IScoped
{
    private readonly IMapper _mapper = mapper;
    private readonly FlightDbContext _dbContext = dbContext;

    public async Task<IList<FlightDto>> GetFlightsAync(CancellationToken cancellationToken)
    {
        var result = await _dbContext.Flights
            .AsNoTracking()
            .OrderByDescending(x => x.TakeOffAt)
            .ProjectTo<FlightDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<IList<FlightReservationDto>> GetReservationsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var result = await _dbContext.FlightReservationQueryModels
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ProjectTo<FlightReservationDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
