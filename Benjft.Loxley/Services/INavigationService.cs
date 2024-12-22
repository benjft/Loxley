namespace Benjft.Loxley.Services;

public interface INavigationService {
    Page Initialize<T>() where T : Page;
    Page Initialize(Page page);
    Task PushAsync(Page page);
    Task PopAsync();
}
