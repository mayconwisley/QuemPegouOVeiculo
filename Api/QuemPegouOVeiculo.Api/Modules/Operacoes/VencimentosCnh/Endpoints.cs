using QuemPegouOVeiculo.Api.Common;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes.VencimentosCnh;

namespace QuemPegouOVeiculo.Api.Modules.Operacoes.VencimentosCnh;

public static class Endpoints
{
    public static void MapVencimentosCnh(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/vencimentos-cnh").WithTags("Vencimentos de CNH");

        group.MapGet("/", async (VencimentoCnhQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:int:min(1)}", async (int id, VencimentoCnhQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (VencimentoCnhInput input, VencimentoCnhCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/vencimentos-cnh");
        });

        group.MapPut("/{id:int:min(1)}", async (int id, VencimentoCnhInput input, VencimentoCnhCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:int:min(1)}", async (int id, VencimentoCnhCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
