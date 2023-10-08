namespace Ouijjane.Village.Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string optionsDirectory = "Options";
        var env = builder.Environment;
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/cache.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/cache.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/cors.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/cors.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{optionsDirectory}/database.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{optionsDirectory}/database.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/hangfire.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/hangfire.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/mail.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/mail.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{optionsDirectory}/microservice.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{optionsDirectory}/microservice.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/middleware.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/middleware.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/localization.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/localization.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/logger.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/logger.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/openapi.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/openapi.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/security.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/security.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/securityheaders.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/securityheaders.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/signalr.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"{optionsDirectory}/signalr.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{optionsDirectory}/swagger.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{optionsDirectory}/swagger.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        return builder;
    }
}
