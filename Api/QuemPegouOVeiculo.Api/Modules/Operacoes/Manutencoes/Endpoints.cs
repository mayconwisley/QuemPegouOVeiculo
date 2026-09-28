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
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, ManutencaoQueries queries, CancellationToken cancellationToken) =>
        {
            var item = await queries.GetAsync(id, cancellationToken);
            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        group.MapPost("/", async (ManutencaoInput input, ManutencaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/manutencoes/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, ManutencaoInput input, ManutencaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, ManutencaoCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
