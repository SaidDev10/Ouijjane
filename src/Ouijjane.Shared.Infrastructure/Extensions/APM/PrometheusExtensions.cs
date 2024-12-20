﻿using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;

namespace Ouijjane.Shared.Infrastructure.Extensions.APM;
internal static class PrometheusExtensions
{
    public static void AddPrometheus(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(builder =>
            {
                builder.AddPrometheusExporter();

                builder.AddMeter("Microsoft.AspNetCore.Hosting",
                                 "Microsoft.AspNetCore.Server.Kestrel");
                               //PS: add here any custom meter when configured

                builder.AddView("http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                    });
            });
    }
}
