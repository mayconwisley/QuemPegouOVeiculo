using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Shared;

public partial class LookupView : UserControl
{
    private readonly FleetApiClient _api;
    private readonly bool _vehicles;
    private readonly Action<LookupRow> _select;
    private readonly Action _close;
    private CancellationTokenSource? _load;
    private int _page = 1;
    private int _total;

    public LookupView(FleetApiClient api, bool vehicles, Action<LookupRow> select, Action close)
    {
        InitializeComponent();
        _api = api;
        _vehicles = vehicles;
        _select = select;
        _close = close;
        TitleText.Text = vehicles ? "Selecionar veículo" : "Selecionar motorista";
        DescriptionText.Text = vehicles
            ? "Busque um veículo cadastrado para continuar."
            : "Busque um motorista cadastrado para continuar.";
        OptionsGrid.Columns[0].Header = vehicles ? "Placa e modelo" : "Nome";
        PreviousButton.IsEnabled = NextButton.IsEnabled = false;
        Loaded += async (_, _) =>
        {
            SearchBox.Focus();
            await LoadAsync();
        };
        Unloaded += (_, _) => _load?.Cancel();
        PreviewKeyDown += (_, e) =>
        {
            if (e.Key != Key.Escape) return;
            _close();
            e.Handled = true;
        };
    }

    private async Task LoadAsync()
    {
        _load?.Cancel();
        _load?.Dispose();
        _load = new CancellationTokenSource();
        try
        {
            LoadErrorText.Visibility = Visibility.Collapsed;
            EmptyState.Visibility = Visibility.Collapsed;
            StatusText.Text = "Carregando...";
            var resource = _vehicles ? "vehicles" : "drivers";
            var path = $"queries/{resource}" + FleetApiClient.Query(
                ("search", SearchBox.Text.Trim()), ("page", _page.ToString()),
                ("pageSize", "50"));
            var result = await _api.GetPageAsync(path, _load.Token);
            _total = result.Total;
            OptionsGrid.ItemsSource = result.Items.Select(item => new LookupRow(
                Guid.TryParse(item["id"]?.ToString(), out var id) ? id : Guid.Empty,
                _vehicles
                    ? $"{item["plate"]} · {item["model"]}"
                    : item["name"]?.ToString() ?? "")).ToArray();
            EmptyState.Text = _total == 0 && SearchBox.Text.Length == 0
                ? _vehicles
                    ? "Nenhum veículo cadastrado. Cadastre um veículo em Cadastros > Veículos para continuar."
                    : "Nenhum motorista cadastrado. Cadastre um motorista em Cadastros > Motoristas para continuar."
                : "Nenhum cadastro corresponde à busca. Tente outro termo.";
            EmptyState.Visibility = result.Items.Count == 0
                ? Visibility.Visible : Visibility.Collapsed;
            StatusText.Text = $"{_total:N0} resultado(s) · página {_page}";
            PreviousButton.IsEnabled = _page > 1;
            NextButton.IsEnabled = _page * 50 < _total;
            SelectButton.IsEnabled = OptionsGrid.SelectedItem is LookupRow;
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            StatusText.Text = "Falha ao carregar os cadastros.";
            LoadErrorText.Text = ex.Message;
            LoadErrorText.Visibility = Visibility.Visible;
        }
    }

    private async void Search_Click(object sender, RoutedEventArgs e)
    {
        _page = 1;
        await LoadAsync();
    }

    private async void Search_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter) return;
        _page = 1;
        await LoadAsync();
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

    private void Options_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
        SelectButton.IsEnabled = OptionsGrid.SelectedItem is LookupRow;

    private void Options_DoubleClick(object sender, MouseButtonEventArgs e) => Choose();
    private void Select_Click(object sender, RoutedEventArgs e) => Choose();
    private void Back_Click(object sender, RoutedEventArgs e) => _close();

    private void Choose()
    {
        if (OptionsGrid.SelectedItem is LookupRow row)
            _select(row);
    }

    public sealed record LookupRow(Guid Id, string Label);
}
