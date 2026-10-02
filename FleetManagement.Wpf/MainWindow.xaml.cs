using System.Windows;
using System.Windows.Controls;
using FleetManagement.Wpf.Features.Access;
using FleetManagement.Wpf.Features.Dashboard;
using FleetManagement.Wpf.Features.Reports;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf;

public partial class MainWindow : Window
{
    private readonly FleetApiClient _api;

    public MainWindow(FleetApiClient api)
    {
        InitializeComponent();
        _api = api;
        _api.SessionExpired += HandleSessionExpired;
        Closed += (_, _) => _api.SessionExpired -= HandleSessionExpired;
        UserText.Text = api.User?.Username ?? "";
        RoleText.Text = api.User?.Role switch
        {
            "Administrator" => "Administrador", "Operator" => "Operador",
            _ => "Consulta"
        };
        BuildNavigation();
        MainContent.Content = new DashboardView(_api);
    }

    private void BuildNavigation()
    {
        NavigationPanel.Children.Add(new TextBlock
        {
            Text = "GESTÃO DE FROTA",
            Foreground = System.Windows.Media.Brushes.White,
            FontSize = 17,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(4, 0, 0, 24)
        });
        AddHeader("VISÃO GERAL");
        AddButton("Painel", () => new DashboardView(_api));
        AddHeader("CADASTROS");
        AddResource("drivers");
        AddResource("vehicles");
        AddResource("license-expirations");
        AddHeader("OPERAÇÃO");
        AddResource("movements");
        AddResource("reservations");
        AddResource("refuelings");
        AddResource("fines");
        AddResource("maintenance");
        AddResource("maintenance-plans");
        AddResource("vehicle-statuses");
        AddHeader("ANÁLISE");
        AddButton("Relatórios e exportação", () => new ReportsView(_api));
        if (_api.User?.IsAdmin == true)
        {
            AddHeader("ADMINISTRAÇÃO");
            AddButton("Usuários", () => new UsersView(_api));
            AddButton("Auditoria", () => new AuditView(_api));
        }
    }

    private void AddResource(string key)
    {
        var resource = ResourceCatalog.Get(key);
        AddButton(resource.Title, () => new ResourceView(_api, resource));
    }

    public void NavigateTo(string resourceKey) =>
        MainContent.Content = new ResourceView(_api, ResourceCatalog.Get(resourceKey));

    private void AddHeader(string text) => NavigationPanel.Children.Add(new TextBlock
    {
        Text = text, Foreground = new System.Windows.Media.SolidColorBrush(
            System.Windows.Media.Color.FromRgb(139, 161, 193)),
        FontSize = 11, FontWeight = FontWeights.SemiBold,
        Margin = new Thickness(5, 17, 0, 7)
    });

    private void AddButton(string text, Func<UIElement> create)
    {
        var button = new Button
        {
            Content = text,
            HorizontalContentAlignment = HorizontalAlignment.Left,
            Background = System.Windows.Media.Brushes.Transparent,
            Foreground = System.Windows.Media.Brushes.White,
            BorderThickness = new Thickness(0),
            Margin = new Thickness(0, 0, 0, 3),
            MinHeight = 37
        };
        button.Click += (_, _) => MainContent.Content = create();
        NavigationPanel.Children.Add(button);
    }

    private void Exit_Click(object sender, RoutedEventArgs e) => Close();

    private void HandleSessionExpired(object? sender, EventArgs e)
    {
        Dispatcher.BeginInvoke(() =>
        {
            MessageBox.Show(this, "Sua sessão expirou. Entre novamente.", "Sessão expirada",
                MessageBoxButton.OK, MessageBoxImage.Information);
            var login = new LoginWindow(_api) { Owner = this };
            if (login.ShowDialog() != true)
            {
                Close();
                return;
            }
            UserText.Text = _api.User?.Username ?? "";
            RoleText.Text = _api.User?.Role switch
            {
                "Administrator" => "Administrador", "Operator" => "Operador",
                _ => "Consulta"
            };
            NavigationPanel.Children.Clear();
            BuildNavigation();
            MainContent.Content = new DashboardView(_api);
        });
    }
}
