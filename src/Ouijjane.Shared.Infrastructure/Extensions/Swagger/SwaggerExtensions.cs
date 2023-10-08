using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ouijjane.Shared.Infrastructure.Extensions.Options;
using Ouijjane.Shared.Infrastructure.Extensions.Swagger;
using Ouijjane.Shared.Infrastructure.Extensions.Swagger.OpenApi;
using Ouijjane.Shared.Infrastructure.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Ouijjane.Shared.Infrastructure.Extensions.Swagger;
public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerOptions = services.BindValidateReturn<SwaggerOptions>(configuration);

        if (!swaggerOptions.Enabled)
        {
            return;
        }

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(config =>
        {
            config.OperationFilter<SwaggerDefaultValues>();

            config.CustomSchemaIds(type => type.FullName!.Replace("+", "_"));
            //config.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" }); ??? what for?

            config.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme } },
                    Array.Empty<string>()
                }});

            config.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Input your Bearer token to access this API", //TODO: locolization
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
            });

            if (swaggerOptions.IncludeXmlComments)
            {
                var xmlFile = swaggerOptions.XmlFile;
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                config.IncludeXmlComments(xmlPath);
            }
        });
    }

    public static void UseSwagger(this WebApplication app)
    {
        var env = app.Environment;

        if (!env.IsProduction())
        {
            var swaggerOptions = app.Configuration.LoadOptions<SwaggerOptions>(nameof(SwaggerOptions));
            
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{httpReq.PathBase.Value}" } });
            });
            app.UseSwaggerUI(config =>
            {
                var descriptions = app.DescribeApiVersions();

                foreach (var description in descriptions) 
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant() + " - " + swaggerOptions.Title;
                    config.SwaggerEndpoint(url, name);
                }
                config.RoutePrefix = "swagger";
                config.DefaultModelsExpandDepth(-1);
                config.DocExpansion(DocExpansion.List);
                config.DisplayRequestDuration();
            });
        }
    }
}
