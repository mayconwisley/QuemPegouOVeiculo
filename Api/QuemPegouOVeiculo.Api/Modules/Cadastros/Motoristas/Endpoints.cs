using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Cadastros.Motoristas;

namespace QuemPegouOVeiculo.Api.Modules.Cadastros.Motoristas;

public static class Endpoints
{
    public static void MapMotoristas(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/motoristas").WithTags("Motoristas");

        group.MapGet("/", async (MotoristaQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            Results.Ok(await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)));

        group.MapGet("/{id:int:min(1)}", async (int id, MotoristaQueries queries, CancellationToken cancellationToken) =>
        {
            var motorista = await queries.GetAsync(id, cancellationToken);
            return motorista is null ? Results.NotFound() : Results.Ok(motorista);
        });

        group.MapPost("/", async (MotoristaInput input, MotoristaCommands commands, CancellationToken cancellationToken) =>
        {
            var id = await commands.CreateAsync(input, cancellationToken);
            return Results.Created($"/api/v1/motoristas/{id}", new { id });
        });

        group.MapPut("/{id:int:min(1)}", async (int id, MotoristaInput input, MotoristaCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.UpdateAsync(id, input, cancellationToken);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, MotoristaCommands commands,
            CancellationToken cancellationToken) =>
        {
            await commands.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        });
    }
}
