using Microsoft.Extensions.Logging;

namespace Ouijjane.Shared.Infrastructure.Diagnostics.Logging;

public static class LoggingHelper
{
    private static readonly Action<ILogger, string, Exception> LoggerMessageTrace = LoggerMessage.Define<string>(LogLevel.Trace, new EventId(0, "TRACE"), "{Message}");
    private static readonly Action<ILogger, string, Exception> LoggerMessageDebug = LoggerMessage.Define<string>(LogLevel.Debug, new EventId(100, "DEBUG"), "{Message}");
    private static readonly Action<ILogger, string, Exception> LoggerMessageInformation = LoggerMessage.Define<string>(LogLevel.Information, new EventId(200, "INFORMATION"), "{Message}");
    private static readonly Action<ILogger, string, Exception> LoggerMessageWarning = LoggerMessage.Define<string>(LogLevel.Warning, new EventId(300, "WARNING"), "{Message}");
    private static readonly Action<ILogger, string, Exception> LoggerMessageError = LoggerMessage.Define<string>(LogLevel.Error, new EventId(400, "ERROR"), "{Message}");
    private static readonly Action<ILogger, string, Exception> LoggerMessageCritical = LoggerMessage.Define<string>(LogLevel.Critical, new EventId(500, "CRITICAL"), "{Message}");

    public static void FastLog(this ILogger logger, LogLevel logLevel, string message, Exception? ex = null)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.", nameof(message));//TODO localisation
        }

        switch (logLevel)
        {
            case LogLevel.Critical:
                {
                    LoggerMessageCritical(logger, message, ex);
                    break;
                }

            case LogLevel.Debug:
                {
                    LoggerMessageDebug(logger, message, ex);
                    break;
                }

            case LogLevel.Error:
                {
                    LoggerMessageError(logger, message, ex);
                    break;
                }

            case LogLevel.Trace:
                {
                    LoggerMessageTrace(logger, message, ex);
                    break;
                }

            case LogLevel.Warning:
                {
                    LoggerMessageWarning(logger, message, ex);
                    break;
                }

            default:
                {
                    LoggerMessageInformation(logger, message, ex);
                    break;
                }
        }
    }
}
