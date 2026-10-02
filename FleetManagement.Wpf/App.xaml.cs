using System.Globalization;
using System.Windows;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf;

public partial class App : Application
{
    private FleetApiClient? _api;

    protected override void OnStartup(StartupEventArgs e)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("pt-BR");
        base.OnStartup(e);
        _api = new FleetApiClient();
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var login = new LoginWindow(_api);
        if (login.ShowDialog() != true)
        {
            Shutdown();
            return;
        }

        var shell = new MainWindow(_api);
        MainWindow = shell;
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        shell.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _api?.Dispose();
        base.OnExit(e);
    }
}
