using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;
public class DatabaseOptions : IOptionsRoot, IValidatableObject
{
    public string DBProvider { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(DBProvider))
        {
            yield return new ValidationResult($"{nameof(DatabaseOptions)}.{nameof(DBProvider)} is not configured", new[] { nameof(DBProvider) }); //TODO: localisation
        }

        if (string.IsNullOrEmpty(ConnectionString))
        {
            yield return new ValidationResult($"{nameof(DatabaseOptions)}.{nameof(ConnectionString)} is not configured", new[] { nameof(ConnectionString) }); //TODO: localisation
        }
    }
}