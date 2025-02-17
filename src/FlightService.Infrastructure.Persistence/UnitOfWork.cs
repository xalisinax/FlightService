using FlightService.Core.Domain.Common.DI;
using FlightService.Core.Domain.Common.Uow;
using Microsoft.EntityFrameworkCore.Storage;

namespace FlightService.Infrastructure.Persistence;

internal class UnitOfWork(FlightDbContext commandContext) : IUnitOfWork, IScoped
{
    private readonly FlightDbContext _commandContext = commandContext;

    private IDbContextTransaction _transaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _transaction = await _commandContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            return;

        await _commandContext.SaveChangesAsync(cancellationToken);

        await _transaction.CommitAsync(cancellationToken);

        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            return;

        await _transaction.RollbackAsync(cancellationToken);
        _transaction = null;
    }
}
