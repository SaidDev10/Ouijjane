﻿using Carter;

namespace Ouijjane.Village.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddWebServices(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddSwagger();

        services.AddCarter();
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
