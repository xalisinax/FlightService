using FlighService.Core.Domain.Common.Exceptions;
using FlighService.Core.Domain.Common.Uow;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlighService.Core.Domain.Common.Pipelines.Commands;

public class CommandPipeline<TRequest, TReponse>(IUnitOfWork unitOfWork, IValidator<TRequest> validator, ILogger<CommandPipeline<TRequest, TReponse>> logger) : IPipelineBehavior<TRequest, TReponse> where TRequest : ICommand
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<TRequest> _validator = validator;
    private readonly ILogger<CommandPipeline<TRequest, TReponse>> _logger = logger;

    public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage));
            throw new BadRequestException(errors);
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var result = await next();

            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogError("Error in execuring command of type : {type}", typeof(TRequest));

            throw;
        }
    }
}
