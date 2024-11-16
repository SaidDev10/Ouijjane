namespace Ouijjane.Village.Api.Endpoints.Inhabitants;

public abstract class GetInhabitants : EndpointWithoutRequest<PaginatedResult<GetAllPagedInhabitantsResponse>>
{
    protected readonly ISender _sender;

    public GetInhabitants(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("/inhabitants");
    }
}