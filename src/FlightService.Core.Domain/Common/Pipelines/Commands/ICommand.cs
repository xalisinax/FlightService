using MediatR;

namespace FlightService.Core.Domain.Common.Pipelines.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
{

}