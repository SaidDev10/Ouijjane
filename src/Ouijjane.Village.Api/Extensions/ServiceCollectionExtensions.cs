using Carter;
using Ouijjane.Village.Infrastructure.Extensions;
using Ouijjane.Shared.Infrastructure.Extensions.Api;
using Ouijjane.Shared.Infrastructure.Extensions.Exceptions;
using Ouijjane.Shared.Infrastructure.Extensions.Swagger;

namespace Ouijjane.Village.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddVillageWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionMiddleware();
        services.AddApi();
        services.AddCarter();
        services.AddSwagger(configuration);
    }

    public static async Task UseVillageWebServices(this WebApplication app)
    {
        if(app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            await app.SeedAsync();
        }
        
        //TODO: Check the order
        app.UseExceptionMiddleware();

        app.UseHsts();
        app.UseHttpsRedirection();
        
        app.MapCarter();
        app.UseSwagger();
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

}
