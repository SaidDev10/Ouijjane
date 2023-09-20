using Carter;
using MediatR;
using Ouijjane.Village.Application.Features.Inhabitants.Queries;

namespace Ouijjane.Village.WebApi.Endpoints
{
    public class Inhabitants : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/inhabitants");
                           //.RequireAuthorization();

            group.MapGet("", async (ISender sender, [AsParameters] GetAllPagedInhabitantsQuery query) =>
            {
                var result = await sender.Send(query);
                return Results.Ok(result);
            });
        }
    }
}
