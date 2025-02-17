using FlightService.Core.Domain.Common.Pipelines.Queries;
using FlightService.Core.Domain.Flights.DTOs;

namespace FlightService.Core.Domain.Flights.Queries;

public class GetFlightsQuery : IQuery<IList<FlightDto>>
{
}
