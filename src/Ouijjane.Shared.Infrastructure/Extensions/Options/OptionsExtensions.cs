﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Exceptions;
using Ouijjane.Shared.Infrastructure.Options;

namespace Ouijjane.Shared.Infrastructure.Extensions.Options;

public static class OptionsExtensions
{
    public static T LoadOptions<T>(this IConfiguration configuration, string sectionName) where T : IOptionsRoot
    {
        var options = configuration.GetSection(sectionName).Get<T>() ?? throw new ConfigurationMissingException(sectionName);
        return options;
    }

    public static T BindValidateReturn<T>(this IServiceCollection services, IConfiguration configuration) where T : class, IOptionsRoot
    {
        services.AddOptions<T>()
            .BindConfiguration(typeof(T).Name)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        return configuration.LoadOptions<T>(typeof(T).Name);
    }

    public static void BindValidate<T>(this IServiceCollection services) where T : class, IOptionsRoot
    {
        services.AddOptions<T>()
            .BindConfiguration(typeof(T).Name)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
