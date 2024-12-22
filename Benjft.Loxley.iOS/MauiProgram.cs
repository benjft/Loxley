using Benjft.Loxley.Extensions;

namespace Benjft.Loxley.iOS;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();

        builder
           .UseSharedMauiApp();

        return builder.Build();
    }
}
