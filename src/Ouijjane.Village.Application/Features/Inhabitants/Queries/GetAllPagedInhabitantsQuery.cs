using FluentValidation;
using Mapster;
using MediatR;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using Ouijjane.Village.Application.Specifications.Inhabitants;
using Ouijjane.Village.Domain.Entities;
using Ouijjane.Shared.Application.Extenstions;
using System.Linq.Expressions;
using Ouijjane.Shared.Application.Models.Result.Pagination;
using Microsoft.Extensions.Logging;

namespace Ouijjane.Village.Application.Features.Inhabitants.Queries;

public record GetAllPagedInhabitantsQuery : PaginationFilter, IRequest<PaginatedResult<GetAllPagedInhabitantsResponse>>
{
}

public class GetAllPagedInhabitantsQueryValidator : AbstractValidator<GetAllPagedInhabitantsQuery>
{
    public GetAllPagedInhabitantsQueryValidator()
    {
        Include(new PaginationFilterValidator());
    }
}

internal class GetAllPagedInhabitantsQueryHandler : IRequestHandler<GetAllPagedInhabitantsQuery, PaginatedResult<GetAllPagedInhabitantsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllPagedInhabitantsQueryHandler> _logger;
    public GetAllPagedInhabitantsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllPagedInhabitantsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PaginatedResult<GetAllPagedInhabitantsResponse>> Handle(GetAllPagedInhabitantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Start] Get all paged inhabitants ");

        Expression<Func<Inhabitant, GetAllPagedInhabitantsResponse>> expression = e => e.Adapt<GetAllPagedInhabitantsResponse>();

        var result = await _unitOfWork
                                    .Repository<Inhabitant>()
                                    .FindQueryable(new FindAllInhabitantsWithPaginationFilterSpec(request))
                                    .Select(expression)
                                    .ToPaginatedListAsync(request);
        
        _logger.LogInformation("[End] Get all paged inhabitants ");
        
        return result;
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