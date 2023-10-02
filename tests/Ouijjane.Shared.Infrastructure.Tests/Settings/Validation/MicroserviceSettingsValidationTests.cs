using Ouijjane.Shared.Infrastructure.Settings;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Settings.Validation;
public class MicroserviceSettingsValidationTests
{    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenProductIsInvalid_ShouldReturnResult(string product)
    {
        // Arrange
        var settings = new MicroserviceSettings
        {
            Product = product,
            Module = "village",
            Component = "api",
            Namespace = "local",
            Version = "1.0.0",
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(MicroserviceSettings.Product), validationResults.SelectMany(r => r.MemberNames));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenModuleIsInvalid_ShouldReturnResult(string module)
    {
        // Arrange
        var settings = new MicroserviceSettings
        {
            Product = "ouijjane",
            Module = module,
            Component = "api",
            Namespace = "local",
            Version = "1.0.0",
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(MicroserviceSettings.Module), validationResults.SelectMany(r => r.MemberNames));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenComponentIsInvalid_ShouldReturnResult(string component)
    {
        // Arrange
        var settings = new MicroserviceSettings
        {
            Product = "ouijjane",
            Module = "village",
            Component = component,
            Namespace = "local",
            Version = "1.0.0",
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(MicroserviceSettings.Component), validationResults.SelectMany(r => r.MemberNames));
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenNamespaceIsInvalid_ShouldReturnResult(string @namespace)
    {
        // Arrange
        var settings = new MicroserviceSettings
        {
            Product = "ouijjane",
            Module = "village",
            Component = "api",
            Namespace = @namespace,
            Version = "1.0.0",
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(MicroserviceSettings.Namespace), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenVersionIsInvalid_ShouldReturnResult(string version)
    {
        // Arrange
        var settings = new MicroserviceSettings
        {
            Product = "ouijjane",
            Module = "village",
            Component = "api",
            Namespace = "local",
            Version = version,
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(MicroserviceSettings.Version), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenAllIsInvalid_ShouldReturnResult(string invalidValue)
    {
        // Arrange
        var settings = new MicroserviceSettings
        {
            Product = invalidValue,
            Module = invalidValue,
            Component = invalidValue,
            Namespace = invalidValue,
            Version = invalidValue,
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert

        Assert.NotEmpty(validationResults);
        Assert.Equal(5, validationResults.Count);

        var invalidMembers = validationResults.SelectMany(r => r.MemberNames).ToList();
        Assert.Contains(nameof(MicroserviceSettings.Product), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceSettings.Module), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceSettings.Component), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceSettings.Namespace), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceSettings.Version), validationResults.SelectMany(r => r.MemberNames));
    }

}
