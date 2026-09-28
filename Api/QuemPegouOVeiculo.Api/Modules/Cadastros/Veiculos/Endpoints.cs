using QuemPegouOVeiculo.Api.Common;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Cadastros.Veiculos;

namespace QuemPegouOVeiculo.Api.Modules.Cadastros.Veiculos;

public static class Endpoints
{
    public static void MapVeiculos(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/veiculos").WithTags("Veículos");

        group.MapGet("/", async (VeiculoQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, VeiculoQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (VeiculoInput input, VeiculoCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/veiculos");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, VeiculoInput input, VeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, VeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
