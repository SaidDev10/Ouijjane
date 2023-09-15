using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using System.Collections;

namespace Ouijjane.Shared.Infrastructure.Persistence.Repositories;
public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private bool _disposed;
    private Hashtable? _repositories;
    //private readonly REDIS _cache;

    public UnitOfWork(TContext context, ILogger<UnitOfWork<TContext>> logger)
    {
        Context = context;
    }

    public TContext Context { get; }

    public async Task<int> Commit(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
    {
        var result = await Commit(cancellationToken);
        //TODO: foreach (var cacheKey in cacheKeys)
        //{
        //    _cache.Remove(cacheKey);
        //}
        return result;
    }

    public async Task<IContextTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        return await new ContextTransaction().BeginTransaction(Context);
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), Context);

            _repositories.Add(type, repositoryInstance);
        }

        return (_repositories[type] as IRepository<TEntity>)!;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        _disposed = true;
    }
}

