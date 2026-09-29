using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.VehicleStatuses;

namespace FleetManagement.Api.Modules.Operations.VehicleStatuses;

public static class Endpoints
{
    public static void MapVehicleStatuses(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/vehicle-statuses").WithTags("Status do veículo");

        group.MapGet("/", async (VehicleStatusQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, VehicleStatusQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (VehicleStatusInput input, VehicleStatusCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/vehicle-statuses");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, VehicleStatusInput input, VehicleStatusCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, VehicleStatusCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
