using System.Globalization;
using System.Text;
using FleetManagement.Api.Common;
using FleetManagement.Api.Modules.Queries;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.MaintenanceRecords;
using FleetManagement.Application.Modules.Operations.Reservations;
using FleetManagement.Application.Modules.Queries;

namespace FleetManagement.Api.Modules.Exports;

public static class Endpoints
{
    private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");

    public static void MapExports(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/exports/{resource}.csv", async (string resource,
            [AsParameters] FleetQueryParameters p, HttpContext context, FleetQueries queries,
            ReservationQueries reservations, MaintenancePlanQueries plans, CancellationToken ct) =>
        {
            var filter = p.Filter;
            return resource switch
            {
                "drivers" => await WriteAsync(context, resource,
                    ["ID", "Nome", "CNH", "Vencimento da CNH", "Categoria", "CPF", "RG", "Ativo"],
                    page => queries.DriversAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Name, x.LicenseNumber, Date(x.LicenseExpiration),
                        x.LicenseCategory, x.Cpf, x.Rg, YesNo(x.Active)], ct),
                "vehicles" => await WriteAsync(context, resource,
                    ["ID", "Placa", "Modelo", "Chassi", "Renavam", "Ativo"],
                    page => queries.VehiclesAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Plate, x.Model, x.Chassis, x.Renavam, YesNo(x.Active)], ct),
                "movements" => await WriteAsync(context, resource,
                    ["ID", "Placa", "Veículo", "Motorista", "Saída (UTC)", "Retorno previsto (UTC)",
                        "Chegada (UTC)", "KM inicial", "KM final", "Descrição"],
                    page => queries.MovementsAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Plate, x.Model, x.Name, Utc(x.DepartureUtc),
                        Utc(x.ExpectedReturnUtc), Utc(x.ArrivalUtc), Id(x.InitialMileage),
                        x.FinalMileage?.ToString(PtBr), x.Description], ct),
                "refuelings" => await WriteAsync(context, resource,
                    ["ID", "Veículo", "Motorista", "Data", "KM", "Valor", "Litros", "Descrição"],
                    page => queries.RefuelingsAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Model, x.Name, Date(x.Date), Id(x.Mileage), Money(x.Amount),
                        x.Liters.ToString("0.000", PtBr), x.Description], ct),
                "fines" => await WriteAsync(context, resource,
                    ["ID", "Veículo", "Motorista", "Data", "Valor", "Pontos", "Descrição"],
                    page => queries.FinesAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Model, x.Name, Date(x.Date), Money(x.Amount),
                        Id(x.Points), x.Description], ct),
                "maintenance" => await WriteAsync(context, resource,
                    ["ID", "Veículo", "Data", "Valor", "Descrição"],
                    page => queries.MaintenanceAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Model, Date(x.Date), Money(x.Amount), x.Description], ct),
                "vehicle-statuses" => await WriteAsync(context, resource,
                    ["ID", "Veículo", "Início (UTC)", "Fim (UTC)", "Descrição"],
                    page => queries.VehicleStatusesAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Model, Utc(x.StartUtc), Utc(x.EndUtc), x.Description], ct),
                "license-expirations" => await WriteAsync(context, resource,
                    ["ID", "Motorista", "Data", "Vencido"],
                    page => queries.LicenseExpirationsAsync(filter, new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Name, Date(x.Date), YesNo(x.Expired)], ct),
                "reservations" => await WriteAsync(context, resource,
                    ["ID", "Placa", "Veículo", "Motorista", "Início (UTC)", "Fim (UTC)",
                        "Finalidade", "Status", "Movimentação ID"],
                    page => reservations.ListAsync(new ReservationFilter(p.VehicleId, p.Status,
                        p.StartUtc, p.EndUtc), new PageRequest(page, 100), ct),
                    x => [Id(x.Id), x.Plate, x.VehicleModel, x.DriverName, Utc(x.StartUtc),
                        Utc(x.EndUtc), x.Purpose, Status(x.Status), x.MovementId?.ToString(PtBr)], ct),
                "maintenance-plans" => await WriteAsync(context, resource,
                    ["ID", "Veículo ID", "Serviço", "Intervalo dias", "Intervalo km",
                        "Próxima data", "Próximo km", "Ativo"],
                    page => plans.ListAsync(new PageRequest(page, 100), ct),
                    x => [Id(x.Id), Id(x.VehicleId), x.Name, x.IntervalDays?.ToString(PtBr),
                        x.IntervalMileage?.ToString(PtBr), x.NextDueDate is null ? null : Date(x.NextDueDate.Value),
                        x.NextDueMileage?.ToString(PtBr), YesNo(x.IsActive)], ct),
                _ => Results.Problem(statusCode: 404, detail: "Recurso de exportação não encontrado.")
            };
        }).WithTags("Exportação");
    }

    private static async Task<IResult> WriteAsync<T>(HttpContext context, string resource,
        string[] headers, Func<int, Task<Result<PagedResult<T>>>> fetch,
        Func<T, string?[]> row, CancellationToken ct)
    {
        const int maxRows = 10000;
        var first = await fetch(1);
        if (!first.IsSuccess)
            return first.ToHttpResult();
        if (first.Value.Total > maxRows)
            return Results.Problem(statusCode: 413,
                detail: $"A exportação permite até {maxRows.ToString("N0", PtBr)} linhas. Reduza o período ou aplique filtros.");

        context.Response.ContentType = "text/csv; charset=utf-8";
        context.Response.Headers["Content-Disposition"] =
            $"attachment; filename=\"{resource}-{DateTime.UtcNow:yyyyMMdd}.csv\"";
        context.Response.Headers["Cache-Control"] = "no-store";
        await using var writer = new StreamWriter(context.Response.Body, new UTF8Encoding(true),
            bufferSize: 16 * 1024, leaveOpen: true);
        await WriteRowAsync(writer, headers, ct);
        var current = first.Value;
        var page = 1;
        while (true)
        {
            foreach (var item in current.Items)
                await WriteRowAsync(writer, row(item), ct);
            if (page * 100 >= first.Value.Total)
                break;
            page++;
            var next = await fetch(page);
            if (!next.IsSuccess)
                throw new InvalidOperationException(next.Error.Message);
            current = next.Value;
        }
        await writer.FlushAsync(ct);
        return Results.Empty;
    }

    private static Task WriteRowAsync(StreamWriter writer, IEnumerable<string?> values,
        CancellationToken ct) => writer.WriteLineAsync(string.Join(";", values.Select(Escape)).AsMemory(), ct);

    private static string Escape(string? value)
    {
        var text = value ?? "";
        var leading = text.TrimStart(' ', '\t', '\r', '\n');
        if (leading.Length > 0 && leading[0] is '=' or '+' or '-' or '@')
            text = "'" + text;
        return "\"" + text.Replace("\"", "\"\"") + "\"";
    }

    private static string Id(int value) => value.ToString(PtBr);
    private static string Money(decimal value) => value.ToString("0.00", PtBr);
    private static string Date(DateOnly value) => value.ToString("dd/MM/yyyy", PtBr);
    private static string Utc(DateTime value) => value.ToString("O", CultureInfo.InvariantCulture);
    private static string? Utc(DateTime? value) => value is null ? null : Utc(value.Value);
    private static string YesNo(bool value) => value ? "Sim" : "Não";
    private static string Status(string value) => value switch
    {
        "Confirmed" => "Confirmada",
        "InUse" => "Em uso",
        "Completed" => "Concluída",
        "Cancelled" => "Cancelada",
        _ => value
    };
}
