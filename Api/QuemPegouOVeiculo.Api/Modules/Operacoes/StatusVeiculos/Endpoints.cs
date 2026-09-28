using QuemPegouOVeiculo.Api.Common;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes.StatusVeiculos;

namespace QuemPegouOVeiculo.Api.Modules.Operacoes.StatusVeiculos;

public static class Endpoints
{
    public static void MapStatusVeiculos(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/status-veiculo").WithTags("Status do veículo");

        group.MapGet("/", async (StatusVeiculoQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, StatusVeiculoQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (StatusVeiculoInput input, StatusVeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/status-veiculo");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, StatusVeiculoInput input, StatusVeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, StatusVeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
