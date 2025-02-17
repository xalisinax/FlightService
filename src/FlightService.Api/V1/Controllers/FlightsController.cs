using FlightService.Core.Domain.Common.MVC;
using FlightService.Core.Domain.Flights.Commands;
using FlightService.Api.V1.Models;
using Microsoft.AspNetCore.Mvc;
using FlightService.Core.Domain.Flights.Queries;
using FlightService.Core.Domain.Flights.DTOs;
using Microsoft.AspNetCore.Authorization;
using FlightService.Core.Domain.Common.Extensions;

namespace FlightService.Api.V1.Controllers;

[ApiController]
[Route("v1/[controller]")]
[Authorize]
public class FlightsController : ParcellController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFlightRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateFlightCommand(request.Origin, request.Destination, request.Provider, request.Capacity, request.TakeOffAt);

        return await SendCommand<CreateFlightCommand, string>(command, cancellationToken);
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetFlightsQuery();

        return await SendQuery<GetFlightsQuery, IList<FlightDto>>(query, cancellationToken);
    }

    [HttpPatch("{id}/reserve")]
    public async Task<IActionResult> Reserve([FromRoute] string id, [FromBody] ReserveFlightRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var command = new ReserveFlightCommand(id, userId, request.Seat);

        return await SendCommand<ReserveFlightCommand, string>(command, cancellationToken);
    }

    [HttpGet("mine")]
    public async Task<IActionResult> MyFlights(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var query = new GetMyFlightReservationsQuery(userId);

        return await SendQuery<GetMyFlightReservationsQuery, IList<FlightReservationDto>>(query, cancellationToken);
    }

    [HttpDelete("{id}/mine")]
    public async Task<IActionResult> DeleteMyFlight([FromRoute] string id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var command = new UnReserveFlightCommand(id, userId);

        return await SendCommand<UnReserveFlightCommand, string>(command, cancellationToken);
    }
}
