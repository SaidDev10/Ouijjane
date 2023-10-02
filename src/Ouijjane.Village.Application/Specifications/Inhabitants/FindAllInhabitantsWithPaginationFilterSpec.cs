using Ouijjane.Shared.Application.Models.Result.Pagination;
using Ouijjane.Shared.Application.Specifications;
using Ouijjane.Village.Domain.Entities;
using System.Linq.Expressions;

namespace Ouijjane.Village.Application.Specifications.Inhabitants;
public class FindAllInhabitantsWithPaginationFilterSpec : PaginationFilterSpec<Inhabitant>
{
    public FindAllInhabitantsWithPaginationFilterSpec(PaginationFilter paginationFilter) : base(paginationFilter)
    {
    }

    protected override FindAllInhabitantsWithPaginationFilterSpec ApplyFilter(string? keyword)
    {
        if (!string.IsNullOrEmpty(keyword))
        {
            if (DateOnly.TryParse(keyword, out var date))
            {
                AddCriteria(x => x.Birthdate == date);
            }
            else
            {
                AddCriteria(x => x.FirstName!.Contains(keyword)
                              || x.LastName!.Contains(keyword)
                              || x.FatherName!.Contains(keyword)
                              || x.Email!.Contains(keyword)
                              || x.Phone!.Contains(keyword)
                              || x.Address!.Contains(keyword));
            }
        }

        return this;
    }

    protected override Expression<Func<Inhabitant, object>> GetSortProperty(string? sortColumn)
    {
        var column = string.IsNullOrEmpty(sortColumn) ? string.Empty : sortColumn;

        return column.ToLower() switch
        {
            "firstname" => inhabitant => inhabitant.FirstName!,
            "lastname" => inhabitant => inhabitant.LastName!,
            "fathername" => inhabitant => inhabitant.FatherName!,
            "email" => inhabitant => inhabitant.Email!,
            "phone" => inhabitant => inhabitant.Phone!,
            "birthdate" => inhabitant => inhabitant.Birthdate!,
            "ismarried" => inhabitant => inhabitant.IsMarried!,

            _ => inhabitant => inhabitant.Id
        };
    }
}
