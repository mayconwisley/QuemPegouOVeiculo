using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Access;

public partial class AuditView : UserControl
{
    private readonly FleetApiClient _api;
    private int _page = 1;
    private int _total;

    public AuditView(FleetApiClient api)
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
            var page = await _api.GetPageAsync($"audit?page={_page}&pageSize=50");
            _total = page.Total;
            AuditGrid.ItemsSource = page.Items.Select(x => new GridRow(x)).ToArray();
            EmptyState.Visibility = page.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            StatusText.Text = $"{_total:N0} evento(s) · página {_page}";
        }
        catch (Exception ex)
        {
            EmptyState.Visibility = Visibility.Collapsed;
            UiErrors.Show(ex, "Auditoria");
        }
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e) => await LoadAsync();
    private async void Previous_Click(object sender, RoutedEventArgs e)
    {
        if (_page > 1) _page--;
        await LoadAsync();
    }
    private async void Next_Click(object sender, RoutedEventArgs e)
    {
        if (_page * 50 < _total) _page++;
        await LoadAsync();
    }

    private void Audit_DoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (AuditGrid.SelectedItem is not GridRow row) return;
        MessageBox.Show(row.Source["changesJson"]?.ToString() ?? "Sem detalhes.",
            $"Evento {row.Source["id"]}", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
