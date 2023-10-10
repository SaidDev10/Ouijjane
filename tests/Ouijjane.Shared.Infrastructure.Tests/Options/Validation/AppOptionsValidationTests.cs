using Ouijjane.Shared.Infrastructure.Options;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Options.Validation;
public class AppOptionsValidationTests
{    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenAppNameIsInvalid_ShouldReturnResult(string appName)
    {
        // Arrange
        var settings = new AppOptions
        {
            Name = appName
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(AppOptions.Name), validationResults.SelectMany(r => r.MemberNames));
    }
}
