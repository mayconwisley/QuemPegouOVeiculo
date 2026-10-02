using System.Globalization;
using System.Text.Json.Nodes;
using FleetManagement.Wpf.Features.Shared;

namespace FleetManagement.Wpf.Features.Reports;

public sealed record ReportTemplate(string Key, string Title, string Description,
    IReadOnlyList<Column> Columns, bool HasAmountTotal = false)
{
    public ResourceDefinition Resource => ResourceCatalog.Get(Key);
}

public static class ReportCatalog
{
    public static IReadOnlyList<ReportTemplate> All { get; } =
    [
        new("drivers", "Cadastro de Motoristas", "Nome, documentos e vencimento da CNH",
            [new("name", "Nome"), new("cpf", "CPF"), new("licenseNumber", "CNH"),
             new("licenseCategory", "Categoria CNH"), new("licenseExpiration", "Vencimento CNH"),
             new("rg", "RG"), new("active", "Ativo")]),
        new("vehicles", "Veículos Cadastrados", "Identificação e situação dos veículos",
            [new("plate", "Placa"), new("model", "Modelo"), new("renavam", "Renavam"),
             new("chassis", "Chassi"), new("active", "Ativo")]),
        new("vehicle-statuses", "Status Veículos", "Períodos e ocorrências por veículo",
            [new("model", "Veículo"), new("startUtc", "Data início"),
             new("endUtc", "Data final"), new("description", "Descrição")]),
        new("movements", "Controle de Veículos", "Saídas, chegadas e quilometragem",
            [new("departureUtc", "Data saída"), new("arrivalUtc", "Data chegada"),
             new("durationDays", "Dias"), new("durationHours", "Horas"),
             new("name", "Motorista"), new("model", "Veículo"),
             new("initialMileage", "KM inicial"), new("finalMileage", "KM final"),
             new("mileageTotal", "KM total"), new("description", "Descrição"),
             new("movementState", "Situação")]),
        new("maintenance", "Controle Manutenção", "Serviços e valores por veículo",
            [new("date", "Data"), new("model", "Veículo"),
             new("description", "Descrição"), new("amount", "Valor")], true),
        new("refuelings", "Controle Combustível", "Abastecimentos, litros e valores",
            [new("date", "Data"), new("model", "Veículo"), new("name", "Motorista"),
             new("mileage", "KM"), new("description", "Descrição"),
             new("amount", "Valor"), new("liters", "Litros")], true),
        new("fines", "Controle Multas", "Infrações, pontos e valores",
            [new("date", "Data"), new("name", "Motorista"), new("model", "Veículo"),
             new("description", "Descrição"), new("points", "Pontos"),
             new("amount", "Valor")], true),
        new("license-expirations", "Vencimentos de CNH", "Alertas e histórico de vencimentos",
            ResourceCatalog.Get("license-expirations").Columns),
        new("reservations", "Reservas", "Programação de uso dos veículos",
            ResourceCatalog.Get("reservations").Columns),
        new("maintenance-plans", "Manutenção Preventiva", "Planos e próximas revisões",
            ResourceCatalog.Get("maintenance-plans").Columns)
    ];
}

public sealed class ReportRow(JsonObject source)
{
    private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");
    private readonly GridRow _row = new(source);

    public JsonObject Source => source;
    public string this[string key] => key switch
    {
        "durationDays" => Duration()?.Days.ToString(PtBr) ?? "",
        "durationHours" => Duration()?.Hours.ToString(PtBr) ?? "",
        "mileageTotal" => MileageTotal(),
        "movementState" => source["arrivalUtc"] is null ? "Em aberto" : "Concluída",
        _ => _row[key]
    };

    private TimeSpan? Duration()
    {
        if (!DateTimeOffset.TryParse(source["departureUtc"]?.ToString(), out var departure)
            || !DateTimeOffset.TryParse(source["arrivalUtc"]?.ToString(), out var arrival))
            return null;
        return arrival - departure;
    }

    private string MileageTotal() =>
        int.TryParse(source["initialMileage"]?.ToString(), out var initial)
        && int.TryParse(source["finalMileage"]?.ToString(), out var final)
            ? (final - initial).ToString("N0", PtBr) : "";
}
