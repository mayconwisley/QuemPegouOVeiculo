using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Shared;

public partial class LookupDialog : Window
{
    private readonly FleetApiClient _api;
    private readonly bool _vehicles;
    private int _page = 1;
    private int _total;

    public LookupDialog(FleetApiClient api, bool vehicles)
    {
        InitializeComponent();
        _api = api;
        _vehicles = vehicles;
        Title = vehicles ? "Selecionar veículo" : "Selecionar motorista";
        Loaded += async (_, _) => await LoadAsync();
    }

    public int SelectedId { get; private set; }
    public string SelectedLabel { get; private set; } = "";

    private async Task LoadAsync()
    {
        try
        {
            var resource = _vehicles ? "vehicles" : "drivers";
            var path = $"queries/{resource}" + FleetApiClient.Query(
                ("search", SearchBox.Text.Trim()), ("page", _page.ToString()),
                ("pageSize", "50"));
            var result = await _api.GetPageAsync(path);
            _total = result.Total;
            OptionsGrid.ItemsSource = result.Items.Select(x => new LookupRow(
                x["id"]?.GetValue<int>() ?? 0,
                _vehicles ? $"{x["plate"]} · {x["model"]}" : x["name"]?.ToString() ?? ""))
                .ToArray();
            StatusText.Text = $"{_total:N0} resultado(s) · página {_page}";
        }
        catch (Exception ex) { UiErrors.Show(ex, Title); }
    }

    private async void Search_Click(object sender, RoutedEventArgs e)
    {
        _page = 1;
        await LoadAsync();
    }
    private async void Search_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            _page = 1;
            await LoadAsync();
        }
    }
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
    private void Options_DoubleClick(object sender, MouseButtonEventArgs e) => Choose();
    private void Select_Click(object sender, RoutedEventArgs e) => Choose();

    private void Choose()
    {
        if (OptionsGrid.SelectedItem is not LookupRow row) return;
        SelectedId = row.Id;
        SelectedLabel = row.Label;
        DialogResult = true;
    }

    public sealed record LookupRow(int Id, string Label);
}
