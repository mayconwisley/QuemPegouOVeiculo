using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Queries;

namespace FleetManagement.Api.Modules.Queries;

public sealed class FleetQueryParameters
{
    public string? Search { get; set; }
    public int? VehicleId { get; set; }
    public int? DriverId { get; set; }
    public bool? Active { get; set; }
    public bool? IsOpen { get; set; }
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public DateTime? StartUtc { get; set; }
    public DateTime? EndUtc { get; set; }
    public string? DateField { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public FleetQueryFilter Filter => new(Search, VehicleId, DriverId, Active, IsOpen,
        FromDate, ToDate, StartUtc, EndUtc, DateField);
    public PageRequest Pagination => new(Page ?? 1, PageSize ?? 50);
}

public static class Endpoints
{
    public static void MapQueries(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/queries").WithTags("Consultas da frota");

        group.MapGet("/drivers", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.DriversAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/vehicles", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.VehiclesAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/movements", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.MovementsAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/refuelings", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.RefuelingsAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/fines", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.FinesAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/maintenance", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.MaintenanceAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/vehicle-statuses", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.VehicleStatusesAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/license-expirations", async ([AsParameters] FleetQueryParameters p, FleetQueries queries,
            CancellationToken ct) =>
            (await queries.LicenseExpirationsAsync(p.Filter, p.Pagination, ct)).ToHttpResult());

        group.MapGet("/vehicles/{id:int:min(1)}/latest-mileage",
            async (int id, string source, FleetQueries queries, CancellationToken ct) =>
                (await queries.LatestMileageAsync(id, source, ct)).ToHttpResult());
    }
}
