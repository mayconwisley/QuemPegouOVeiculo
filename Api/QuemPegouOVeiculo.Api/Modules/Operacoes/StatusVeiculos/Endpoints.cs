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
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, StatusVeiculoQueries queries, CancellationToken cancellationToken) =>
        {
            var item = await queries.GetAsync(id, cancellationToken);
            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        group.MapPost("/", async (StatusVeiculoInput input, StatusVeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/status-veiculo/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, StatusVeiculoInput input, StatusVeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, StatusVeiculoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
