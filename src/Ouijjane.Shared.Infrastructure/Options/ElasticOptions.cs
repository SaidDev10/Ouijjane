using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options;
public class ElasticOptions : IOptionsRoot, IValidatableObject
{
    public bool EnableElasticSearch { get; set; } = false;
    public string ElasticSearchUrl { get; set; } = string.Empty;
    public bool EnableApm { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EnableElasticSearch)
        {
            if (string.IsNullOrEmpty(ElasticSearchUrl))
            {
                yield return new ValidationResult($"{nameof(SwaggerOptions)}.{nameof(ElasticSearchUrl)} is not configured", new[] { nameof(ElasticSearchUrl) }); //TODO: localisation
            }
            else if (!Uri.IsWellFormedUriString(ElasticSearchUrl, UriKind.Absolute))
            {
                yield return new ValidationResult($"{nameof(SwaggerOptions)}.{nameof(ElasticSearchUrl)} is not a valid url", new[] { nameof(ElasticSearchUrl) }); 
            }
        }
    }
}
