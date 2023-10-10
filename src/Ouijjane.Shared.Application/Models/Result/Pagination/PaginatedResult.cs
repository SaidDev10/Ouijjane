namespace Ouijjane.Shared.Application.Models.Result.Pagination;
public class PaginatedResult<T> : Result
{
    public PaginatedResult(List<T> data)
    {
        Data = data;
    }

    public List<T>? Data { get; set; }

    internal PaginatedResult(bool succeeded, List<T>? data = default, List<string>? messages = null, int count = 0, int pageNumber = 1, int pageSize = 10)
    {
        Data = data;
        CurrentPage = pageNumber;
        Succeeded = succeeded;
        PageSize = pageSize;
        TotalPages = count == 0 ? 1 : (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public static PaginatedResult<T> Failure(List<string> messages)
    {
        return new PaginatedResult<T>(false, default, messages);
    }

    public static PaginatedResult<T> Success(List<T> data, int count, int pageNumber, int pageSize)
    {
        return new PaginatedResult<T>(true, data, null, count, pageNumber, pageSize);
    }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }
    public int PageSize { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;
}