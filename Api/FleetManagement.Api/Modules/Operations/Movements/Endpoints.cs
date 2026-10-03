using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Movements;

namespace FleetManagement.Api.Modules.Operations.Movements;

public static class Endpoints
{
    public static void MapMovements(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/movements").WithTags("Movimentações");

        group.MapGet("/", async (MovementQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:guid}", async (Guid id, MovementQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (MovementInput input, MovementCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/movements");
        });

        group.MapPut("/{id:guid}", async (Guid id, MovementInput input, MovementCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/{id:guid}/complete", async (Guid id, CompleteMovementInput input,
            MovementCommands commands, CancellationToken cancellationToken) =>
        {
            return (await commands.ConcludeAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapPut("/{id:guid}/expected-return", async (Guid id, ExpectedReturnInput input,
            MovementCommands commands, CancellationToken cancellationToken) =>
            (await commands.ScheduleReturnAsync(id, input.ExpectedReturnUtc, cancellationToken)).ToHttpResult());

        group.MapGet("/{id:guid}/checklists", async (Guid id, ChecklistService checklists,
            CancellationToken cancellationToken) =>
            (await checklists.ListAsync(id, cancellationToken)).ToHttpResult());

        group.MapPut("/{id:guid}/checklists/{phase}", async (Guid id, string phase,
            ChecklistInput input, ChecklistService checklists, CancellationToken cancellationToken) =>
            (await checklists.SaveAsync(id, phase, input, cancellationToken)).ToHttpResult());

        group.MapDelete("/{id:guid}", async (Guid id, MovementCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}

public sealed record ExpectedReturnInput(DateTime? ExpectedReturnUtc);
