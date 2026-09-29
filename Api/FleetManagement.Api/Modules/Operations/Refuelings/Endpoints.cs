using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Refuelings;

namespace FleetManagement.Api.Modules.Operations.Refuelings;

public static class Endpoints
{
    public static void MapRefuelings(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/refuelings").WithTags("Abastecimentos");

        group.MapGet("/", async (RefuelingQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, RefuelingQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (RefuelingInput input, RefuelingCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/refuelings");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, RefuelingInput input, RefuelingCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, RefuelingCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
