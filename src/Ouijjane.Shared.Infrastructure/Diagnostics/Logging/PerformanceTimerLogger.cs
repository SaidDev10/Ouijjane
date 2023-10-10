using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Diagnostics;

namespace Ouijjane.Shared.Infrastructure.Diagnostics.Logging;
public class PerformanceTimer
{
    public static IDisposable Log(ILogger logger, string message, params object[] args)
    {
        return new PerformanceTimeLogger(logger, message, true, args);
    }

    public static IDisposable LogAlways(ILogger logger, string message, params object[] args)
    {
        return new PerformanceTimeLogger(logger, message, false, args);
    }

    private class PerformanceTimeLogger : IDisposable
    {
        private const string ElapsedTimeKey = "elapsedTime";
        private const string ElapsedMillisecondsKey = "ElapsedMilliseconds";

        private readonly ILogger _logger;
        private readonly string _message;
        private readonly bool _includeTiming;
        private readonly object[] _args;
        private readonly Stopwatch _timer;

        public PerformanceTimeLogger(ILogger logger, string message, bool includeTiming, params object[] args)
        {
            _logger = logger;
            _message = message;
            _includeTiming = includeTiming;
            _args = args;
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void Dispose()
        {
            _timer.Stop();

            var ts = _timer.Elapsed;
            var ms = _timer.ElapsedMilliseconds;

            if (_includeTiming && _logger.IsEnabled(LogLevel.Debug))
            {
                LogMessage(ts, ms, LogLevel.Debug);
            }
            else if (ms > 500 && _logger.IsEnabled(LogLevel.Warning))
            {
                LogMessage(ts, ms, LogLevel.Warning);
            }
        }

        private void LogMessage(TimeSpan ts, long ms, LogLevel logLevel)
        {
            using (LogContext.PushProperty(ElapsedTimeKey, $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds}"))
            {
                using (LogContext.PushProperty(ElapsedMillisecondsKey, ms))
                {
                    _logger.FastLog(logLevel, string.Format(_message, _args));
                }
            }
        }
    }
}