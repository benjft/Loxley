using Loxley.Extensions;

namespace Loxley.Droid;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseSharedMauiApp()
            .RegisterAppServices(typeof(MauiApp).Assembly);

        return builder.Build();
    }
}