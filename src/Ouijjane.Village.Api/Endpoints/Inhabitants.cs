using Asp.Versioning.Conventions;
using Carter;
using MediatR;
using Ouijjane.Village.Application.Features.Inhabitants.Queries;

namespace Ouijjane.Village.Api.Endpoints
{
    public class Inhabitants : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var versionSet = app.NewApiVersionSet()
                                .HasApiVersion(1)
                                .HasApiVersion(2)
                                .Build();

            var group = app.MapGroup("api/v{version:apiVersion}/inhabitants")
                           .WithApiVersionSet(versionSet);
                           //.RequireAuthorization();



            group.MapGet("", async (ISender sender, [AsParameters] GetAllPagedInhabitantsQuery query) =>
            {
                var result = await sender.Send(query);
                return Results.Ok(result);
            }).MapToApiVersion(1)
              .MapToApiVersion(2);
        }
    }
}
