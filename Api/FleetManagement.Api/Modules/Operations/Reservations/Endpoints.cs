using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Reservations;

namespace FleetManagement.Api.Modules.Operations.Reservations;

public static class Endpoints
{
    public static void MapReservations(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/reservations").WithTags("Reservas");
        group.MapGet("/", async ([AsParameters] ReservationQueryParameters p,
            ReservationQueries queries, CancellationToken ct) =>
            (await queries.ListAsync(p.Filter, p.Pagination, ct)).ToHttpResult());
        group.MapGet("/{id:guid}", async (Guid id, ReservationQueries queries,
            CancellationToken ct) =>
            (await queries.GetAsync(id, ct)).ToHttpResult());
        group.MapPost("/", async (ReservationInput input, ReservationCommands commands,
            CancellationToken ct) =>
            (await commands.CreateAsync(input, ct)).ToCreatedHttpResult("/api/v1/reservations"));
        group.MapPut("/{id:guid}", async (Guid id, ReservationInput input,
            ReservationCommands commands, CancellationToken ct) =>
            (await commands.UpdateAsync(id, input, ct)).ToHttpResult());
        group.MapPost("/{id:guid}/cancel", async (Guid id, ReservationCommands commands,
            CancellationToken ct) =>
            (await commands.CancelAsync(id, ct)).ToHttpResult());
        group.MapPost("/{id:guid}/start", async (Guid id, StartReservationInput input,
            ReservationCommands commands, CancellationToken ct) =>
            (await commands.StartAsync(id, input, ct)).ToCreatedHttpResult("/api/v1/movements"));
    }
}

public sealed class ReservationQueryParameters
{
    public Guid? VehicleId { get; set; }
    public string? Status { get; set; }
    public DateTime? FromUtc { get; set; }
    public DateTime? ToUtc { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public ReservationFilter Filter => new(VehicleId, Status, FromUtc, ToUtc);
    public PageRequest Pagination => new(Page ?? 1, PageSize ?? 50);
}
