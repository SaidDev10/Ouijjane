using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ouijjane.Shared.Infrastructure.Extensions.Exceptions;
public static class ExceptionExtensions
{
    public static IServiceCollection AddExceptionMiddleware(this IServiceCollection services)
    {
        return services.AddScoped<ExceptionMiddleware>();
    }

    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
