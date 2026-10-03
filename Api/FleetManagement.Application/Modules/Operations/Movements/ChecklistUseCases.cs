using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.Movements;

public sealed record ChecklistInput(bool TiresOk, bool LightsOk, bool FluidsOk,
    bool BodyOk, string? Notes, DateTime CheckedAtUtc);

public sealed record ChecklistView(Guid Id, Guid MovementId, string Phase, bool TiresOk,
    bool LightsOk, bool FluidsOk, bool BodyOk, string Notes, DateTime CheckedAtUtc);

public interface IMovementChecklistStore
{
    Task<MovementChecklist?> FindAsync(Guid movementId, string phase, CancellationToken ct);
    Task<IReadOnlyList<ChecklistView>> ListAsync(Guid movementId, CancellationToken ct);
    Task AddAsync(MovementChecklist checklist, CancellationToken ct);
    Task DeleteForMovementAsync(Guid movementId, CancellationToken ct);
    Task SaveAsync(CancellationToken ct);
}

public sealed class ChecklistService(IMovementChecklistStore store,
    ICommandRepository<VehicleMovement> movements)
{
    public Task<Result<IReadOnlyList<ChecklistView>>> ListAsync(Guid movementId, CancellationToken ct) =>
        Result.CaptureValueAsync(async () =>
        {
            if (await movements.GetByIdAsync(movementId, ct) is null)
                return Result<IReadOnlyList<ChecklistView>>.Failure(Error.NotFound("Movimentação", movementId));
            return Result<IReadOnlyList<ChecklistView>>.Success(await store.ListAsync(movementId, ct));
        });

    public Task<Result> SaveAsync(Guid movementId, string phase, ChecklistInput input, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            if (!ChecklistPhases.IsValid(phase))
                return Result.Failure(Error.Validation("Etapa do checklist inválida."));
            var movement = await movements.GetByIdAsync(movementId, ct);
            if (movement is null)
                return Result.Failure(Error.NotFound("Movimentação", movementId));
            if (phase == ChecklistPhases.Arrival && movement.IsOpen)
                return Result.Failure(Error.Conflict("Registre a chegada antes do checklist de retorno."));
            var checklist = await store.FindAsync(movementId, phase, ct);
            if (checklist is null)
            {
                checklist = new MovementChecklist(movementId, phase, input.TiresOk,
                    input.LightsOk, input.FluidsOk, input.BodyOk, input.Notes, input.CheckedAtUtc);
                await store.AddAsync(checklist, ct);
            }
            else
                checklist.Update(input.TiresOk, input.LightsOk, input.FluidsOk,
                    input.BodyOk, input.Notes, input.CheckedAtUtc);
            await store.SaveAsync(ct);
            return Result.Success();
        });
}
