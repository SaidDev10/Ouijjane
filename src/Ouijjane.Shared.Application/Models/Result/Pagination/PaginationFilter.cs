using FluentValidation;

namespace Ouijjane.Shared.Application.Models.Result.Pagination;

public record PaginationFilter
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public string? Keyword { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; } // [asc|desc]
}

public class PaginationFilterValidator : AbstractValidator<PaginationFilter>
{
    public PaginationFilterValidator()
    {
        RuleFor(x => x.SortOrder)
            .Must(sortOrder => sortOrder == "asc" || sortOrder == "desc")
            .WithMessage("SortOrder must be 'asc' or 'desc'.")
            .When(x => !string.IsNullOrEmpty(x?.SortOrder));
    }
}