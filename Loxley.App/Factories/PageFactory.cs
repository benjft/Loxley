using Loxley.App.Factories.Interfaces;
using Loxley.Core.Attributes;

namespace Loxley.App.Factories;

[ServiceFor<IPageFactory>]
[ServiceLifetime(ServiceLifetime.Singleton)]
public class PageFactory(IServiceProvider services) : IPageFactory {
    public Page GetPage<TPage>() where TPage : Page {
        return services.GetRequiredService<TPage>();
    }
}
