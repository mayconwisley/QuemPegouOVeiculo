using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Fines;

namespace FleetManagement.Api.Modules.Operations.Fines;

public static class Endpoints
{
    public static void MapFines(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/fines").WithTags("Multas");

        group.MapGet("/", async (FineQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:guid}", async (Guid id, FineQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (FineInput input, FineCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/fines");
        });

        group.MapPut("/{id:guid}", async (Guid id, FineInput input, FineCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, FineCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
