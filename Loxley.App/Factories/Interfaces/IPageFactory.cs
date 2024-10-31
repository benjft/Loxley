namespace Loxley.App.Factories.Interfaces;

public interface IPageFactory {
    Page GetPage<TPage>() where TPage : Page;
}
