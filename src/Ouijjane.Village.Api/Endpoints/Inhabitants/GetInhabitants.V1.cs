namespace Ouijjane.Village.Api.Endpoints.Inhabitants;

public class GetInhabitantsV1(ISender sender) : GetInhabitants(sender)
{
    public override void Configure()
    {
        base.Configure();
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        GetAllPagedInhabitantsQuery query = new();
        var result = await _sender.Send(query, cancellationToken);
        await SendAsync(result, cancellation: cancellationToken);
    }
}