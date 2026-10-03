using System.Globalization;
using System.IO;
using System.Printing;
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
        ReportBox.ItemsSource = ReportCatalog.All;
        ReportBox.SelectedIndex = 0;
        Loaded += async (_, _) => await PreviewAsync();
    }

    private ReportTemplate? Selected => ReportBox.SelectedItem as ReportTemplate;

    private string Filters(bool export = false)
    {
        if (Selected is null) return "";
        var dated = Selected.Key is "movements" or "vehicle-statuses";
        var dateRecords = Selected.Key is "refuelings" or "fines" or "maintenance";
        var reservation = Selected.Key == "reservations";
        var activeFilter = Selected.Key is "drivers" or "vehicles";
        var openFilter = Selected.Key is "movements" or "vehicle-statuses";
        var from = ParseDate(FromDate, "Data inicial");
        var to = ParseDate(ToDate, "Data final");
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

    private static DateTime? ParseDate(TextBox input, string label)
    {
        var value = input.Text.Trim();
        if (value.Length == 0) return null;
        if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"),
            DateTimeStyles.None, out var date))
            return date;
        throw new FormatException($"{label}: use dd/MM/aaaa.");
    }

    private static string AddPage(string filter, int page) => filter.Length == 0
        ? $"?page={page}&pageSize=100"
        : $"{filter}&page={page}&pageSize=100";

    private async Task PreviewAsync()
    {
        if (Selected is not { } report) return;
        try
        {
            ClearFilterError();
            EmptyState.Visibility = Visibility.Collapsed;
            StatusText.Text = "Carregando relatório...";
            var data = await _api.GetPageAsync(report.Resource.ListPath + AddPage(Filters(), _page));
            _total = data.Total;
            PreviewGrid.ItemsSource = data.Items.Select(x => new ReportRow(x)).ToArray();
            ReportSummaryText.Text = report.HasAmountTotal
                ? $"{_total:N0} registro(s) · total desta página: {AmountTotal(data.Items).ToString("C", CultureInfo.GetCultureInfo("pt-BR"))}"
                : $"{_total:N0} registro(s)";
            EmptyState.Visibility = data.Items.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            StatusText.Text = $"{_total:N0} registro(s) · página {_page}";
            PreviousButton.IsEnabled = _page > 1;
            NextButton.IsEnabled = _page * 100 < _total;
        }
        catch (FormatException ex)
        {
            ShowFilterError(ex.Message);
            StatusText.Text = "Corrija os filtros para visualizar o relatório.";
        }
        catch (Exception ex)
        {
            EmptyState.Visibility = Visibility.Collapsed;
            UiErrors.Show(ex, "Relatório");
        }
    }

    private void Report_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (PreviewGrid is null || Selected is not { } report) return;
        ReportTitleText.Text = report.Title;
        ReportDescriptionText.Text = report.Description;
        ReportSummaryText.Text = "";
        ClearFilterError();
        PreviewGrid.Columns.Clear();
        foreach (var column in report.Columns)
            PreviewGrid.Columns.Add(new DataGridTextColumn
            {
                Header = column.Label,
                Binding = new Binding($"[{column.Key}]") { Mode = BindingMode.OneWay },
                MinWidth = 120,
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
        var hasDate = report.Key is "movements" or "vehicle-statuses" or
            "refuelings" or "fines" or "maintenance" or "reservations";
        VehiclePanel.Visibility = report.Key is "movements" or "vehicle-statuses" or
            "refuelings" or "fines" or "maintenance" or "reservations"
            ? Visibility.Visible : Visibility.Collapsed;
        DriverPanel.Visibility = report.Key is "movements" or "refuelings" or "fines" or
            "license-expirations"
            ? Visibility.Visible : Visibility.Collapsed;
        StatePanel.Visibility = report.Key is "drivers" or "vehicles" or
            "movements" or "vehicle-statuses" or "reservations"
            ? Visibility.Visible : Visibility.Collapsed;
        DateFieldPanel.Visibility = report.Key is "movements" or "vehicle-statuses"
            ? Visibility.Visible : Visibility.Collapsed;
        DateRangePanel.Visibility = hasDate ? Visibility.Visible : Visibility.Collapsed;
        SearchBox.IsEnabled = report.Key is not ("reservations" or "maintenance-plans");
        SearchBox.Clear();
        StateBox.ItemsSource = report.Key == "reservations"
            ? new[] { "Todas", "Confirmadas", "Em uso", "Concluídas", "Canceladas" }
            : new[] { "Todos", "Ativos / abertos", "Inativos / concluídos" };
        DateFieldBox.ItemsSource = report.Key == "vehicle-statuses"
            ? new[] { "Início", "Fim" } : new[] { "Saída", "Chegada" };
        if (!hasDate) FromDate.Clear();
        if (!hasDate) ToDate.Clear();
        _vehicleId = _driverId = null;
        VehicleButton.Content = "Todos";
        DriverButton.Content = "Todos";
        StateBox.SelectedIndex = DateFieldBox.SelectedIndex = 0;
        _page = 1;
        PreviousButton.IsEnabled = NextButton.IsEnabled = false;
        if (IsLoaded) _ = PreviewAsync();
    }

    private async void Preview_Click(object sender, RoutedEventArgs e)
    {
        _page = 1;
        await PreviewAsync();
    }

    private void Vehicle_Click(object sender, RoutedEventArgs e)
    {
        ShowLookup(vehicles: true, selection =>
        {
            _vehicleId = selection.Id;
            VehicleButton.Content = selection.Label;
        });
    }

    private void Driver_Click(object sender, RoutedEventArgs e)
    {
        ShowLookup(vehicles: false, selection =>
        {
            _driverId = selection.Id;
            DriverButton.Content = selection.Label;
        });
    }

    private void ShowLookup(bool vehicles, Action<LookupView.LookupRow> select)
    {
        LookupHost.Content = new LookupView(_api, vehicles, selection =>
        {
            select(selection);
            CloseLookup();
        }, CloseLookup);
        ReportContent.Visibility = Visibility.Collapsed;
        LookupHost.Visibility = Visibility.Visible;
    }

    private void CloseLookup()
    {
        LookupHost.Content = null;
        LookupHost.Visibility = Visibility.Collapsed;
        ReportContent.Visibility = Visibility.Visible;
    }

    private async void Clear_Click(object sender, RoutedEventArgs e)
    {
        ClearFilterError();
        _vehicleId = _driverId = null;
        VehicleButton.Content = "Todos";
        DriverButton.Content = "Todos";
        SearchBox.Clear();
        FromDate.Clear();
        ToDate.Clear();
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
        string filters;
        try
        {
            filters = Filters(export: true);
            ClearFilterError();
        }
        catch (FormatException ex)
        {
            ShowFilterError(ex.Message);
            return;
        }
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
            var bytes = await _api.DownloadAsync($"exports/{report.Key}.csv" + filters);
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
        string filters;
        try
        {
            filters = Filters();
            ClearFilterError();
        }
        catch (FormatException ex)
        {
            ShowFilterError(ex.Message);
            return;
        }
        try
        {
            var print = new PrintDialog();
            if (report.Columns.Count > 7)
                print.PrintTicket.PageOrientation = PageOrientation.Landscape;
            if (print.ShowDialog() != true) return;
            var first = await _api.GetPageAsync(report.Resource.ListPath + AddPage(filters, 1));
            if (first.Total > 10000)
                throw new InvalidOperationException("A impressão permite até 10.000 registros. Aplique filtros.");
            var rows = first.Items.Select(x => new ReportRow(x)).ToList();
            for (var page = 2; rows.Count < first.Total; page++)
            {
                StatusText.Text = $"Preparando impressão: {rows.Count:N0} de {first.Total:N0}...";
                var next = await _api.GetPageAsync(report.Resource.ListPath + AddPage(filters, page));
                if (next.Items.Count == 0) break;
                rows.AddRange(next.Items.Select(x => new ReportRow(x)));
            }
            var document = CreateDocument(report, rows, print.PrintableAreaWidth);
            print.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, report.Title);
            StatusText.Text = $"{rows.Count:N0} registro(s) enviados para impressão.";
        }
        catch (Exception ex) { UiErrors.Show(ex, "Imprimir relatório"); }
    }

    private static FlowDocument CreateDocument(ReportTemplate report,
        IReadOnlyList<ReportRow> rows, double pageWidth)
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
        document.Blocks.Add(new Paragraph(new Run(report.Description))
        { FontSize = 10, Margin = new Thickness(0, 0, 0, 8) });
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
        if (report.HasAmountTotal)
        {
            var total = AmountTotal(rows.Select(row => row.Source));
            document.Blocks.Add(new Paragraph(new Run(
                $"Valor total: {total.ToString("C", CultureInfo.GetCultureInfo("pt-BR"))}"))
            {
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Right,
                Margin = new Thickness(0, 10, 0, 0)
            });
        }
        return document;
    }

    private static decimal AmountTotal(IEnumerable<System.Text.Json.Nodes.JsonObject> rows) =>
        rows.Sum(row => decimal.TryParse(row["amount"]?.ToString(),
            NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ? value : 0m);

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

    private void ShowFilterError(string message)
    {
        FilterErrorText.Text = message;
        FilterErrorText.Visibility = Visibility.Visible;
    }

    private void ClearFilterError()
    {
        FilterErrorText.Text = "";
        FilterErrorText.Visibility = Visibility.Collapsed;
    }
}
