using Benjft.Loxley.Pages;
using Benjft.Loxley.Services;
using Benjft.Util.DependencyInjection.Attributes;

namespace Benjft.Loxley;

[ServiceType<App>]
[Singleton]
public partial class App : Application {
    private readonly INavigationService _navigationService;

    public App(INavigationService navigationService) {
        _navigationService = navigationService;

        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState) {
        var navigationPage = _navigationService.Initialize<MainPage>();
        return new Window(navigationPage);
    }
}
