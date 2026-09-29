using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Movements;

namespace FleetManagement.Api.Modules.Operations.Movements;

public static class Endpoints
{
    public static void MapMovements(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/movements").WithTags("Movimentações");

        group.MapGet("/", async (MovementQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, MovementQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (MovementInput input, MovementCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/movements");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, MovementInput input, MovementCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/{id:int:min(1)}/complete", async (int id, CompleteMovementInput input,
            MovementCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.ConcludeAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, MovementCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
