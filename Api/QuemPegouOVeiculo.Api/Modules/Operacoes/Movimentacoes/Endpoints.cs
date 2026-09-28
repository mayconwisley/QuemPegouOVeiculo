using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Movimentacoes;

namespace QuemPegouOVeiculo.Api.Modules.Operacoes.Movimentacoes;

public static class Endpoints
{
    public static void MapMovimentacoes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/movimentacoes").WithTags("Movimentações");

        group.MapGet("/", async (MovimentacaoQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, MovimentacaoQueries queries, CancellationToken cancellationToken) =>
        {
            var movimentacao = await queries.GetAsync(id, cancellationToken);
            return movimentacao is null ? Results.NotFound() : Results.Ok(movimentacao);
        });

        group.MapPost("/", async (MovimentacaoInput input, MovimentacaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/movimentacoes/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, MovimentacaoInput input, MovimentacaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapPost("/{id:int:min(1)}/concluir", async (int id, ConcluirMovimentacaoInput input,
            MovimentacaoCommands commands, CancellationToken cancellationToken) =>
        {
            await commands.ConcludeAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, MovimentacaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
