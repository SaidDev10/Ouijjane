using Ouijjane.Shared.Infrastructure.Options;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Options.Validation;
public class ElasticOptionsValidationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid value")]
    public void WhenElasticSearchUrlIsUnset_ShouldReturnResult(string elasticSearchUrl)
    {
        // Arrange
        var settings = new ElasticOptions
        {
            EnableElasticSearch = true,
            ElasticSearchUrl = elasticSearchUrl
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(ElasticOptions.ElasticSearchUrl), validationResults.SelectMany(r => r.MemberNames));
    }
}
