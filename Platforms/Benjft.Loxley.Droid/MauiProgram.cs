using Benjft.Loxley.Droid.Logging;
using Benjft.Loxley.Extensions;
using Benjft.Util.Debug.Helpers;
using Microsoft.Extensions.Logging;

namespace Benjft.Loxley.Droid;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();

        const LogLevel logLevel = DebugFlagHelper.IsDebug ? LogLevel.Debug : LogLevel.Information;
        builder.Logging.AddProvider(new LogcatLoggerProvider())
           .AddFilter("Benjft.Loxley", logLevel);

        builder.UseSharedMauiApp();

        return builder.Build();
    }
}
