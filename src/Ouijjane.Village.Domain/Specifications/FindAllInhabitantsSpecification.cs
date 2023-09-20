using Ouijjane.Shared.Domain.Specifications;
using Ouijjane.Village.Domain.Entities;
using System.Linq.Expressions;

namespace Ouijjane.Village.Domain.Specifications;
public class FindAllInhabitantsSpecification : BaseSpecification<Inhabitant>
{

    public FindAllInhabitantsSpecification ApplyFilter(string searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            if (DateOnly.TryParse(searchString, out var date))
            {
                AddCriteria(x => x.Birthdate == date);
            }
            else
            {
                AddCriteria(x => x.FirstName!.Contains(searchString)
                              || x.LastName!.Contains(searchString)
                              || x.FatherName!.Contains(searchString)
                              || x.Email!.Contains(searchString)
                              || x.Phone!.Contains(searchString)
                              || x.Address!.Contains(searchString));
            }
        }

        return this;
    }

    public FindAllInhabitantsSpecification ApplyPagination(int pageNumber, int pageSize)
    {
        ApplyPaging(pageNumber, pageSize);

        return this;
    }

    public FindAllInhabitantsSpecification ApplyOrder(string sortColumn, string sortOrder)
    {
        if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortOrder))
        {
            if (sortOrder == "desc")
            {
                ApplyOrderByDescending(GetSortProperty(sortColumn));
            }
            else
            {
                ApplyOrderBy(GetSortProperty(sortColumn));
            }
        }

        return this;
    }

    public FindAllInhabitantsSpecification ReadOnly()
    {
        ApplyReadOnly();
        
        return this;
    }

    private static Expression<Func<Inhabitant, object>> GetSortProperty(string sortColumn)
    {
        return sortColumn.ToLower() switch
        {
            "firstname" => inhabitant => inhabitant.FirstName!,
            "lastname" => inhabitant => inhabitant.LastName!,
            "fathername" => inhabitant => inhabitant.FatherName!,
            "email" => inhabitant => inhabitant.Email!,
            "phone" => inhabitant => inhabitant.Phone!,
            "birthdate" => inhabitant => inhabitant.Birthdate!,
            "ismarried" => inhabitant => inhabitant.IsMarried!,

            _ => inhabitant => inhabitant.FirstName!//TODO: _ => inhabitant => inhabitant.Id
        };
    }
}
