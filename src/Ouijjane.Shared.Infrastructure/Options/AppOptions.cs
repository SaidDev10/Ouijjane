using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;

public class AppOptions : IOptionsRoot
{
    [Required(ErrorMessage = $"{nameof(MicroserviceOptions)}.{nameof(Name)} is not configured")] //TODO: localisation
    public string Name { get; set; } = "Ouijjane";
}
