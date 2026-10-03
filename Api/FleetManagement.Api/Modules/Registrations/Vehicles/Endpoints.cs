using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Registrations.Vehicles;

namespace FleetManagement.Api.Modules.Registrations.Vehicles;

public static class Endpoints
{
    public static void MapVehicles(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/vehicles").WithTags("Veículos");

        group.MapGet("/", async (VehicleQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:guid}", async (Guid id, VehicleQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (VehicleInput input, VehicleCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/vehicles");
        });

        group.MapPut("/{id:guid}", async (Guid id, VehicleInput input, VehicleCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, VehicleCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
