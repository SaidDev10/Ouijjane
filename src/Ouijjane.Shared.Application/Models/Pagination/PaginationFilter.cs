namespace Ouijjane.Shared.Application.Models.Pagination;

public record PaginationFilter
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Keyword { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; } // [asc|desc]

    //public PaginationFilter(int? pageNumber, int? pageSize, string? searchString, string? sortColumn, string? sortOrder)
    //{
    //    PageNumber = (!pageNumber.HasValue || pageNumber.Value <= 0) ? 1 : pageNumber.Value;
    //    PageSize = (!pageSize.HasValue || pageSize.Value <= 0)? 10 : pageSize.Value;
    //    SearchString = searchString ?? string.Empty;
    //    SortColumn = sortColumn ?? string.Empty;
    //    SortOrder = sortOrder ?? "asc";
    //}
}