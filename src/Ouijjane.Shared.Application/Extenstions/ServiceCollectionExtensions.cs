using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Behaviours;
using Ouijjane.Shared.Application.Configurations;

namespace Ouijjane.Shared.Application.Extenstions;
public static class ServiceCollectionExtensions
{
    public static void AddSharedApplicationServices(this IServiceCollection services, IConfiguration configuration, Type type)
    {
        services.AddMediator(type);
        services.AddMicroServiceConfiguration(configuration);
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

    private static void AddMicroServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string sectionName = "MicroService";

        services.Configure<MicroServiceConfiguration>(configuration.GetSection(sectionName));
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
