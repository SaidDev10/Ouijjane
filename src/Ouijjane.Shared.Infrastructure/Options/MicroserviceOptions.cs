using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;
public class MicroserviceOptions : IOptionsRoot, IValidatableObject
{
    public string? Product { get; set; }

    public string? Module { get; set; }

    public string? Component { get; set; }

    public string? Version { get; set; }

    public string? Namespace { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Product))
        {
            yield return new ValidationResult($"{nameof(MicroserviceOptions)}.{nameof(Product)} is not configured", new[] { nameof(Product) }); //TODO: localisation
        }

        if (string.IsNullOrEmpty(Module))
        {
            yield return new ValidationResult($"{nameof(MicroserviceOptions)}.{nameof(Module)} is not configured", new[] { nameof(Module) }); //TODO: localisation
        }

        if (string.IsNullOrEmpty(Component))
        {
            yield return new ValidationResult($"{nameof(MicroserviceOptions)}.{nameof(Component)} is not configured", new[] { nameof(Component) }); //TODO: localisation
        }

        if (string.IsNullOrEmpty(Version))
        {
            yield return new ValidationResult($"{nameof(MicroserviceOptions)}.{nameof(Version)} is not configured", new[] { nameof(Version) }); //TODO: localisation
        }

        if (string.IsNullOrEmpty(Namespace))
        {
            yield return new ValidationResult($"{nameof(MicroserviceOptions)}.{nameof(Namespace)} is not configured", new[] { nameof(Namespace) }); //TODO: localisation
        }
    }
}
