using QuemPegouOVeiculo.Api.Common;
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
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, MovimentacaoQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (MovimentacaoInput input, MovimentacaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/movimentacoes");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, MovimentacaoInput input, MovimentacaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/{id:int:min(1)}/concluir", async (int id, ConcluirMovimentacaoInput input,
            MovimentacaoCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.ConcludeAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, MovimentacaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
