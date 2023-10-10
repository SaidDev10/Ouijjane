using Microsoft.AspNetCore.Builder;
using Ouijjane.Shared.Infrastructure.Extensions.Options;
using Ouijjane.Shared.Infrastructure.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;

namespace Ouijjane.Shared.Infrastructure.Extensions.Logging;

public static class SerilogExtensions
{
    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        var envName = builder.Environment.EnvironmentName;
        var config = builder.Configuration;
        var appOptions = builder.Services.BindValidateReturn<AppOptions>(config);
        var elasticOptions = builder.Services.BindValidateReturn<ElasticOptions>(config);
        var serilogOptions = builder.Services.BindValidateReturn<SerilogOptions>(config);

        builder.Host.UseSerilog((_, _, serilogConfig) =>
        {
            ConfigureEnrichers(serilogConfig, serilogOptions.EnableErichers, appOptions.Name);

            ConfigureElastic(serilogConfig, elasticOptions, appOptions.Name, envName);

            ConfigureConsoleLogging(serilogConfig, serilogOptions.StructuredConsoleLogging);

            SetMinimumLogLevel(serilogConfig, serilogOptions.MinimumLogLevel);

            OverideMinimumLogLevel(serilogConfig, serilogOptions.OverrideMinimumLogLevel);
        });

        return builder;
    }

    private static void ConfigureElastic(LoggerConfiguration serilogConfig, ElasticOptions elasticOptions, string appName, string envName)
    {
        if (elasticOptions.EnableElasticSearch)
        {
            envName = envName.Replace(".", "-");

            serilogConfig.WriteTo.Async(wt => wt.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(elasticOptions.ElasticSearchUrl))
                {
                    IndexFormat = $"{appName}-logs-{envName}-{DateTime.UtcNow:yyyy-MM}",
                    AutoRegisterTemplate = true,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                }));
        }
    }

    private static void ConfigureEnrichers(LoggerConfiguration config, bool enableErichers, string appName)
    {
        if (enableErichers)
        {
            config
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", appName)
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId();
        }
    }

    private static void ConfigureConsoleLogging(LoggerConfiguration serilogConfig, bool structuredConsoleLogging)
    {
        if (structuredConsoleLogging)
        {
            serilogConfig.WriteTo.Async(wt => wt.Console(new CompactJsonFormatter()));
        }
        else
        {
            serilogConfig.WriteTo.Async(wt => wt.Console());
        }
    }

    private static void SetMinimumLogLevel(LoggerConfiguration serilogConfig, string minLogLevel)
    {
        var loggingLevelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = minLogLevel.ToLower() switch
            {
                "debug" => LogEventLevel.Debug,
                "information" => LogEventLevel.Information,
                "warning" => LogEventLevel.Warning,
                _ => LogEventLevel.Information,
            }
        };
        serilogConfig.MinimumLevel.ControlledBy(loggingLevelSwitch);
    }

    private static void OverideMinimumLogLevel(LoggerConfiguration serilogConfig, bool overrideMinimumLogLevel)
    {
        if (overrideMinimumLogLevel)
        {
            serilogConfig
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                         .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                         .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Internal.WebHost", LogEventLevel.Warning)
                         .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                         .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);
            //.MinimumLevel.Override("Hangfire", LogEventLevel.Warning)
            //.MinimumLevel.Override("OpenIddict.Validation", LogEventLevel.Error)
            //.MinimumLevel.Override("System.Net.Http.HttpClient.OpenIddict", LogEventLevel.Error);
        }
    }
}
