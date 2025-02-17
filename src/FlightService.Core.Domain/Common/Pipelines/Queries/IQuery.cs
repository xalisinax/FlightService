using MediatR;

namespace FlightService.Core.Domain.Common.Pipelines.Queries;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
