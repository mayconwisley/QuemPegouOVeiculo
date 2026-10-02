using System.Runtime.ExceptionServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using FleetManagement.Wpf;
using FleetManagement.Wpf.Features.Access;
using FleetManagement.Wpf.Features.Dashboard;
using FleetManagement.Wpf.Features.Reports;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;
using Xunit;

namespace FleetManagement.Wpf.Tests;

public sealed class PresentationSmokeTests
{
    [Fact]
    public void AllViews_LoadTheirXamlOnStaThread()
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try
            {
                var app = new App();
                app.InitializeComponent();
                using var api = new FleetApiClient();

                _ = new LoginWindow(api);
                _ = new MainWindow(api);
                _ = new DashboardView(api);
                _ = new ReportsView(api);
                _ = new UsersView(api);
                _ = new AuditView(api);
                _ = new LookupDialog(api, vehicles: true);
                _ = new EditDialog(api, "Teste", ResourceCatalog.Get("drivers").Fields);
                _ = new UserDialog("Teste", requireUsername: true, requirePassword: true);
                foreach (var resource in ResourceCatalog.All)
                    _ = new ResourceView(api, resource);

                var plate = new TextBlock();
                BindingOperations.SetBinding(plate, TextBlock.TextProperty,
                    new Binding("[plate]")
                    {
                        Source = new GridRow(new JsonObject { ["plate"] = "ABC1D23" })
                    });
                BindingOperations.GetBindingExpression(plate, TextBlock.TextProperty)?.UpdateTarget();
                Assert.Equal("ABC1D23", plate.Text);
            }
            catch (Exception ex)
            {
                failure = ex;
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();
        if (failure is not null)
            ExceptionDispatchInfo.Capture(failure).Throw();
    }
}
