using Ouijjane.Village.Domain.Entities;

namespace Ouijjane.Village.Application.Tests.Builders;

public class InhabitantBuilder : BaseEntityBuilder<Inhabitant>
{
    public InhabitantBuilder()
    {
        SetDefaultRules((f, e) =>
        {
            e.FirstName = f.Name.FirstName();
            e.LastName = f.Name.LastName();
            e.FatherName = f.Name.FirstName();
            e.Address = f.Address.ToString();
            e.Email = f.Internet.Email(e.FirstName, e.LastName);
            e.Phone = f.Phone.Locale.ToString();
            e.Birthdate = f.Date.PastDateOnly(1900);
            e.IsMarried = f.Random.Bool();
        });
    }
}
