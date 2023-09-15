using System.Transactions;

namespace Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> Commit(CancellationToken cancellationToken);

    Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);


    Task<IContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
}