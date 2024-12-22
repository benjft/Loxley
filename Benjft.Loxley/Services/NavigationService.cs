using Benjft.Util.DependencyInjection.Attributes;
using Microsoft.Extensions.Logging;

namespace Benjft.Loxley.Services;

[Singleton]
[ServiceType<INavigationService>]
public class NavigationService(IServiceProvider serviceProvider, ILogger<NavigationService> logger)
    : INavigationService {
    private NavigationPage? _navigationPage = null;

    public Page Initialize<T>() where T : Page {
        var rootPage = serviceProvider.GetRequiredService<T>();
        return Initialize(rootPage);
    }

    public Page Initialize(Page page) {
        if (_navigationPage != null) {
            logger.LogWarning("Navigation Service already initialized.");
            return _navigationPage;
        }

        _navigationPage = new NavigationPage(page);
        return _navigationPage;
    }

    public Task PushAsync(Page page) {
        if (_navigationPage == null) {
            logger.LogWarning("Navigation Service not initialized.");
            return Task.CompletedTask;
        }

        return _navigationPage.PushAsync(page);
    }

    public Task PopAsync() {
        if (_navigationPage == null) {
            logger.LogWarning("Navigation Service not initialized.");
            return Task.CompletedTask;
        }

        return _navigationPage.PopAsync();
    }
}
