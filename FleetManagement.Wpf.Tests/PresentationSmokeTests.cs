using System.IO;
using System.Runtime.ExceptionServices;
using System.Text.Json.Nodes;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using FleetManagement.Wpf;
using FleetManagement.Wpf.Features.Access;
using FleetManagement.Wpf.Features.Dashboard;
using FleetManagement.Wpf.Features.Reports;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;
using FleetManagement.Wpf.Themes;
using Xunit;

namespace FleetManagement.Wpf.Tests;

public sealed class PresentationSmokeTests
{
    [Fact]
    public void AllViewsAndThemes_LoadAndSwitchOnStaThread()
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try
            {
                var app = new App();
                app.InitializeComponent();
                using var api = new FleetApiClient();
                var lightKeys = app.Resources.MergedDictionaries[0].Keys
                    .Cast<object>().OrderBy(x => x.ToString()).ToArray();
                var darkPalette = new ResourceDictionary
                {
                    Source = new Uri("/FleetManagement.Wpf;component/Themes/Dark.xaml",
                        UriKind.Relative)
                };
                Assert.Equal(lightKeys, darkPalette.Keys.Cast<object>()
                    .OrderBy(x => x.ToString()).ToArray());
                var settingsPath = Path.Combine(Path.GetTempPath(),
                    "fleet-theme-test-" + Guid.NewGuid().ToString("N") + ".json");
                try
                {
                    var systemDark = false;
                    using var themes = new ThemeManager(app, () => systemDark,
                        settingsPath, monitorSystem: false);
                    var existingWindow = new LoginWindow(api);
                    var root = (Grid)existingWindow.Content;
                    Assert.Equal(ThemePreference.Automatic, themes.Preference);
                    Assert.False(themes.IsDark);
                    Assert.Equal(Color.FromRgb(244, 247, 251),
                        ((SolidColorBrush)root.Background).Color);
                    themes.SetPreference(ThemePreference.Dark);
                    Assert.True(themes.IsDark);
                    Assert.Equal(Color.FromRgb(13, 21, 35),
                        ((SolidColorBrush)root.Background).Color);
                    Assert.Equal(Color.FromRgb(13, 21, 35),
                        ((SolidColorBrush)app.FindResource("CanvasBrush")).Color);
                    themes.SetPreference(ThemePreference.Automatic);
                    systemDark = true;
                    themes.RefreshSystemTheme();
                    Assert.True(themes.IsDark);
                    systemDark = false;
                    themes.RefreshSystemTheme();
                    Assert.False(themes.IsDark);
                    Assert.Contains("Automatic", File.ReadAllText(settingsPath));
                }
                finally
                {
                    if (File.Exists(settingsPath)) File.Delete(settingsPath);
                }
                _ = new LoginWindow(api);
                _ = new MainWindow(api);
                _ = new DashboardView(api);
                var reports = new ReportsView(api);
                _ = new UsersView(api);
                _ = new AuditView(api);
                _ = new LookupView(api, vehicles: true, _ => { }, () => { });
                _ = new ResourceEditorView(api, "Teste", ResourceCatalog.Get("drivers").Fields,
                    _ => Task.CompletedTask, () => { });
                _ = new UserEditorView("Teste", _ => Task.CompletedTask, () => { },
                    requireUsername: true, requirePassword: true);
                foreach (var resource in ResourceCatalog.All)
                    _ = new ResourceView(api, resource);

                var reservation = new ResourceEditorView(api, "Nova reserva",
                    ResourceCatalog.Get("reservations").Fields,
                    _ => Task.CompletedTask, () => { }, resourceKey: "reservations");
                var fields = (StackPanel)reservation.FindName("FieldsPanel")!;
                var purpose = (TextBox)fields.Children[9];
                purpose.Text = "Visita técnica";
                var vehicleButton = (Button)fields.Children[1];
                vehicleButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                var lookupHost = (ContentControl)reservation.FindName("LookupHost")!;
                Assert.Equal(Visibility.Visible, lookupHost.Visibility);
                var vehicleLookup = Assert.IsType<LookupView>(lookupHost.Content);
                var options = (DataGrid)vehicleLookup.FindName("OptionsGrid")!;
                options.ItemsSource = new[] { new LookupView.LookupRow(12, "ABC1D23 · Sedan") };
                options.SelectedIndex = 0;
                ((Button)vehicleLookup.FindName("SelectButton")!)
                    .RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Assert.Null(lookupHost.Content);
                Assert.Equal(12, vehicleButton.Tag);
                Assert.Equal("Visita técnica", purpose.Text);

                var driverButton = (Button)fields.Children[3];
                driverButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                var driverLookup = Assert.IsType<LookupView>(lookupHost.Content);
                var drivers = (DataGrid)driverLookup.FindName("OptionsGrid")!;
                drivers.ItemsSource = new[] { new LookupView.LookupRow(8, "Ana Souza") };
                drivers.SelectedIndex = 0;
                ((Button)driverLookup.FindName("SelectButton")!)
                    .RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Assert.Equal(8, driverButton.Tag);
                Assert.Equal("Visita técnica", purpose.Text);

                ((ComboBox)reports.FindName("ReportBox")!).SelectedIndex = 3;
                ((Button)reports.FindName("VehicleButton")!)
                    .RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                var reportHost = (ContentControl)reports.FindName("LookupHost")!;
                var reportLookup = Assert.IsType<LookupView>(reportHost.Content);
                var reportOptions = (DataGrid)reportLookup.FindName("OptionsGrid")!;
                reportOptions.ItemsSource = new[] { new LookupView.LookupRow(12, "ABC1D23 · Sedan") };
                reportOptions.SelectedIndex = 0;
                ((Button)reportLookup.FindName("SelectButton")!)
                    .RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Assert.Null(reportHost.Content);
                Assert.Equal("ABC1D23 · Sedan",
                    ((Button)reports.FindName("VehicleButton")!).Content);

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
