using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Options
{
    public class SwaggerOptions : IOptionsRoot, IValidatableObject
    {
        public bool Enabled { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IncludeXmlComments { get; set; } = false;
        public string XmlFile { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Enabled)
            {
                if (string.IsNullOrEmpty(Title))
                {
                    yield return new ValidationResult($"{nameof(SwaggerOptions)}.{nameof(Title)} is not configured", new[] { nameof(Title) }); //TODO: localisation
                }

                if (IncludeXmlComments)
                {
                    if (string.IsNullOrEmpty(XmlFile))
                    {
                        yield return new ValidationResult($"{nameof(SwaggerOptions)}.{nameof(XmlFile)} is not configured", new[] { nameof(XmlFile) }); //TODO: localisation
                    }
                }
            }
        }
    }
}
