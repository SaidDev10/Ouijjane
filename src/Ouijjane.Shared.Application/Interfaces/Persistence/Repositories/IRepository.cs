using Ouijjane.Shared.Application.Specifications;
using System.Linq.Expressions;

namespace Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
public interface IRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity?> FindById(int id, CancellationToken cancellationToken = default);

    Task<TEntity?> FindOne(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    Task<T?> FindOne<T>(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default) where T : class, TEntity;

    Task<TEntity?> FindOne(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default);

    Task<TProjection?> FindOne<TProjection>(Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    Task<TProjection?> FindOne<TProjection>(Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default);

    Task<TEntity?> FindSingle(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    Task<TEntity?> FindSingle(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default);

    Task<T?> FindSingle<T>(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default) where T : class, TEntity;

    Task<TProjection?> FindSingle<TProjection>(Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    IQueryable<TEntity> FindQueryable(ISpecification<TEntity>? specification = null);

    Task<IEnumerable<TEntity>> Find(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> Find<T>(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default) where T : class, TEntity;

    Task<IEnumerable<TEntity>> Find(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<TProjection>> Find<TProjection>(Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<TProjection>> Find<TProjection>(Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default);

    Task<IDictionary<TKey, TEntity>> Dictionary<TKey>(Func<TEntity, TKey> selector, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default) where TKey : notnull;

    Task<IDictionary<TKey, TProjection>> Dictionary<TKey, TProjection>(Func<TProjection, TKey> selector, Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default) where TKey : notnull;

    Task<IDictionary<TKey, TProjection>> Dictionary<TKey, TProjection>(Func<TProjection, TKey> selector, Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default) where TKey : notnull;

    Task<IDictionary<TKey, TElement>> Dictionary<TKey, TElement>(Func<TEntity, TKey> idSelector, Func<TEntity, TElement> elementSelector, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default) where TKey : notnull;

    Task<IDictionary<TKey, TElement>> Dictionary<TKey, TProjection, TElement>(Func<TProjection, TKey> idSelector, Func<TProjection, TElement> elementSelector, Expression<Func<TEntity, TProjection>> projection, ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default) where TKey : notnull;

    Task<IDictionary<TKey, TElement>> Dictionary<TKey, TProjection, TElement>(Func<TProjection, TKey> idSelector, Func<TProjection, TElement> elementSelector, Expression<Func<TEntity, TProjection>> projection, IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default) where TKey : notnull;

    Task Add(TEntity entity, CancellationToken cancellationToken = default);

    Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    Task<bool> Contains(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    Task<bool> Contains(IEnumerable<ISpecification<TEntity>?>? specifications = null, CancellationToken cancellationToken = default);

    Task<bool> Contains(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<int> Count(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);

    Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<int> Count(IEnumerable<ISpecification<TEntity>?> specifications, CancellationToken cancellationToken = default);
}