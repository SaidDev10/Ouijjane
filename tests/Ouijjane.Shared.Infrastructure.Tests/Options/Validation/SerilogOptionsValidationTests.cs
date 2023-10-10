using Ouijjane.Shared.Infrastructure.Options;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Options.Validation;
public class SerilogOptionsValidationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid value")]
    public void WhenMinimumLogLevelIsInvalid_ShouldReturnResult(string minimumLogLevel)
    {
        // Arrange
        var settings = new SerilogOptions
        {
            EnableErichers = true,
            StructuredConsoleLogging = false,
            MinimumLogLevel = minimumLogLevel,
            OverrideMinimumLogLevel = true
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(SerilogOptions.MinimumLogLevel), validationResults.SelectMany(r => r.MemberNames));
    }
}
