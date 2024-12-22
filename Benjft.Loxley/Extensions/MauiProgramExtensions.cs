using Benjft.Util.DependencyInjection.Extensions;

namespace Benjft.Loxley.Extensions;

public static class MauiProgramExtensions {
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder) {
        builder
           .UseMauiApp<App>()
           .ConfigureFonts(
                fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

        builder.Services.ScanAppDomainForServices();

        return builder;
    }
}
