using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Benjft.Loxley.Droid.Logging;

public sealed class LogcatLoggerProvider : ILoggerProvider {
    private readonly ConcurrentDictionary<string, ILogger> _loggers = new();

    public ILogger CreateLogger(string categoryName) {
        categoryName = categoryName.Split('.').Last();

        var logger = _loggers.GetOrAdd(categoryName, new LogcatLogger(categoryName));
        return logger;
    }

    public void Dispose() {
        _loggers.Clear();
    }
}
