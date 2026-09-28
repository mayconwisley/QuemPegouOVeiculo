using QuemPegouOVeiculo.Api.Common;
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
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, AbastecimentoQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (AbastecimentoInput input, AbastecimentoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/abastecimentos");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, AbastecimentoInput input, AbastecimentoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, AbastecimentoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
