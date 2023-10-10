using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;
public class SerilogOptions : IOptionsRoot, IValidatableObject
{
    public bool EnableErichers { get; set; } = true;
    public bool StructuredConsoleLogging { get; set; } = false;
    public string MinimumLogLevel { get; set; } = "Information";
    public bool OverrideMinimumLogLevel { get; set; } = true;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(MinimumLogLevel))
        {
            yield return new ValidationResult($"{nameof(SwaggerOptions)}.{nameof(MinimumLogLevel)} is not configured", new[] { nameof(MinimumLogLevel) }); //TODO: localisation
        }
        else
        {
            var minimumLogLevels = new List<string> { "Debug", "Information", "Warning", "Error" };
            if (!minimumLogLevels.Contains(MinimumLogLevel, StringComparer.OrdinalIgnoreCase))
            {
                yield return new ValidationResult($"{nameof(SwaggerOptions)}.{nameof(MinimumLogLevel)} is invalid", new[] { nameof(MinimumLogLevel) });
            }
        }
    }
}
