using AutoMapper;
using FlightService.Core.Domain.Flights.DTOs;
using FlightService.Core.Domain.Flights.Entities;

namespace FlightService.MappingProfiles.Flights.Entities;

public class FlightProfile : Profile
{
    public FlightProfile()
    {
        CreateMap<Flight, FlightDto>()
            .ForMember(x => x.Capacity, mo => mo.MapFrom(s => s.Capacity - s.Passengers.Count))
            .ForMember(x => x.TakeOff, mo => mo.MapFrom(s => s.TakeOffAt.ToString()));
    }
}
