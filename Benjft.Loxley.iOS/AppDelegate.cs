using Foundation;

namespace Benjft.Loxley.iOS;

[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate {
    protected override MauiApp CreateMauiApp() {
        return MauiProgram.CreateMauiApp();
    }
}
