using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Settings;
public class MicroserviceSettings : IValidatableObject
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
            yield return new ValidationResult($"{nameof(MicroserviceSettings)}.{nameof(Product)} is not configured", new[] { nameof(Product) });
        }

        if (string.IsNullOrEmpty(Module))
        {
            yield return new ValidationResult($"{nameof(MicroserviceSettings)}.{nameof(Module)} is not configured", new[] { nameof(Module) });
        }

        if (string.IsNullOrEmpty(Component))
        {
            yield return new ValidationResult($"{nameof(MicroserviceSettings)}.{nameof(Component)} is not configured", new[] { nameof(Component) });
        }

        if (string.IsNullOrEmpty(Version))
        {
            yield return new ValidationResult($"{nameof(MicroserviceSettings)}.{nameof(Version)} is not configured", new[] { nameof(Version) });
        }

        if (string.IsNullOrEmpty(Namespace))
        {
            yield return new ValidationResult($"{nameof(MicroserviceSettings)}.{nameof(Namespace)} is not configured", new[] { nameof(Namespace) });
        }
    }
}
