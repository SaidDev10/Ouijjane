using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using Ouijjane.Shared.Application.Wrapper;
using Ouijjane.Village.Domain.Entities;
using Ouijjane.Village.Domain.Specifications;
using System.Linq.Expressions;

namespace Ouijjane.Village.Application.Features.Inhabitants.Queries;

public record GetAllPagedInhabitantsQuery : IRequest<PaginatedResult<GetAllPagedInhabitantsResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchString { get; set; }
    public string? SortColumn { get; set; } 
    public string? SortOrder { get; set; } // [asc|desc]

}

internal class GetAllPagedInhabitantsQueryHandler : IRequestHandler<GetAllPagedInhabitantsQuery, PaginatedResult<GetAllPagedInhabitantsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllPagedInhabitantsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<GetAllPagedInhabitantsResponse>> Handle(GetAllPagedInhabitantsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Inhabitant, GetAllPagedInhabitantsResponse>> expression = e => e.Adapt<GetAllPagedInhabitantsResponse>();

        var inhabitants = await _unitOfWork.Repository<Inhabitant>()
                   .FindQueryable(new FindAllInhabitantsSpecification()
                                    .ApplyFilter(request.SearchString!)
                                    .ApplyPagination(request.PageNumber, request.PageSize)
                                    .ApplyOrder(request.SortColumn!, request.SortOrder!))
                   .Select(expression)
                   .ToListAsync(cancellationToken);

        return PaginatedResult<GetAllPagedInhabitantsResponse>.Success(inhabitants, inhabitants.Count, request.PageNumber, request.PageSize);
    }
}

public record GetAllPagedInhabitantsResponse
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateOnly Birthdate { get; set; }
    public bool IsMarried { get; set; }
}

public class GetAllPagedInhabitantsQueryValidator : AbstractValidator<GetAllPagedInhabitantsQuery>
{
    public GetAllPagedInhabitantsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.SortColumn)
            .NotEmpty()
            .WithMessage("Both SortColumn and SortOrder must be set or neither of them.")
            .When(x => !string.IsNullOrEmpty(x?.SortOrder));

        RuleFor(x => x.SortOrder)
            .NotEmpty()
            .WithMessage("Both SortColumn and SortOrder must be set or neither of them.")
            .When(x => !string.IsNullOrEmpty(x?.SortColumn));

        RuleFor(x => x.SortOrder)
            .Must(sortOrder => sortOrder == "asc" || sortOrder == "desc")
            .WithMessage("SortOrder must be 'asc' or 'desc'.")
            .When(x => !string.IsNullOrEmpty(x?.SortOrder));
    }
}