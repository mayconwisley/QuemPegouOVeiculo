using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.MaintenanceRecords;

namespace FleetManagement.Api.Modules.Operations.MaintenanceRecords;

public static class Endpoints
{
    public static void MapMaintenance(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/maintenance").WithTags("Manutenções");

        group.MapGet("/", async (MaintenanceQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:guid}", async (Guid id, MaintenanceQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (MaintenanceInput input, MaintenanceCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/maintenance");
        });

        group.MapPut("/{id:guid}", async (Guid id, MaintenanceInput input, MaintenanceCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, MaintenanceCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
