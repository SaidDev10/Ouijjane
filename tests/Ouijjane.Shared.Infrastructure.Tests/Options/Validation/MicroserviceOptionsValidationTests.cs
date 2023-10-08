using Ouijjane.Shared.Infrastructure.Options;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Options.Validation;
public class MicroserviceOptionsValidationTests
{    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenProductIsInvalid_ShouldReturnResult(string product)
    {
        // Arrange
        var settings = new MicroserviceOptions
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
        Assert.Contains(nameof(MicroserviceOptions.Product), validationResults.SelectMany(r => r.MemberNames));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenModuleIsInvalid_ShouldReturnResult(string module)
    {
        // Arrange
        var settings = new MicroserviceOptions
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
        Assert.Contains(nameof(MicroserviceOptions.Module), validationResults.SelectMany(r => r.MemberNames));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenComponentIsInvalid_ShouldReturnResult(string component)
    {
        // Arrange
        var settings = new MicroserviceOptions
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
        Assert.Contains(nameof(MicroserviceOptions.Component), validationResults.SelectMany(r => r.MemberNames));
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenNamespaceIsInvalid_ShouldReturnResult(string @namespace)
    {
        // Arrange
        var settings = new MicroserviceOptions
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
        Assert.Contains(nameof(MicroserviceOptions.Namespace), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenVersionIsInvalid_ShouldReturnResult(string version)
    {
        // Arrange
        var settings = new MicroserviceOptions
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
        Assert.Contains(nameof(MicroserviceOptions.Version), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenAllIsInvalid_ShouldReturnResult(string invalidValue)
    {
        // Arrange
        var settings = new MicroserviceOptions
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
        Assert.Contains(nameof(MicroserviceOptions.Product), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceOptions.Module), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceOptions.Component), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceOptions.Namespace), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(MicroserviceOptions.Version), validationResults.SelectMany(r => r.MemberNames));
    }

}
