using Loxley.App.Services.Interfaces;

namespace Loxley.App;

public partial class App : Application {
    public App(INavigationService navigationService) {
        InitializeComponent();

        MainPage = navigationService.NavigateTo<MainPage>();
    }
}
