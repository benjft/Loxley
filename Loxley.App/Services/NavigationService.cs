using Loxley.App.Factories.Interfaces;
using Loxley.App.Services.Interfaces;
using Loxley.Core.Attributes;

namespace Loxley.App.Services;

[ServiceFor<INavigationService>]
[ServiceLifetime(ServiceLifetime.Singleton)]
public class NavigationService(IPageFactory pageFactory) : INavigationService {
    private NavigationPage? _navigationPage;

    public NavigationPage NavigateTo<TPage>() where TPage : Page {
        var page = pageFactory.GetPage<TPage>();

        if (_navigationPage is null) {
            _navigationPage = new NavigationPage(page);
        } else {
            _navigationPage.PushAsync(page);
        }

        return _navigationPage;
    }
}
