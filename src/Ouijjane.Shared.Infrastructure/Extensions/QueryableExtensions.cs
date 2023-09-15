using Microsoft.EntityFrameworkCore;
using Ouijjane.Shared.Domain.Specifications;

namespace Ouijjane.Shared.Infrastructure.Extensions;
public static class QueryableExtensions
{
    public static IQueryable<T> Specify<T>(this IQueryable<T> inputQuery, ISpecification<T>? specification) where T : class
    {
        var query = inputQuery;

        if (specification == null) return query;

        if (specification.IsReadOnly)
        {
            query = query.AsNoTracking();
        }

        foreach (var criteria in specification.Criteria)
        {
            query = query.Where(criteria);
        }

        if (specification.SplitQuery)
        {
            query = query.AsSplitQuery();
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
        else if (specification.OrderedExpression != null)
        {
            query = specification.OrderedExpression(query);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }
}