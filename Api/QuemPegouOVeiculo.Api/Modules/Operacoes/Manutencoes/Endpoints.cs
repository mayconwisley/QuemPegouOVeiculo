using QuemPegouOVeiculo.Api.Common;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Manutencoes;

namespace QuemPegouOVeiculo.Api.Modules.Operacoes.Manutencoes;

public static class Endpoints
{
    public static void MapManutencoes(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/manutencoes").WithTags("Manutenções");

        group.MapGet("/", async (ManutencaoQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, ManutencaoQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (ManutencaoInput input, ManutencaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/manutencoes");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, ManutencaoInput input, ManutencaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, ManutencaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
