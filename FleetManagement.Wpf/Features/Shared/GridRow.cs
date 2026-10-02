using System.Globalization;
using System.Text.Json.Nodes;

namespace FleetManagement.Wpf.Features.Shared;

public sealed class GridRow
{
    private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");

    public GridRow(JsonObject source) => Source = source;

    public JsonObject Source { get; }
    public int Id => Source["id"]?.GetValue<int>() ?? 0;

    public string this[string key] => Format(Source[key], key);

    private static string Format(JsonNode? node, string key)
    {
        if (node is null)
            return "";
        var raw = node.ToString();
        if (bool.TryParse(raw, out var flag))
            return flag ? "Sim" : "Não";
        if (key == "status")
            return raw switch
            {
                "Confirmed" => "Confirmada", "InUse" => "Em uso",
                "Completed" => "Concluída", "Cancelled" => "Cancelada", _ => raw
            };
        if (key.EndsWith("Utc", StringComparison.Ordinal) &&
            DateTimeOffset.TryParse(raw, CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal, out var instant))
            return instant.ToLocalTime().ToString("dd/MM/yyyy HH:mm", PtBr);
        if ((key == "date" || key.EndsWith("Date", StringComparison.Ordinal)) &&
            DateOnly.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return date.ToString("dd/MM/yyyy", PtBr);
        if (key is "amount" or "liters" && decimal.TryParse(raw,
            NumberStyles.Any, CultureInfo.InvariantCulture, out var amount))
            return amount.ToString("N2", PtBr);
        return raw;
    }
}
