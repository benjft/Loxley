using Benjft.Util.DependencyInjection.Attributes;
using Microsoft.Extensions.Logging;

namespace Benjft.Loxley.Pages;

[ServiceType<MainPage>]
[Transient]
public partial class MainPage {
    private readonly ILogger<MainPage> _logger;
    private int _count;

    public MainPage(ILogger<MainPage> logger) {
        _logger = logger;
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e) {
        _count++;

        _logger.LogInformation($"Clicked {_count} times");
        if (_count == 1) {
            CounterBtn.Text = $"Clicked {_count} time";
        } else {
            CounterBtn.Text = $"Clicked {_count} times";
        }

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
