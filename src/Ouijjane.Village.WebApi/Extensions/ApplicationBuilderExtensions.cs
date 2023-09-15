using Carter;
using Ouijjane.Village.Infrastructure.Extensions;

namespace Ouijjane.Village.WebApi.Extensions;
public static class ApplicationBuilderExtensions
{
    public static async Task UseWebMiddleware(this WebApplication app, IWebHostEnvironment environment)
    {
        app.UseExceptionHandling(environment);

        app.AddSwagger(environment);

        await app.AddDatabaseInitialisation(environment);

        app.UseHttpsRedirection();

        app.MapCarter();
    }

    private static void UseExceptionHandling(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
    }

    private static void AddSwagger(this WebApplication app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    private static async Task AddDatabaseInitialisation(this WebApplication app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            await app.SeedAsync();
        }
    }
}
