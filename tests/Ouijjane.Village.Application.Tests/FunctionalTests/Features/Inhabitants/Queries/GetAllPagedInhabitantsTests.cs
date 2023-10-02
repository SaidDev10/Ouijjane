using Ouijjane.Village.Application.Features.Inhabitants.Queries;
using Ouijjane.Village.Application.Tests.Builders;
using static Ouijjane.Village.Application.Tests.FunctionalTestFixture;

namespace Ouijjane.Village.Application.Tests.FunctionalTests.Features.Inhabitants.Queries;

[Collection("Functional test collection")]
public class GetAllPagedInhabitantsTests : IAsyncLifetime
{
    public async Task InitializeAsync() => await ResetState();

    [Fact]
    public async Task ShouldReturnAllInhabitants()
    {
        //arrange
        //await RunAsDefaultUserAsync();
        var total = 15;
        var fakeInhabitants = new InhabitantBuilder().BuildList(total);
        await AddRangeAsync(fakeInhabitants);
        var query = new GetAllPagedInhabitantsQuery { PageSize = total };

        //act
        var result = await SendAsync(query);

        //assess
        result.CurrentPage.Should().Be(1);
        result.HasNextPage.Should().Be(false);
        result.HasPreviousPage.Should().Be(false);
        result.PageSize.Should().Be(total);
        result.TotalPages.Should().Be(1);
        result.TotalCount.Should().Be(total);
        result.Data.Should().HaveCount(total);
    }

    [Fact]
    public async Task ShouldReturnNothing()
    {
        //arrange
        //await RunAsDefaultUserAsync();
        var query = new GetAllPagedInhabitantsQuery();

        //act
        var result = await SendAsync(query);

        //assess
        result.CurrentPage.Should().Be(1);
        result.HasNextPage.Should().Be(false);
        result.HasPreviousPage.Should().Be(false);
        result.PageSize.Should().Be(10);
        result.TotalPages.Should().Be(1);
        result.TotalCount.Should().Be(0);
        result.Data.Should().HaveCount(0);
    }

    public Task DisposeAsync() => Task.CompletedTask;

}
