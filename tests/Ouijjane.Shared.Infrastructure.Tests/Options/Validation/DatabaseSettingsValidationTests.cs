using Ouijjane.Shared.Infrastructure.Options;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Options.Validation;
public class DatabaseOptionsValidationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenConnectionStringIsInvalid_ShouldReturnResult(string connectionString)
    {
        // Arrange
        var settings = new DatabaseOptions
        {
            DBProvider = "dbProvider",
            ConnectionString = connectionString
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(DatabaseOptions.ConnectionString), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenDBProviderIsInvalid_ShouldReturnResult(string dbProvider)
    {
        // Arrange
        var settings = new DatabaseOptions
        {
            DBProvider = dbProvider,
            ConnectionString = "connectionString"
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(DatabaseOptions.DBProvider), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenAllAreInvalid_ShouldReturnAllResults(string invalidValue)
    {
        // Arrange
        var settings = new DatabaseOptions
        {
            DBProvider = invalidValue,
            ConnectionString = invalidValue
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults, true);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(2, validationResults.Count);

        var invalidMembers = validationResults.SelectMany(r => r.MemberNames).ToList();
        Assert.Contains(nameof(DatabaseOptions.ConnectionString), invalidMembers);
        Assert.Contains(nameof(DatabaseOptions.DBProvider), invalidMembers);
    }
}
