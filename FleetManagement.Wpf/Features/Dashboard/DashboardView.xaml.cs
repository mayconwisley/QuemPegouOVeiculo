using System.Globalization;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Dashboard;

public partial class DashboardView : UserControl
{
    private readonly FleetApiClient _api;

    public DashboardView(FleetApiClient api)
    {
        InitializeComponent();
        _api = api;
        Loaded += async (_, _) => await LoadAsync();
    }

    private async Task LoadAsync()
    {
        try
        {
            EmptyState.Visibility = Visibility.Collapsed;
            StatusText.Text = "Atualizando painel...";
            var data = await _api.GetAsync("dashboard");
            MetricsPanel.Children.Clear();
            AddMetric("Veículos ativos", data, "activeVehicles");
            AddMetric("Motoristas ativos", data, "activeDrivers");
            AddMetric("Viagens em aberto", data, "openMovements");
            AddMetric("Status sem fim", data, "openVehicleStatuses");
            AddMetric("CNHs até 30 dias", data, "expiringLicenses");
            AddMetric("Retornos atrasados", data, "overdueReturns");
            AddMetric("Checklists pendentes", data, "pendingChecklists");
            AddMetric("Revisões até 30 dias", data, "dueMaintenancePlans");

            AttentionGrid.ItemsSource = data["attention"]?.AsArray().OfType<JsonObject>()
                .Select(x => new AttentionItem(
                    x["kind"]?.ToString() ?? "",
                    KindLabel(x["kind"]?.ToString()),
                    x["description"]?.ToString() ?? "",
                    FormatDate(x)))
                .ToArray() ?? [];
            EmptyState.Visibility = AttentionGrid.Items.Count == 0
                ? Visibility.Visible : Visibility.Collapsed;
            StatusText.Text = $"Atualizado em {DateTime.Now:dd/MM/yyyy HH:mm}";
        }
        catch (Exception ex)
        {
            EmptyState.Visibility = Visibility.Collapsed;
            StatusText.Text = "Não foi possível carregar o painel.";
            UiErrors.Show(ex, "Painel operacional");
        }
    }

    private void AddMetric(string label, JsonObject data, string key)
    {
        var card = new Border
        {
            Width = 184, Margin = new Thickness(0, 0, 12, 12),
            Style = (Style)FindResource("CardStyle")
        };
        var panel = new StackPanel();
        panel.Children.Add(new TextBlock
        {
            Text = label, TextWrapping = TextWrapping.Wrap, MinHeight = 34
        });
        var count = new TextBlock
        {
            Text = data[key]?.ToString() ?? "0", FontSize = 25, FontWeight = FontWeights.Bold,
            Margin = new Thickness(0, 5, 0, 0)
        };
        count.SetResourceReference(TextBlock.ForegroundProperty, "AccentBrush");
        panel.Children.Add(count);
        card.Child = panel;
        MetricsPanel.Children.Add(card);
    }

    private static string KindLabel(string? kind) => kind switch
    {
        "license" => "CNH",
        "overdue-return" => "Retorno atrasado",
        "departure-checklist" => "Checklist de saída",
        "arrival-checklist" => "Checklist de chegada",
        "maintenance-plan" => "Manutenção preventiva",
        _ => "Viagem em aberto"
    };

    private static string FormatDate(JsonObject attention)
    {
        var due = attention["dueDate"]?.ToString();
        if (DateOnly.TryParse(due, out var date))
            return date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"));
        var raw = attention["occurredAtUtc"]?.ToString();
        return DateTimeOffset.TryParse(raw, out var instant)
            ? instant.ToLocalTime().ToString("dd/MM/yyyy HH:mm") : "";
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e) => await LoadAsync();

    private void Attention_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (AttentionGrid.SelectedItem is not AttentionItem item) return;
        var resource = item.RawKind switch
        {
            "license" => "drivers",
            "maintenance-plan" => "maintenance-plans",
            _ => "movements"
        };
        (Window.GetWindow(this) as MainWindow)?.NavigateTo(resource);
    }

    public sealed record AttentionItem(string RawKind, string Kind, string Description, string Date);
}
