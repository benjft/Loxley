namespace Loxley.App.Services.Interfaces;

public interface INavigationService {
    NavigationPage NavigateTo<TPage>() where TPage : Page;
}
