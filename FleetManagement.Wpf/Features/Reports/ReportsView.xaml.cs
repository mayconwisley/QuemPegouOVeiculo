using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using FleetManagement.Wpf.Features.Shared;
using FleetManagement.Wpf.Infrastructure.Api;
using Microsoft.Win32;

namespace FleetManagement.Wpf.Features.Reports;

public partial class ReportsView : UserControl
{
    private readonly FleetApiClient _api;
    private int _page = 1;
    private int _total;
    private int? _vehicleId;
    private int? _driverId;

    public ReportsView(FleetApiClient api)
    {
        InitializeComponent();
        _api = api;
        ReportBox.ItemsSource = ResourceCatalog.Reports;
        ReportBox.SelectedIndex = 0;
        Loaded += async (_, _) => await PreviewAsync();
    }

    private ResourceDefinition? Selected => ReportBox.SelectedItem as ResourceDefinition;

    private string Filters(bool export = false)
    {
        if (Selected is null) return "";
        var dated = Selected.Key is "movements" or "vehicle-statuses";
        var dateRecords = Selected.Key is "refuelings" or "fines" or "maintenance";
        var reservation = Selected.Key == "reservations";
        var activeFilter = Selected.Key is "drivers" or "vehicles";
        var openFilter = Selected.Key is "movements" or "vehicle-statuses";
        var from = FromDate.SelectedDate;
        var to = ToDate.SelectedDate;
        if (from.HasValue && to.HasValue && from > to)
            throw new FormatException("A data inicial deve ser anterior à final.");
        return FleetApiClient.Query(
            ("search", SearchBox.Text.Trim()),
            ("vehicleId", _vehicleId?.ToString(CultureInfo.InvariantCulture)),
            ("driverId", _driverId?.ToString(CultureInfo.InvariantCulture)),
            ("active", activeFilter ? StateValue() : null),
            ("isOpen", openFilter ? StateValue() : null),
            ("status", reservation ? ReservationStatus() : null),
            ("dateField", dated ? DateFieldValue() : null),
            ("fromDate", dateRecords && from.HasValue ? from.Value.ToString("yyyy-MM-dd") : null),
            ("toDate", dateRecords && to.HasValue ? to.Value.ToString("yyyy-MM-dd") : null),
            ("startUtc", (dated || reservation && export) && from.HasValue
                ? LocalDayUtc(from.Value) : null),
            ("endUtc", (dated || reservation && export) && to.HasValue
                ? LocalDayUtc(to.Value.AddDays(1)) : null),
            ("fromUtc", reservation && !export && from.HasValue
                ? LocalDayUtc(from.Value) : null),
            ("toUtc", reservation && !export && to.HasValue
                ? LocalDayUtc(to.Value.AddDays(1)) : null));
    }

    private string? StateValue() => StateBox.SelectedIndex switch
    {
        1 => "true", 2 => "false", _ => null
    };

    private string? ReservationStatus() => StateBox.SelectedIndex switch
    {
        1 => "Confirmed", 2 => "InUse", 3 => "Completed", 4 => "Cancelled", _ => null
    };

    private string DateFieldValue() => Selected?.Key switch
    {
        "movements" => DateFieldBox.SelectedIndex == 1 ? "arrival" : "departure",
        "vehicle-statuses" => DateFieldBox.SelectedIndex == 1 ? "end" : "start",
        _ => "departure"
    };

    private static string LocalDayUtc(DateTime date) =>
        DateTime.SpecifyKind(date.Date, DateTimeKind.Local).ToUniversalTime()
            .ToString("O", CultureInfo.InvariantCulture);

    private static string AddPage(string filter, int page) => filter.Length == 0
        ? $"?page={page}&pageSize=100"
        : $"{filter}&page={page}&pageSize=100";

    private async Task PreviewAsync()
    {
        if (Selected is not { } report) return;
        try
        {
            StatusText.Text = "Carregando relatório...";
            var data = await _api.GetPageAsync(report.ListPath + AddPage(Filters(), _page));
            _total = data.Total;
            PreviewGrid.ItemsSource = data.Items.Select(x => new GridRow(x)).ToArray();
            StatusText.Text = $"{_total:N0} registro(s) · página {_page}";
        }
        catch (Exception ex) { UiErrors.Show(ex, "Relatório"); }
    }

    private void Report_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (PreviewGrid is null || Selected is not { } report) return;
        PreviewGrid.Columns.Clear();
        foreach (var column in report.Columns)
            PreviewGrid.Columns.Add(new DataGridTextColumn
            {
                Header = column.Label,
                Binding = new Binding($"[{column.Key}]") { Mode = BindingMode.OneWay },
                MinWidth = 85,
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
        var hasDate = report.Key is "movements" or "vehicle-statuses" or
            "refuelings" or "fines" or "maintenance" or "reservations";
        VehicleButton.Visibility = report.Key is "movements" or "vehicle-statuses" or
            "refuelings" or "fines" or "maintenance" or "reservations"
            ? Visibility.Visible : Visibility.Collapsed;
        DriverButton.Visibility = report.Key is "movements" or "refuelings" or "fines" or
            "license-expirations"
            ? Visibility.Visible : Visibility.Collapsed;
        StateBox.Visibility = report.Key is "drivers" or "vehicles" or
            "movements" or "vehicle-statuses" or "reservations"
            ? Visibility.Visible : Visibility.Collapsed;
        DateFieldBox.Visibility = report.Key is "movements" or "vehicle-statuses"
            ? Visibility.Visible : Visibility.Collapsed;
        FromDate.IsEnabled = ToDate.IsEnabled = hasDate;
        SearchBox.IsEnabled = report.Key is not ("reservations" or "maintenance-plans");
        SearchBox.Clear();
        StateBox.ItemsSource = report.Key == "reservations"
            ? new[] { "Todas", "Confirmadas", "Em uso", "Concluídas", "Canceladas" }
            : new[] { "Todos", "Ativos / abertos", "Inativos / concluídos" };
        DateFieldBox.ItemsSource = report.Key == "vehicle-statuses"
            ? new[] { "Início", "Fim" } : new[] { "Saída", "Chegada" };
        if (!hasDate) FromDate.SelectedDate = ToDate.SelectedDate = null;
        _vehicleId = _driverId = null;
        VehicleButton.Content = "Veículo: todos";
        DriverButton.Content = "Motorista: todos";
        StateBox.SelectedIndex = DateFieldBox.SelectedIndex = 0;
        _page = 1;
        if (IsLoaded) _ = PreviewAsync();
    }

    private async void Preview_Click(object sender, RoutedEventArgs e)
    {
        _page = 1;
        await PreviewAsync();
    }

    private void Vehicle_Click(object sender, RoutedEventArgs e)
    {
        var picker = new LookupDialog(_api, vehicles: true)
        { Owner = Window.GetWindow(this) };
        if (picker.ShowDialog() != true) return;
        _vehicleId = picker.SelectedId;
        VehicleButton.Content = picker.SelectedLabel;
    }

    private void Driver_Click(object sender, RoutedEventArgs e)
    {
        var picker = new LookupDialog(_api, vehicles: false)
        { Owner = Window.GetWindow(this) };
        if (picker.ShowDialog() != true) return;
        _driverId = picker.SelectedId;
        DriverButton.Content = picker.SelectedLabel;
    }

    private async void Clear_Click(object sender, RoutedEventArgs e)
    {
        _vehicleId = _driverId = null;
        VehicleButton.Content = "Veículo: todos";
        DriverButton.Content = "Motorista: todos";
        SearchBox.Clear();
        FromDate.SelectedDate = ToDate.SelectedDate = null;
        StateBox.SelectedIndex = DateFieldBox.SelectedIndex = 0;
        _page = 1;
        await PreviewAsync();
    }

    private async void Previous_Click(object sender, RoutedEventArgs e)
    {
        if (_page > 1) _page--;
        await PreviewAsync();
    }

    private async void Next_Click(object sender, RoutedEventArgs e)
    {
        if (_page * 100 < _total) _page++;
        await PreviewAsync();
    }

    private async void Export_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } report) return;
        var save = new SaveFileDialog
        {
            Filter = "Arquivo CSV (*.csv)|*.csv",
            FileName = $"{report.Key}-{DateTime.Today:yyyyMMdd}.csv"
        };
        if (save.ShowDialog(Window.GetWindow(this)) != true) return;
        var temporaryFile = save.FileName + "." + Guid.NewGuid().ToString("N") + ".tmp";
        try
        {
            StatusText.Text = "Exportando...";
            var bytes = await _api.DownloadAsync($"exports/{report.Key}.csv" + Filters(export: true));
            await File.WriteAllBytesAsync(temporaryFile, bytes);
            File.Move(temporaryFile, save.FileName, overwrite: true);
            StatusText.Text = $"Arquivo salvo: {save.FileName}";
        }
        catch (Exception ex) { UiErrors.Show(ex, "Exportar relatório"); }
        finally
        {
            if (File.Exists(temporaryFile))
                File.Delete(temporaryFile);
        }
    }

    private async void Print_Click(object sender, RoutedEventArgs e)
    {
        if (Selected is not { } report) return;
        var print = new PrintDialog();
        if (print.ShowDialog() != true) return;
        try
        {
            var filters = Filters();
            var first = await _api.GetPageAsync(report.ListPath + AddPage(filters, 1));
            if (first.Total > 10000)
                throw new InvalidOperationException("A impressão permite até 10.000 registros. Aplique filtros.");
            var rows = first.Items.Select(x => new GridRow(x)).ToList();
            for (var page = 2; rows.Count < first.Total; page++)
            {
                StatusText.Text = $"Preparando impressão: {rows.Count:N0} de {first.Total:N0}...";
                var next = await _api.GetPageAsync(report.ListPath + AddPage(filters, page));
                if (next.Items.Count == 0) break;
                rows.AddRange(next.Items.Select(x => new GridRow(x)));
            }
            var document = CreateDocument(report, rows, print.PrintableAreaWidth);
            print.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, report.Title);
            StatusText.Text = $"{rows.Count:N0} registro(s) enviados para impressão.";
        }
        catch (Exception ex) { UiErrors.Show(ex, "Imprimir relatório"); }
    }

    private static FlowDocument CreateDocument(ResourceDefinition report,
        IReadOnlyList<GridRow> rows, double pageWidth)
    {
        var document = new FlowDocument
        {
            FontFamily = new FontFamily("Segoe UI"), FontSize = 9,
            PageWidth = pageWidth, PagePadding = new Thickness(26),
            ColumnWidth = pageWidth
        };
        document.Blocks.Add(new Paragraph(new Run(report.Title))
        {
            FontSize = 19, FontWeight = FontWeights.Bold
        });
        document.Blocks.Add(new Paragraph(new Run(
            $"Emitido em {DateTime.Now:dd/MM/yyyy HH:mm} · {rows.Count:N0} registro(s)"))
        { Foreground = Brushes.DimGray });

        var table = new Table { CellSpacing = 0 };
        foreach (var _ in report.Columns)
            table.Columns.Add(new TableColumn());
        var group = new TableRowGroup();
        table.RowGroups.Add(group);
        var heading = new TableRow { Background = Brushes.LightGray };
        foreach (var column in report.Columns)
            heading.Cells.Add(Cell(column.Label, true));
        group.Rows.Add(heading);
        foreach (var row in rows)
        {
            var tableRow = new TableRow();
            foreach (var column in report.Columns)
                tableRow.Cells.Add(Cell(row[column.Key], false));
            group.Rows.Add(tableRow);
        }
        document.Blocks.Add(table);
        return document;
    }

    private static TableCell Cell(string text, bool bold) => new(new Paragraph(new Run(text))
    {
        Margin = new Thickness(0)
    })
    {
        Padding = new Thickness(4),
        BorderBrush = Brushes.LightGray,
        BorderThickness = new Thickness(0, 0, 0, 0.5),
        FontWeight = bold ? FontWeights.Bold : FontWeights.Normal
    };
}
