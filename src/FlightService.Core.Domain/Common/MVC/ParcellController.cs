using FlightService.Core.Domain.Common.Exceptions;
using FlightService.Core.Domain.Common.Pipelines.Commands;
using FlightService.Core.Domain.Common.Pipelines.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FlightService.Core.Domain.Common.MVC;

public abstract class ParcellController : ControllerBase
{
    private IMediator Mediator
    {
        get
        {
            return HttpContext.RequestServices.GetRequiredService<IMediator>();
        }
    }
    protected async Task<IActionResult> SendCommand<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand<TResponse>
    {
        try
        {
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
    protected async Task<IActionResult> SendCommand<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        try
        {
            await Mediator.Send(command, cancellationToken);

            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
    protected async Task<IActionResult> SendQuery<TQuery,TResponse>(TQuery query,CancellationToken cancellationToken) where TQuery : IQuery<TResponse>
    {
        try
        {
            var result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
