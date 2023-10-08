using Microsoft.AspNetCore.WebUtilities;
using Ouijjane.Shared.Application.Exceptions;
using System.Net;

namespace Ouijjane.Shared.Infrastructure.Extensions.Exceptions;

public class ExceptionDetails
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public Guid TraceId { get; set; } = Guid.NewGuid();
    public List<string>? Errors { get; private set; }
    public int? Status { get; set; }
    public string? StackTrace { get; set; }
    public string? Type { get; set; }

    internal static ExceptionDetails HandleFluentValidationException(FluentValidation.ValidationException exception)
    {
        var errorResult = new ExceptionDetails()
        {
            Title = "Validation Failed",
            Detail = "One or More Validations failed",
            Status = (int)HttpStatusCode.BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Errors = new(),
        };
        if (exception.Errors.Count() == 1)
        {
            errorResult.Detail = exception.Errors.FirstOrDefault()!.ErrorMessage;
        }
        foreach (var error in exception.Errors)
        {
            errorResult.Errors.Add(error.ErrorMessage);
        }
        return errorResult;
    }

    internal static ExceptionDetails HandleDefaultException(Exception exception)
    {
        var errorResult = new ExceptionDetails()
        {
            Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError),
            Detail = exception.Message.Trim(),
            Status = (int)HttpStatusCode.InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        };
        return errorResult;
    }

    internal static ExceptionDetails HandleNotFoundException(NotFoundException exception)
    {
        var errorResult = new ExceptionDetails()
        {
            Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
            Detail = exception.Message.Trim(),
            Status = (int)HttpStatusCode.NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
        };
        return errorResult;
    }

    internal static ExceptionDetails HandleUnauthorizedException(UnauthorizedException unauthorizedException)
    {
        return new ExceptionDetails()
        {
            Title = string.IsNullOrEmpty(unauthorizedException.Error) ? ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Unauthorized) : unauthorizedException.Error,
            Detail = string.IsNullOrEmpty(unauthorizedException.Description) ? unauthorizedException.Message.Trim() : unauthorizedException.Description,
            Status = (int)HttpStatusCode.Unauthorized,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };
    }

    internal static ExceptionDetails HandleForbiddenException(ForbiddenException forbiddenException)
    {
        return new ExceptionDetails()
        {
            Title = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Forbidden),
            Detail = forbiddenException.Message.Trim(),
            Status = (int)HttpStatusCode.Forbidden,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };
    }
}
