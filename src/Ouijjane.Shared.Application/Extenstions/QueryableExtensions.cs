using Microsoft.EntityFrameworkCore;
using Ouijjane.Shared.Application.Models.Pagination;
using Ouijjane.Shared.Application.Specifications;

namespace Ouijjane.Shared.Application.Extenstions;
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

        //if (specification.SplitQuery)
        //{
        //    query = query.AsSplitQuery();
        //}

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
            query = specification.Skip > 0
                ? query.Skip(specification.Skip).Take(specification.Take)
                : query.Take(specification.Take);
        }

        if (specification.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }

    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, PaginationFilter filter) where T : class
    {
        if (source == null) throw new Exception();

        int pageNumber = (!filter.PageNumber.HasValue || filter.PageNumber.Value <= 0) ? 1 : filter.PageNumber.Value;
        int pageSize = (!filter.PageSize.HasValue || filter.PageSize.Value <= 0) ? 10 : filter.PageSize.Value;

        var items = await source.ToListAsync();
        var count = await source.CountAsync();

        return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
    }
}