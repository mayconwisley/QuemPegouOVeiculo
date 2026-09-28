using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes.VencimentosCnh;

namespace QuemPegouOVeiculo.Api.Modules.Operacoes.VencimentosCnh;

public static class Endpoints
{
    public static void MapVencimentosCnh(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/vencimentos-cnh").WithTags("Vencimentos de CNH");

        group.MapGet("/", async (VencimentoCnhQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, VencimentoCnhQueries queries, CancellationToken cancellationToken) =>
        {
            var item = await queries.GetAsync(id, cancellationToken);
            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        group.MapPost("/", async (VencimentoCnhInput input, VencimentoCnhCommands commands,
            CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/vencimentos-cnh/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, VencimentoCnhInput input, VencimentoCnhCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, VencimentoCnhCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
