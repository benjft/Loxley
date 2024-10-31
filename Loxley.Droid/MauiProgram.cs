using Loxley.App.Extensions;

namespace Loxley.Droid;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();

        var topLevelAssembly = typeof(MauiProgram).Assembly;

        builder
            .UseSharedMauiApp()
            .AddAppServices(topLevelAssembly)
            .AddPages(topLevelAssembly);

        return builder.Build();
    }
}
