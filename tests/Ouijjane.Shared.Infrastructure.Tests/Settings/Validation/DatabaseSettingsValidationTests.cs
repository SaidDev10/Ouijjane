using Ouijjane.Shared.Infrastructure.Settings;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Settings.Validation;
public class DatabaseSettingsValidationTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenConnectionStringIsInvalid_ShouldReturnResult(string connectionString)
    {
        // Arrange
        var settings = new DatabaseSettings
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
        Assert.Contains(nameof(DatabaseSettings.ConnectionString), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenDBProviderIsInvalid_ShouldReturnResult(string dbProvider)
    {
        // Arrange
        var settings = new DatabaseSettings
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
        Assert.Contains(nameof(DatabaseSettings.DBProvider), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenAllAreInvalid_ShouldReturnAllResults(string invalidValue)
    {
        // Arrange
        var settings = new DatabaseSettings
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
        Assert.Contains(nameof(DatabaseSettings.ConnectionString), invalidMembers);
        Assert.Contains(nameof(DatabaseSettings.DBProvider), invalidMembers);
    }
}
