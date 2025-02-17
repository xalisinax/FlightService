using AutoMapper;
using FlightService.Core.Domain.Flights.DTOs;
using FlightService.Core.Domain.Flights.QueryModels;

namespace FlightService.MappingProfiles.Flights.QueryModels;

public class FlightReservationQueryModelProfile : Profile
{
    public FlightReservationQueryModelProfile()
    {
        CreateMap<FlightReservationQueryModel, FlightReservationDto>()
            .ForMember(x => x.TakeOffAt, mo => mo.MapFrom(s => s.TakeOffAt.ToString()))
            .ForMember(x => x.Seat, mo => mo.MapFrom(s => $"CH:{s.Seat}"));

    }
}
