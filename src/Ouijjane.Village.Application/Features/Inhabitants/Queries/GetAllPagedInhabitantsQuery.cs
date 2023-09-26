using FluentValidation;
using Mapster;
using MediatR;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using Ouijjane.Shared.Application.Models.Pagination;
using Ouijjane.Village.Application.Specifications.Inhabitants;
using Ouijjane.Village.Domain.Entities;
using Ouijjane.Shared.Application.Extenstions;
using System.Linq.Expressions;

namespace Ouijjane.Village.Application.Features.Inhabitants.Queries;

public record GetAllPagedInhabitantsQuery : PaginationFilter, IRequest<PaginatedResult<GetAllPagedInhabitantsResponse>>
{
}

public class GetAllPagedInhabitantsQueryValidator : AbstractValidator<GetAllPagedInhabitantsQuery>
{
    public GetAllPagedInhabitantsQueryValidator()
    {
        RuleFor(x => x.SortOrder)
            .Must(sortOrder => sortOrder == "asc" || sortOrder == "desc")
            .WithMessage("SortOrder must be 'asc' or 'desc'.")
            .When(x => !string.IsNullOrEmpty(x?.SortOrder));
    }
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

        return await _unitOfWork.Repository<Inhabitant>()
                   .FindQueryable(new FindAllInhabitantsWithPaginationFilterSpec(request))
                   .Select(expression)
                   .ToPaginatedListAsync(request);
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