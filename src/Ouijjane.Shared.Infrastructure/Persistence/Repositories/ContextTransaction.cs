using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;

namespace Ouijjane.Shared.Infrastructure.Persistence.Repositories;
internal sealed class ContextTransaction : IContextTransaction
{
    private IDbContextTransaction? _transaction;

    public async Task<IContextTransaction> BeginTransaction(DbContext context)
    {
        _transaction = await context.Database.BeginTransactionAsync();
        return this;
    }

    public async Task Commit(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;

        await _transaction.CommitAsync(cancellationToken);
    }

    public async Task Rollback(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;

        await _transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing) return;
        if (_transaction is null) return;

        _transaction.Dispose();
        _transaction = null;

    }

    private async ValueTask DisposeAsyncCore()
    {
        if (_transaction is null) return;

        await _transaction.DisposeAsync();

        _transaction = null;
    }
}