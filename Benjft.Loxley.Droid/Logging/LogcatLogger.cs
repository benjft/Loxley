using Java.Lang;
using Microsoft.Extensions.Logging;
using Exception = System.Exception;

namespace Benjft.Loxley.Droid.Logging;

public class LogcatLogger(string category) : ILogger {
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter) {
        var message = formatter(state, exception);
        var throwable = exception != null ? Throwable.FromException(exception) : null;

        switch (logLevel) {
            case LogLevel.None:
            case LogLevel.Trace:
                Android.Util.Log.Verbose(category, throwable!, message);
                break;
            case LogLevel.Debug:
                Android.Util.Log.Debug(category, throwable!, message);
                break;
            case LogLevel.Information:
                Android.Util.Log.Info(category, throwable!, message);
                break;
            case LogLevel.Warning:
                Android.Util.Log.Warn(category, throwable!, message);
                break;
            case LogLevel.Error:
                Android.Util.Log.Error(category, throwable!, message);
                break;
            case LogLevel.Critical:
                Android.Util.Log.Wtf(category, throwable!, message);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
        }
    }

    public bool IsEnabled(LogLevel logLevel) {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
        return null;
    }
}
