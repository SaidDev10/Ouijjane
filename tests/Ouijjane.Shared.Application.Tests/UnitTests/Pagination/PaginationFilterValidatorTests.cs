using FluentAssertions;
using FluentValidation;
using Ouijjane.Shared.Application.Models.Result.Pagination;

namespace Ouijjane.Shared.Application.Tests.UnitTests.Pagination;
public class PaginationFilterValidatorTests
{
    private readonly PaginationFilterValidator _paginationFilterValidator;

    public PaginationFilterValidatorTests()
    {
        _paginationFilterValidator = new PaginationFilterValidator();
    }

    #region SortOrder
    [Fact]
    public async Task WhenSortOrderIsNull_ShouldNotThrowValidationException()
    {
        //Arrange
        var paginationFilter = new PaginationFilter { SortOrder = null };

        //Act
        var act = () => _paginationFilterValidator.ValidateAndThrowAsync(paginationFilter);

        //Assert
        await act.Should().NotThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenSortOrderEqualsAsc_ShouldNotThrowValidationException()
    {
        //Arrange
        var paginationFilter = new PaginationFilter { SortOrder = "asc" };

        //Act
        var act = () => _paginationFilterValidator.ValidateAndThrowAsync(paginationFilter);

        //Assert
        await act.Should().NotThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenSortOrderEqualsDesc_ShouldNotThrowValidationException()
    {
        //Arrange
        var paginationFilter = new PaginationFilter { SortOrder = "desc" };

        //Act
        var act = () => _paginationFilterValidator.ValidateAndThrowAsync(paginationFilter);

        //Assert
        await act.Should().NotThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task WhenSortOrderIsDifferentThanAscAndDesc_ShouldThrowValidationException()
    {
        //Arrange
        var paginationFilter = new PaginationFilter { SortOrder = "invalidValue" };

        //Act
        var act = () => _paginationFilterValidator.ValidateAndThrowAsync(paginationFilter);

        //Assert
        var result = await act.Should().ThrowAsync<ValidationException>();
        var error = result.And.Errors.First();
        error.PropertyName.Should().Be(nameof(PaginationFilter.SortOrder));
        error.ErrorMessage.Should().Be("SortOrder must be 'asc' or 'desc'.");
    }
    #endregion SortOrder
}
