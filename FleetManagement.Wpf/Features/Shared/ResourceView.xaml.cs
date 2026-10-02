using System.Net.Http;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Shared;

public partial class ResourceView : UserControl
{
    private readonly FleetApiClient _api;
    private readonly ResourceDefinition _resource;
    private CancellationTokenSource? _load;
    private int _page = 1;
    private int _total;

    public ResourceView(FleetApiClient api, ResourceDefinition resource)
    {
        InitializeComponent();
        _api = api;
        _resource = resource;
        TitleText.Text = resource.Title;
        SearchBox.Visibility = resource.Key is "reservations" or "maintenance-plans"
            ? Visibility.Collapsed : Visibility.Visible;
        SearchButton.Visibility = SearchBox.Visibility;
        ConfigureFilter();

        foreach (var column in resource.Columns)
            ResultsGrid.Columns.Add(new DataGridTextColumn
            {
                Header = column.Label,
                Binding = new Binding($"[{column.Key}]") { Mode = BindingMode.OneWay },
                MinWidth = 90,
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });

        var canWrite = _api.User?.CanWrite == true;
        EmptyState.Text = canWrite
            ? "Nenhum registro encontrado. Ajuste os filtros ou crie um novo cadastro."
            : "Nenhum registro encontrado. Ajuste os filtros da consulta.";
        NewButton.Visibility = EditButton.Visibility = canWrite ? Visibility.Visible : Visibility.Collapsed;
        DeleteButton.Visibility = canWrite && resource.CanDelete ? Visibility.Visible : Visibility.Collapsed;
        ConfigureActions(canWrite);
        Loaded += async (_, _) => await RefreshAsync();
        Unloaded += (_, _) => _load?.Cancel();
        Selection_Changed(this, null!);
    }

    private GridRow? Selected => ResultsGrid.SelectedItem as GridRow;

    private void ShowEditor(string title, IReadOnlyList<Field> fields,
        JsonObject? original, Func<JsonObject, Task>? submit,
        string? resourceKey = null, bool readOnly = false)
    {
        EditorHost.Content = new ResourceEditorView(_api, title, fields, submit,
            CloseEditor, original, resourceKey, readOnly);
        ListContent.Visibility = Visibility.Collapsed;
        EditorHost.Visibility = Visibility.Visible;
    }

    private void CloseEditor()
    {
        EditorHost.Content = null;
        EditorHost.Visibility = Visibility.Collapsed;
        ListContent.Visibility = Visibility.Visible;
    }

    private void ConfigureFilter()
    {
        string[]? labels = _resource.Key switch
        {
            "reservations" => ["Todas", "Confirmadas", "Em uso", "Concluídas", "Canceladas"],
            "movements" => ["Todas", "Em aberto", "Concluídas"],
            "vehicle-statuses" => ["Todas", "Em aberto", "Concluídas"],
            _ => null
        };
        if (labels is null) return;
        StateFilter.ItemsSource = labels;
        StateFilter.SelectedIndex = 0;
        StateFilter.Visibility = Visibility.Visible;
    }

    private (string Name, string? Value) StateQuery()
    {
        if (_resource.Key == "reservations")
        {
            string? status = StateFilter.SelectedIndex switch
            {
                1 => "Confirmed", 2 => "InUse", 3 => "Completed",
                4 => "Cancelled", _ => null
            };
            return ("status", status);
        }
        return ("isOpen", StateFilter.SelectedIndex switch
        {
            1 => "true", 2 => "false", _ => null
        });
    }

    private void ConfigureActions(bool canWrite)
    {
        if (_resource.Key == "movements")
        {
            SecondaryActionButton.Content = canWrite ? "Checklist" : "Ver checklist";
            SecondaryActionButton.Visibility = Visibility.Visible;
        }
        if (!canWrite)
            return;
        switch (_resource.Key)
        {
            case "movements":
                ActionButton.Content = "Registrar chegada";
                SecondaryActionButton.Content = "Checklist";
                ThirdActionButton.Content = "Previsão de retorno";
                break;
            case "reservations":
                ActionButton.Content = "Iniciar viagem";
                SecondaryActionButton.Content = "Cancelar reserva";
                break;
            case "maintenance-plans":
                ActionButton.Content = "Registrar execução";
                break;
        }
        if (ActionButton.Content is not null)
            ActionButton.Visibility = Visibility.Visible;
        if (SecondaryActionButton.Content is not null)
            SecondaryActionButton.Visibility = Visibility.Visible;
        if (ThirdActionButton.Content is not null)
            ThirdActionButton.Visibility = Visibility.Visible;
    }

    private async Task RefreshAsync()
    {
        _load?.Cancel();
        _load?.Dispose();
        _load = new CancellationTokenSource();
        try
        {
            EmptyState.Visibility = Visibility.Collapsed;
            StatusText.Text = "Carregando...";
            var search = SearchBox.Visibility == Visibility.Visible ? SearchBox.Text.Trim() : null;
            var path = _resource.ListPath + FleetApiClient.Query(
                ("search", search), StateQuery(),
                ("page", _page.ToString()), ("pageSize", "50"));
            var data = await _api.GetPageAsync(path, _load.Token);
            _total = data.Total;
            ResultsGrid.ItemsSource = data.Items.Select(x => new GridRow(x)).ToArray();
            EmptyState.Visibility = data.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            StatusText.Text = $"{_total:N0} registro(s) · página {_page} de {Math.Max(1, (_total + 49) / 50)}";
            PreviousButton.IsEnabled = _page > 1;
            NextButton.IsEnabled = _page * 50 < _total;
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            EmptyState.Visibility = Visibility.Collapsed;
            StatusText.Text = "Falha ao carregar os registros.";
            UiErrors.Show(ex, _resource.Title);
        }
    }

    private void Selection_Changed(object sender, SelectionChangedEventArgs e)
    {
        var row = Selected;
        var selected = row is not null;
        DetailsButton.IsEnabled = EditButton.IsEnabled = DeleteButton.IsEnabled = selected;
        ActionButton.IsEnabled = SecondaryActionButton.IsEnabled =
            ThirdActionButton.IsEnabled = selected;
        if (row is null) return;
        if (_resource.Key == "movements")
        {
            ActionButton.IsEnabled = row.Source["arrivalUtc"] is null;
            ThirdActionButton.IsEnabled = row.Source["arrivalUtc"] is null;
        }
        else if (_resource.Key == "reservations")
        {
            var confirmed = row.Source["status"]?.ToString() == "Confirmed";
            EditButton.IsEnabled = SecondaryActionButton.IsEnabled = confirmed;
            ActionButton.IsEnabled = confirmed;
        }
        else if (_resource.Key == "maintenance-plans")
        {
            ActionButton.IsEnabled = row.Source["isActive"]?.GetValue<bool>() == true;
        }
    }

    private async void Search_Click(object sender, RoutedEventArgs e)
    {
        _page = 1;
        await RefreshAsync();
    }

    private async void State_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (!IsLoaded) return;
        _page = 1;
        await RefreshAsync();
    }

    private async void Search_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            _page = 1;
            await RefreshAsync();
        }
    }

    private async void Refresh_Click(object sender, RoutedEventArgs e) => await RefreshAsync();
    private async void Previous_Click(object sender, RoutedEventArgs e)
    {
        if (_page > 1) _page--;
        await RefreshAsync();
    }
    private async void Next_Click(object sender, RoutedEventArgs e)
    {
        if (_page * 50 < _total) _page++;
        await RefreshAsync();
    }

    private async void New_Click(object sender, RoutedEventArgs e) => await EditAsync(null);
    private async void Edit_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is { } row)
            await EditAsync(row);
    }

    private async void Details_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row) return;
        try
        {
            var current = _resource.Key is "reservations" or "maintenance-plans"
                ? await _api.GetAsync($"{_resource.CommandPath}/{row.Id}")
                : row.Source;
            ShowEditor($"Detalhes · {_resource.Title}", _resource.Fields,
                current, null, _resource.Key, readOnly: true);
        }
        catch (Exception ex) { UiErrors.Show(ex, "Consultar registro"); }
    }

    private async Task EditAsync(GridRow? row)
    {
        try
        {
            JsonObject? current = row?.Source;
            if (row is not null && _resource.Key is "reservations" or "maintenance-plans")
                current = await _api.GetAsync($"{_resource.CommandPath}/{row.Id}");
            var path = row is null ? _resource.CommandPath : $"{_resource.CommandPath}/{row.Id}";
            ShowEditor(row is null ? $"Novo · {_resource.Title}" :
                $"Editar · {_resource.Title}", _resource.Fields, current,
                async value =>
                {
                    await _api.SendAsync(row is null ? HttpMethod.Post : HttpMethod.Put, path, value);
                    await RefreshAsync();
                }, _resource.Key);
        }
        catch (Exception ex) { UiErrors.Show(ex, "Salvar registro"); }
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row || MessageBox.Show(
            $"Excluir o registro {row.Id}?", "Confirmar exclusão",
            MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
            return;
        try
        {
            await _api.SendAsync(HttpMethod.Delete, $"{_resource.CommandPath}/{row.Id}");
            await RefreshAsync();
        }
        catch (Exception ex) { UiErrors.Show(ex, "Excluir registro"); }
    }

    private void Action_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row) return;
        switch (_resource.Key)
        {
            case "movements":
                ShowActionEditor(row, "Registrar chegada",
                    [new("arrivalUtc", "Chegada", FieldKind.DateTime),
                     new("finalMileage", "KM final", FieldKind.Integer)],
                    $"movements/{row.Id}/complete", HttpMethod.Post);
                break;
            case "reservations":
                ShowActionEditor(row, "Iniciar viagem",
                    [new("initialMileage", "KM inicial", FieldKind.Integer),
                     new("description", "Descrição", Required: false)],
                    $"reservations/{row.Id}/start", HttpMethod.Post);
                break;
            case "maintenance-plans":
                ShowActionEditor(row, "Registrar execução",
                    [new("date", "Data", FieldKind.Date),
                     new("mileage", "KM", FieldKind.Integer),
                     new("amount", "Valor", FieldKind.Decimal),
                     new("notes", "Observações", Required: false)],
                    $"maintenance-plans/{row.Id}/complete", HttpMethod.Post);
                break;
        }
    }

    private async void SecondaryAction_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } row) return;
        if (_resource.Key == "movements")
        {
            var phase = "departure";
            if (row.Source["arrivalUtc"] is not null)
            {
                var choice = MessageBox.Show("Checklist de saída (Sim) ou de chegada (Não)?",
                    "Escolher checklist", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (choice == MessageBoxResult.Cancel) return;
                phase = choice == MessageBoxResult.Yes ? "departure" : "arrival";
            }
            try
            {
                var current = (await _api.GetArrayAsync($"movements/{row.Id}/checklists"))
                    .FirstOrDefault(x => x["phase"]?.ToString() == phase);
                if (current is null && _api.User?.CanWrite != true)
                {
                    MessageBox.Show("Ainda não há checklist registrado nesta etapa.",
                        "Checklist", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                ShowEditor(phase == "departure" ?
                    "Checklist de saída" : "Checklist de chegada",
                    [new("tiresOk", "Pneus OK", FieldKind.Boolean),
                     new("lightsOk", "Luzes OK", FieldKind.Boolean),
                     new("fluidsOk", "Fluidos OK", FieldKind.Boolean),
                     new("bodyOk", "Lataria OK", FieldKind.Boolean),
                     new("notes", "Observações", Required: false),
                     new("checkedAtUtc", "Conferido em", FieldKind.DateTime)], current,
                    _api.User?.CanWrite == true ? async value =>
                    {
                        await _api.SendAsync(HttpMethod.Put,
                            $"movements/{row.Id}/checklists/{phase}", value);
                        await RefreshAsync();
                    } : null, readOnly: _api.User?.CanWrite != true);
            }
            catch (Exception ex) { UiErrors.Show(ex, "Salvar checklist"); }
        }
        else if (_resource.Key == "reservations")
        {
            if (MessageBox.Show("Cancelar a reserva selecionada?", "Confirmar",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            try
            {
                await _api.SendAsync(HttpMethod.Post, $"reservations/{row.Id}/cancel");
                await RefreshAsync();
            }
            catch (Exception ex) { UiErrors.Show(ex, "Cancelar reserva"); }
        }
    }

    private void ThirdAction_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is { } row)
            ShowActionEditor(row, "Previsão de retorno",
                [new("expectedReturnUtc", "Retorno previsto", FieldKind.DateTime, false)],
                $"movements/{row.Id}/expected-return", HttpMethod.Put);
    }

    private void ShowActionEditor(GridRow row, string title, IReadOnlyList<Field> fields,
        string path, HttpMethod method)
    {
        ShowEditor(title, fields, row.Source, async value =>
        {
            await _api.SendAsync(method, path, value);
            await RefreshAsync();
        });
    }
}
