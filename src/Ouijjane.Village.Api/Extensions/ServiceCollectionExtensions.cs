namespace Ouijjane.Village.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddVillageWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionMiddleware();
        services.AddApi();
        services.AddFastEndpoints();
        services.AddSwagger(configuration);
    }


    public static async Task UseVillageMiddlewares(this WebApplication app)
    {
        if(!app.Environment.IsProduction())
        {
            app.UseMigrationsEndPoint();
            await app.SeedAsync();
        }
        
        //TODO: Check the order
        app.UseExceptionMiddleware();

        app.UseHsts();
        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseFastEndpoints(c => c.Versioning.Prefix = "v");
        app.UseSwagger();
        app.MapPrometheusScrapingEndpoint();
    }
}


//The proper order should be: ↓

//• Exception Handler
//• HSTS
//• HttpsRedirection
//• Static Files
//• Routing
//• CORS
//• Authentication
//• Authorization
//• Custom Middlewares
//• Endpoints
