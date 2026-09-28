using QuemPegouOVeiculo.Api.Common;
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
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, MotoristaQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (MotoristaInput input, MotoristaCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/motoristas");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, MotoristaInput input, MotoristaCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, MotoristaCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
