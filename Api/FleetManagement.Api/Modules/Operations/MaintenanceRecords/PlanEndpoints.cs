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
        group.MapGet("/{id:int:min(1)}", async (int id, MaintenancePlanQueries queries, CancellationToken ct) =>
            (await queries.GetAsync(id, ct)).ToHttpResult());
        group.MapPost("/", async (MaintenancePlanInput input, MaintenancePlanCommands commands,
            CancellationToken ct) =>
            (await commands.CreateAsync(input, ct)).ToCreatedHttpResult("/api/v1/maintenance-plans"));
        group.MapPut("/{id:int:min(1)}", async (int id, MaintenancePlanInput input,
            MaintenancePlanCommands commands, CancellationToken ct) =>
            (await commands.UpdateAsync(id, input, ct)).ToHttpResult());
        group.MapPost("/{id:int:min(1)}/complete", async (int id, CompletePlanInput input,
            MaintenancePlanCommands commands, CancellationToken ct) =>
            (await commands.CompleteAsync(id, input, ct)).ToHttpResult());
    }
}
