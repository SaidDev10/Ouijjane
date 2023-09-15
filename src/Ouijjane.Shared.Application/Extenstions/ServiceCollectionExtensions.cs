using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Behaviours;

namespace Ouijjane.Shared.Application.Extenstions;
public static class ServiceCollectionExtensions
{
    public static void AddSharedApplicationServices(this IServiceCollection services, Type type)
    {
        services.AddMediator(type);
    }

    private static void AddMediator(this IServiceCollection services, Type type)
    {
        services.AddValidatorsFromAssemblyContaining(type);

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining(type);
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });
    }

    //private static void AddMapster(this IServiceCollection services)
    //{
    //    //add mapper
    //    var config = TypeAdapterConfig.GlobalSettings;
    //    config.Scan(Assembly.GetExecutingAssembly());

    //    services.AddSingleton(config);
    //    services.AddScoped<IMapper, ServiceMapper>();
    //}
}
