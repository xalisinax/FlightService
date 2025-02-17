﻿using FlightService.Core.Domain.Common.Exceptions;
using FlightService.Core.Domain.Common.Extensions;
using FlightService.Core.Domain.Common.Uow;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlightService.Core.Domain.Common.Pipelines.Commands;

public class CommandPipeline<TRequest, TReponse>(IUnitOfWork unitOfWork, ILogger<CommandPipeline<TRequest, TReponse>> logger,IValidator<TRequest> validator = null) : IPipelineBehavior<TRequest, TReponse> where TRequest : ICommand<TReponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<TRequest> _validator = validator;
    private readonly ILogger<CommandPipeline<TRequest, TReponse>> _logger = logger;

    public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
    {
        if(_validator != null)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.ToDictionary(e => e.PropertyName.ToCamelCase(), e => e.ErrorMessage);
                throw new BadRequestException(errors);
            }
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
