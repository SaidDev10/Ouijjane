using Microsoft.EntityFrameworkCore;
using Ouijjane.Shared.Application.Extenstions;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using Ouijjane.Shared.Application.Specifications;
using System.Linq.Expressions;

namespace Ouijjane.Shared.Infrastructure.Persistence.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context = context;
    }

    public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public async Task<bool> Contains(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await Count(specification, cancellationToken) > 0;
    }

    public async Task<bool> Contains(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
    {
        return await Count(specifications, cancellationToken) > 0;
    }

    public async Task<bool> Contains(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Count(predicate, cancellationToken) > 0;
    }

    public async Task<int> Count(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Specify(specification).CountAsync(cancellationToken);
    }

    public async Task<int> Count(IEnumerable<ISpecification<TEntity>?>? specifications, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            query = specifications.Aggregate(query, (current, specification) => current.Specify(specification));
        }

        return await query.CountAsync(cancellationToken);
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(predicate).CountAsync(cancellationToken);
    }

    public async Task<TEntity?> FindOne(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Specify(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> FindOne<T>(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
       where T : class, TEntity
    {
        return (T?)await _context.Set<TEntity>().OfType<T>().Specify(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> FindOne(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            query = specifications.Aggregate(query, (current, specification) => current.Specify(specification));
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TProjection?> FindOne<TProjection>(Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Specify(specification).Select(projection).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TProjection?> FindOne<TProjection>(Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            query = specifications.Aggregate(query, (current, specification) => current.Specify(specification));
        }

        return await query.Select(projection).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> FindSingle(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Specify(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> FindSingle<T>(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        where T : class, TEntity
    {
        return (T?)await _context.Set<TEntity>().OfType<T>().Specify(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> FindSingle(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            query = specifications.Aggregate(query, (current, specification) => current.Specify(specification));
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TProjection?> FindSingle<TProjection>(Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Specify(specification).Select(projection).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> Find(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Specify(specification).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> Find<T>(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
       where T : class, TEntity
    {
        return (IEnumerable<T>)await _context.Set<TEntity>().OfType<T>().Specify(specification).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> Find(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            foreach (var specification in specifications)
            {
                query = query.Specify(specification);
            }
        }

        return await query.ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> FindQueryable(ISpecification<TEntity>? specification = null)
    {
        return _context.Set<TEntity>().Specify(specification);
    }

    public async Task<IEnumerable<TProjection>> Find<TProjection>(Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().Specify(specification).Select(projection);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TProjection>> Find<TProjection>(Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            foreach (var specification in specifications)
            {
                query = query.Specify(specification);
            }
        }

        return await query.Select(projection).ToListAsync(cancellationToken);
    }

    public async Task<IDictionary<TKey, TEntity>> Dictionary<TKey>(Func<TEntity, TKey> selector, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return await _context.Set<TEntity>().Specify(specification).ToDictionaryAsync(selector, cancellationToken);
    }

    public async Task<IDictionary<TKey, TElement>> Dictionary<TKey, TElement>(Func<TEntity, TKey> idSelector, Func<TEntity, TElement> elementSelector, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        return await _context.Set<TEntity>().Specify(specification).ToDictionaryAsync(idSelector, elementSelector, cancellationToken);
    }

    public async Task<IDictionary<TKey, TProjection>> Dictionary<TKey, TProjection>(Func<TProjection, TKey> selector, Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var query = _context.Set<TEntity>().Specify(specification).Select(projection);

        return await query.ToDictionaryAsync(selector, cancellationToken);
    }

    public async Task<IDictionary<TKey, TElement>> Dictionary<TKey, TProjection, TElement>(Func<TProjection, TKey> idSelector, Func<TProjection, TElement> elementSelector, Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var query = _context.Set<TEntity>().Specify(specification).Select(projection);

        return await query.ToDictionaryAsync(idSelector, elementSelector, cancellationToken);
    }

    public async Task<IDictionary<TKey, TProjection>> Dictionary<TKey, TProjection>(Func<TProjection, TKey> selector, Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            query = specifications.Aggregate(query, (current, specification) => current.Specify(specification));
        }

        return await query.Select(projection).ToDictionaryAsync(selector, cancellationToken);
    }

    public async Task<IDictionary<TKey, TElement>> Dictionary<TKey, TProjection, TElement>(Func<TProjection, TKey> idSelector, Func<TProjection, TElement> elementSelector, Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (specifications != null)
        {
            query = specifications.Aggregate(query, (current, specification) => current.Specify(specification));
        }

        return await query.Select(projection).ToDictionaryAsync(idSelector, elementSelector, cancellationToken);
    }

    public async ValueTask<TEntity?> FindById(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}