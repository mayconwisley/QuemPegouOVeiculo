using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Abastecimentos;

namespace QuemPegouOVeiculo.Api.Modules.Operacoes.Abastecimentos;

public static class Endpoints
{
    public static void MapAbastecimentos(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/abastecimentos").WithTags("Abastecimentos");

        group.MapGet("/", async (AbastecimentoQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, AbastecimentoQueries queries, CancellationToken cancellationToken) =>
        {
            var item = await queries.GetAsync(id, cancellationToken);
            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        group.MapPost("/", async (AbastecimentoInput input, AbastecimentoCommands commands,
            CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/abastecimentos/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, AbastecimentoInput input, AbastecimentoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, AbastecimentoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
