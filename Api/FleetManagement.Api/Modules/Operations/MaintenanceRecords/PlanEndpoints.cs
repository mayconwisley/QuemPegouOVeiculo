using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.MaintenanceRecords;

namespace FleetManagement.Api.Modules.Operations.MaintenanceRecords;

public static class PlanEndpoints
{
    public static void MapMaintenancePlans(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/maintenance-plans").WithTags("Manutenção preventiva");
        group.MapGet("/", async (MaintenancePlanQueries queries, int page = 1, int pageSize = 50,
            CancellationToken ct = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), ct)).ToHttpResult());
        group.MapGet("/{id:guid}", async (Guid id, MaintenancePlanQueries queries, CancellationToken ct) =>
            (await queries.GetAsync(id, ct)).ToHttpResult());
        group.MapPost("/", async (MaintenancePlanInput input, MaintenancePlanCommands commands,
            CancellationToken ct) =>
            (await commands.CreateAsync(input, ct)).ToCreatedHttpResult("/api/v1/maintenance-plans"));
        group.MapPut("/{id:guid}", async (Guid id, MaintenancePlanInput input,
            MaintenancePlanCommands commands, CancellationToken ct) =>
            (await commands.UpdateAsync(id, input, ct)).ToHttpResult());
        group.MapPost("/{id:guid}/complete", async (Guid id, CompletePlanInput input,
            MaintenancePlanCommands commands, CancellationToken ct) =>
            (await commands.CompleteAsync(id, input, ct)).ToHttpResult());
    }
}
