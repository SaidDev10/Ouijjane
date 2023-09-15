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
            AddCriteria(x => x.FirstName!.Contains(searchString)
                          || x.LastName!.Contains(searchString)
                          || x.FatherName!.Contains(searchString)
                          || x.Email!.Contains(searchString)
                          || x.Phone!.Contains(searchString)
                          || x.Address!.Contains(searchString)
                          || x.Birthdate!.ToShortDateString().Contains(searchString)); // => throws exception "Linq Cannot Translate ToShortDateString()"

            //AddCriteria(x => !string.IsNullOrEmpty(x.FirstName) && x.FirstName.Contains(searchString)
            //              || !string.IsNullOrEmpty(x.LastName) && x.LastName.Contains(searchString)
            //              || !string.IsNullOrEmpty(x.FatherName) && x.FatherName.Contains(searchString)
            //              || !string.IsNullOrEmpty(x.Email) && x.Email.Contains(searchString)
            //              || !string.IsNullOrEmpty(x.Phone) && x.Phone.Contains(searchString)
            //              || !string.IsNullOrEmpty(x.Address) && x.Address.Contains(searchString)
            //              || x.Birthdate.ToShortDateString().Contains(searchString) // => throws exception "Linq Cannot Translate ToShortDateString()"
            //               );
        }

        return this;
    }

    public FindAllInhabitantsSpecification ApplyPagination(int skip, int take)
    {
        ApplyPaging(skip, take);

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
