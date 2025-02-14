using FlighService.Core.Domain.Common.Exceptions;
using FlighService.Core.Domain.Common.Pipelines.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FlighService.Core.Domain.Common.MVC;

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
            return BadRequest(ex.Message);
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
}
