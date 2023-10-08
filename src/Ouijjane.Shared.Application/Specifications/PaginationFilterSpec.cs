using Ouijjane.Shared.Application.Models.Result.Pagination;
using Ouijjane.Shared.Domain.Entities;
using System.Linq.Expressions;

namespace Ouijjane.Shared.Application.Specifications;

public abstract class PaginationFilterSpec<T> : BaseSpecification<T> where T : Entity
{
    public PaginationFilterSpec(PaginationFilter filter)
    {
        ApplyFilter(filter.Keyword);
        ApplyPagination(filter);
        ApplyOrder(filter);
        ApplyReadOnly();
    }

    private PaginationFilterSpec<T> ApplyPagination(PaginationFilter filter)
    {
        int pageNumber = (!filter.PageNumber.HasValue || filter.PageNumber.Value <= 0) ? 1 : filter.PageNumber.Value;
        int pageSize = (!filter.PageSize.HasValue || filter.PageSize.Value <= 0)? 10 : filter.PageSize.Value;

        ApplyPaging(pageNumber, pageSize);

        return this;
    }

    private PaginationFilterSpec<T> ApplyOrder(PaginationFilter filter)
    {
        var sortOrder = string.IsNullOrEmpty(filter.SortOrder) ? OrderContants.ASCENDING : filter.SortOrder;

        if (sortOrder == OrderContants.DESCENDING)
        {
            ApplyOrderByDescending(GetSortProperty(filter.SortColumn));
        }
        else
        {
            ApplyOrderBy(GetSortProperty(filter.SortColumn));
        }

        return this;
    }

    protected abstract Expression<Func<T, object>> GetSortProperty(string? sortColumn);

    protected abstract PaginationFilterSpec<T> ApplyFilter(string? keyword);

}