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
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, VeiculoQueries queries, CancellationToken cancellationToken) =>
        {
            var veiculo = await queries.GetAsync(id, cancellationToken);
            return veiculo is null ? Results.NotFound() : Results.Ok(veiculo);
        });

        group.MapPost("/", async (VeiculoInput input, VeiculoCommands commands, CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/veiculos/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, VeiculoInput input, VeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, VeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
