using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ouijjane.Shared.Infrastructure.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ouijjane.Shared.Infrastructure.Extensions.Swagger.OpenApi;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;
    private readonly SwaggerOptions _options;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<SwaggerOptions> options)
    {
        this.provider = provider;
        _options = options.Value;
    }

    public void Configure(SwaggerGenOptions options)
    {
        // add a swagger document for each discovered API version
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _options));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, SwaggerOptions options)
    {
        var info = new OpenApiInfo()
        {
            Title = options.Title,
            Version = description.ApiVersion.ToString(),
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}