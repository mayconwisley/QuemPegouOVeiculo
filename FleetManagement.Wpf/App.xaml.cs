using System.Globalization;
using System.Windows;
using FleetManagement.Wpf.Infrastructure.Api;
using FleetManagement.Wpf.Themes;

namespace FleetManagement.Wpf;

public partial class App : Application
{
    private FleetApiClient? _api;
    internal ThemeManager? ThemeManager { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("pt-BR");
        base.OnStartup(e);
        ThemeManager = new ThemeManager(this);
        _api = new FleetApiClient();
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var login = new LoginWindow(_api);
        if (login.ShowDialog() != true)
        {
            Shutdown();
            return;
        }

        var shell = new MainWindow(_api, ThemeManager);
        MainWindow = shell;
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        shell.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        ThemeManager?.Dispose();
        _api?.Dispose();
        base.OnExit(e);
    }
}
