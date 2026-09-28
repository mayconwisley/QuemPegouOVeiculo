using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Multas;

namespace QuemPegouOVeiculo.Api.Modules.Operacoes.Multas;

public static class Endpoints
{
    public static void MapMultas(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/multas").WithTags("Multas");

        group.MapGet("/", async (MultaQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, MultaQueries queries, CancellationToken cancellationToken) =>
        {
            var item = await queries.GetAsync(id, cancellationToken);
            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        group.MapPost("/", async (MultaInput input, MultaCommands commands, CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/multas/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, MultaInput input, MultaCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, MultaCommands commands, CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
