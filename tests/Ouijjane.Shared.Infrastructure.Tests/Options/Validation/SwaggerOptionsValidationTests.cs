using Ouijjane.Shared.Infrastructure.Options;
using System.ComponentModel.DataAnnotations;

namespace Ouijjane.Shared.Infrastructure.Tests.Options.Validation;
public class SwaggerOptionsValidationTests
{
    [Theory]
    [InlineData("titleTest", false, "")]
    [InlineData("titleTest", false, null)]
    [InlineData("titleTest", false, "fileNameTest")]
    [InlineData(null, true, "fileNameTest")]
    [InlineData("", true, "fileNameTest")]
    [InlineData("titleTest", true, "fileNameTest")]
    public void WhenSwaggerIsDisabled_ShouldNotReturnResult(string title, bool includeXmlComments, string xmlFile)
    {
        // Arrange
        var settings = new SwaggerOptions
        {
            Enabled = false,
            Title = title,
            IncludeXmlComments = includeXmlComments,
            XmlFile = xmlFile
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.Empty(validationResults);
    }

    [Theory]
    [InlineData(null, true, "fileNameTest")]
    [InlineData("", true, "fileNameTest")]
    [InlineData(null, false, "")]
    [InlineData(null, false, null)]
    [InlineData("", false, "fileNameTest")]
    public void WhenSwaggerIsEnabledAndTitleInvalid_ShouldReturnResult(string title, bool includeXmlComments, string xmlFile)
    {
        // Arrange
        var settings = new SwaggerOptions
        {
            Enabled = true,
            Title = title,
            IncludeXmlComments = includeXmlComments,
            XmlFile = xmlFile
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(SwaggerOptions.Title), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData("titleTest", true, null)]
    [InlineData("titleTest", true, "")]
    public void WhenSwaggerIsEnabledAndIncludeXmlCommentsAndXmlFileInvalid_ShouldReturnResult(string title, bool includeXmlComments, string xmlFile)
    {
        // Arrange
        var settings = new SwaggerOptions
        {
            Enabled = true,
            Title = title,
            IncludeXmlComments = includeXmlComments,
            XmlFile = xmlFile
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(1, validationResults.Count);
        Assert.Contains(nameof(SwaggerOptions.XmlFile), validationResults.SelectMany(r => r.MemberNames));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData("", "")]
    public void WhenSwaggerIsEnabledAndAllInvalid_ShouldReturnResult(string title, string xmlFile)
    {
        // Arrange
        var settings = new SwaggerOptions
        {
            Enabled = true,
            Title = title,
            IncludeXmlComments = true,
            XmlFile = xmlFile
        };
        var validationContext = new ValidationContext(settings);

        // Act
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(settings, validationContext, validationResults);

        // Assert
        Assert.NotEmpty(validationResults);
        Assert.Equal(2, validationResults.Count);
        Assert.Contains(nameof(SwaggerOptions.Title), validationResults.SelectMany(r => r.MemberNames));
        Assert.Contains(nameof(SwaggerOptions.XmlFile), validationResults.SelectMany(r => r.MemberNames));
    }

}
