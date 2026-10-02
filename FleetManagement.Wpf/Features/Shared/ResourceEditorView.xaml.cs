using System.Globalization;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using FleetManagement.Wpf.Infrastructure.Api;

namespace FleetManagement.Wpf.Features.Shared;

public partial class ResourceEditorView : UserControl
{
    private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");
    private readonly FleetApiClient _api;
    private readonly IReadOnlyList<Field> _fields;
    private readonly JsonObject? _original;
    private readonly string? _resourceKey;
    private readonly bool _readOnly;
    private readonly Func<JsonObject, Task>? _submit;
    private readonly Action _close;
    private readonly Dictionary<string, Control> _controls = [];

    public ResourceEditorView(FleetApiClient api, string title, IReadOnlyList<Field> fields,
        Func<JsonObject, Task>? submit, Action close, JsonObject? original = null,
        string? resourceKey = null, bool readOnly = false)
    {
        InitializeComponent();
        _api = api;
        _fields = fields;
        _original = original;
        _resourceKey = resourceKey;
        _readOnly = readOnly;
        _submit = submit;
        _close = close;
        TitleText.Text = title;
        DescriptionText.Text = readOnly ? "Dados do registro selecionado" :
            "Preencha os campos obrigatórios marcados com *";
        BuildFields();
        if (_readOnly)
        {
            SaveButton.Visibility = Visibility.Collapsed;
            CancelButton.Content = "Voltar";
            foreach (var control in _controls.Values)
            {
                if (control is TextBox text)
                    text.IsReadOnly = true;
                else
                    control.IsEnabled = false;
            }
        }
        if (_original is not null && _resourceKey is "reservations" or "maintenance-plans" &&
            _controls.TryGetValue("vehicleId", out var vehicle))
            vehicle.IsEnabled = false;
    }

    private void BuildFields()
    {
        foreach (var field in _fields)
        {
            var label = new TextBlock
            {
                Text = field.Label + (field.Required ? " *" : "") +
                    (field.Kind == FieldKind.Date ? " · dd/MM/aaaa" :
                     field.Kind == FieldKind.DateTime ? " · dd/MM/aaaa HH:mm" : ""),
                Style = (Style)FindResource("FieldLabelStyle")
            };
            var raw = _original?[field.Key]?.ToString();
            Control control = field.Kind switch
            {
                FieldKind.Boolean => new CheckBox
                {
                    IsChecked = raw is null ? field.Key is "active" or "isActive" :
                        bool.TryParse(raw, out var value) && value,
                    Content = "Sim", Margin = new Thickness(0, 7, 0, 7)
                },
                FieldKind.Vehicle or FieldKind.Driver => CreateLookupButton(field, raw),
                _ => new TextBox { Text = InitialText(field, raw) }
            };
            _controls.Add(field.Key, control);
            FieldsPanel.Children.Add(label);
            FieldsPanel.Children.Add(control);
        }
    }

    private Button CreateLookupButton(Field field, string? raw)
    {
        var button = new Button
        {
            Content = int.TryParse(raw, out var id) ? $"Selecionado: #{id}" : "Selecionar...",
            Tag = int.TryParse(raw, out var selectedId) ? selectedId : null,
            HorizontalContentAlignment = HorizontalAlignment.Left
        };
        button.Click += async (_, _) =>
        {
            var picker = new LookupDialog(_api, field.Kind == FieldKind.Vehicle)
            { Owner = Window.GetWindow(this) };
            if (picker.ShowDialog() != true) return;
            button.Tag = picker.SelectedId;
            button.Content = picker.SelectedLabel;
            if (field.Kind == FieldKind.Vehicle)
                await PrefillMileageAsync(picker.SelectedId);
        };
        return button;
    }

    private async Task PrefillMileageAsync(int vehicleId)
    {
        if (_original is not null) return;
        var (key, source) = _resourceKey switch
        {
            "movements" => ("initialMileage", "movement"),
            "refuelings" => ("mileage", "refueling"),
            _ => (null, null)
        };
        if (key is null || !_controls.TryGetValue(key, out var control) ||
            control is not TextBox { Text.Length: 0 } mileage)
            return;
        try
        {
            var result = await _api.GetAsync(
                $"queries/vehicles/{vehicleId}/latest-mileage?source={source}");
            if (result["mileage"] is not null)
                mileage.Text = result["mileage"]!.ToString();
        }
        catch (Exception ex) { UiErrors.Show(ex, "Consultar quilometragem"); }
    }

    private static string InitialText(Field field, string? raw)
    {
        if (raw is null)
            return field.Required ? field.Kind switch
            {
                FieldKind.Date => DateTime.Today.ToString("dd/MM/yyyy", PtBr),
                FieldKind.DateTime => DateTime.Now.ToString("dd/MM/yyyy HH:mm", PtBr),
                _ => ""
            } : "";
        if (field.Kind == FieldKind.Date && DateOnly.TryParse(raw, out var date))
            return date.ToString("dd/MM/yyyy", PtBr);
        if (field.Kind == FieldKind.DateTime && DateTimeOffset.TryParse(raw, out var utc))
            return utc.ToLocalTime().ToString("dd/MM/yyyy HH:mm", PtBr);
        if (field.Kind == FieldKind.Decimal && decimal.TryParse(raw,
            NumberStyles.Number, CultureInfo.InvariantCulture, out var number))
            return number.ToString(PtBr);
        return raw;
    }

    private async void Save_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var value = new JsonObject();
            foreach (var field in _fields)
                value[field.Key] = Read(field);
            if (_submit is null) return;
            SaveButton.IsEnabled = false;
            ErrorText.Visibility = Visibility.Collapsed;
            await _submit(value);
            _close();
        }
        catch (FormatException ex)
        {
            ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
        finally
        {
            SaveButton.IsEnabled = true;
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e) => _close();

    private JsonNode? Read(Field field)
    {
        var control = _controls[field.Key];
        if (control is CheckBox check)
            return JsonValue.Create(check.IsChecked == true);
        if (control is Button button)
        {
            if (button.Tag is int id)
                return JsonValue.Create(id);
            throw new FormatException($"Selecione {field.Label.ToLower(PtBr)}.");
        }
        var raw = ((TextBox)control).Text.Trim();
        if (raw.Length == 0)
        {
            if (!field.Required)
                return null;
            throw new FormatException($"Informe {field.Label.ToLower(PtBr)}.");
        }

        return field.Kind switch
        {
            FieldKind.Integer => int.TryParse(raw, NumberStyles.Integer, PtBr, out var integer)
                ? JsonValue.Create(integer) : throw new FormatException($"{field.Label}: número inteiro inválido."),
            FieldKind.Decimal => decimal.TryParse(raw, NumberStyles.Number, PtBr, out var number)
                ? JsonValue.Create(number) : throw new FormatException($"{field.Label}: valor inválido."),
            FieldKind.Date => DateOnly.TryParseExact(raw, "dd/MM/yyyy", PtBr,
                DateTimeStyles.None, out var date)
                ? JsonValue.Create(date.ToString("yyyy-MM-dd"))
                : throw new FormatException($"{field.Label}: use dd/MM/aaaa."),
            FieldKind.DateTime => DateTime.TryParseExact(raw,
                ["dd/MM/yyyy HH:mm", "dd/MM/yyyy HH:mm:ss"], PtBr, DateTimeStyles.None, out var local)
                ? JsonValue.Create(DateTime.SpecifyKind(local, DateTimeKind.Local).ToUniversalTime()
                    .ToString("O", CultureInfo.InvariantCulture))
                : throw new FormatException($"{field.Label}: use dd/MM/aaaa HH:mm."),
            _ => JsonValue.Create(raw)
        };
    }

    private void ShowError(string message)
    {
        ErrorText.Text = message;
        ErrorText.Visibility = Visibility.Visible;
    }

}
