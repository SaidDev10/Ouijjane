using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Extenstions;
using Ouijjane.Village.Application.Features.Inhabitants.Queries;

namespace Ouijjane.Village.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSharedApplicationServices(typeof(GetAllPagedInhabitantsQueryValidator));
    }
}
