using System.Windows;
using System.Windows.Controls;
using FleetManagement.Wpf.Features.Access;
using FleetManagement.Wpf.Features.Dashboard;
using FleetManagement.Wpf.Features.Reports;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;
using FleetManagement.Wpf.Themes;

namespace FleetManagement.Wpf;

public partial class MainWindow : Window
{
    private readonly FleetApiClient _api;
    private readonly ThemeManager? _themeManager;
    private readonly Dictionary<string, Button> _navigationButtons = new();
    private Button? _selectedButton;

    public MainWindow(FleetApiClient api, ThemeManager? themeManager = null)
    {
        InitializeComponent();
        _themeManager = themeManager ?? (Application.Current as App)?.ThemeManager;
        _themeManager?.AttachWindow(this);
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
        SelectThemePreference();
        Navigate("dashboard", "Painel", () => new DashboardView(_api));
    }

    private void BuildNavigation()
    {
        AddHeader("VISÃO GERAL");
        AddButton("dashboard", "Painel", () => new DashboardView(_api));
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
        AddButton("reports", "Relatórios e exportação", () => new ReportsView(_api));
        if (_api.User?.IsAdmin == true)
        {
            AddHeader("ADMINISTRAÇÃO");
            AddButton("users", "Usuários", () => new UsersView(_api));
            AddButton("audit", "Auditoria", () => new AuditView(_api));
        }
    }

    private void AddResource(string key)
    {
        var resource = ResourceCatalog.Get(key);
        AddButton(key, resource.Title, () => new ResourceView(_api, resource));
    }

    public void NavigateTo(string resourceKey)
    {
        var resource = ResourceCatalog.Get(resourceKey);
        Navigate(resourceKey, resource.Title, () => new ResourceView(_api, resource));
    }

    private void AddHeader(string text)
    {
        var header = new TextBlock
        {
            Text = text, FontSize = 11, FontWeight = FontWeights.SemiBold,
            Margin = new Thickness(11, 17, 0, 8)
        };
        header.SetResourceReference(TextBlock.ForegroundProperty, "SidebarMutedBrush");
        NavigationPanel.Children.Add(header);
    }

    private void AddButton(string key, string text, Func<UIElement> create)
    {
        var button = new Button
        {
            Content = text,
            Style = (Style)FindResource("SidebarNavButtonStyle")
        };
        button.Click += (_, _) => Navigate(key, text, create);
        _navigationButtons[key] = button;
        NavigationPanel.Children.Add(button);
    }

    private void Navigate(string key, string title, Func<UIElement> create)
    {
        MainContent.Content = create();
        SectionText.Text = title;
        if (_selectedButton is not null) _selectedButton.Tag = null;
        _selectedButton = _navigationButtons.GetValueOrDefault(key);
        if (_selectedButton is not null) _selectedButton.Tag = "Selected";
    }

    private void SelectThemePreference()
    {
        var preference = _themeManager?.Preference ?? ThemePreference.Automatic;
        foreach (ComboBoxItem item in ThemeSelector.Items)
        {
            if (item.Tag?.ToString() == preference.ToString())
            {
                ThemeSelector.SelectedItem = item;
                break;
            }
        }
    }

    private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeSelector.SelectedItem is ComboBoxItem item
            && Enum.TryParse<ThemePreference>(item.Tag?.ToString(), out var preference))
        {
            try
            {
                _themeManager?.SetPreference(preference);
            }
            catch (Exception ex) when (ex is System.IO.IOException or UnauthorizedAccessException)
            {
                MessageBox.Show(this, "Não foi possível salvar a preferência de aparência.",
                    "Aparência", MessageBoxButton.OK, MessageBoxImage.Warning);
                SelectThemePreference();
            }
        }
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
            _navigationButtons.Clear();
            _selectedButton = null;
            BuildNavigation();
            Navigate("dashboard", "Painel", () => new DashboardView(_api));
        });
    }
}
