using System.Reflection;
using Loxley.Core.Extensions;
using Microsoft.Extensions.Logging;

namespace Loxley.Extensions;

public static class MauiProgramExtensions {
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder) {
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        #if DEBUG
        builder.Logging.AddDebug();
        #endif

        return builder;
    }

    public static MauiAppBuilder AddAppServices(this MauiAppBuilder builder, Assembly topLevelAssembly) {
        var services = builder.Services;
        services.AddAppServices(topLevelAssembly);
        return builder;
    }
}