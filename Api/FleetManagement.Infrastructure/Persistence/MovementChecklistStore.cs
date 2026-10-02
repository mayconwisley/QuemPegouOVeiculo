using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Movements;
using FleetManagement.Domain.Modules.Operations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class MovementChecklistStore(FleetDbContext db) : IMovementChecklistStore
{
    public Task<MovementChecklist?> FindAsync(int movementId, string phase, CancellationToken ct) =>
        db.MovementChecklists.SingleOrDefaultAsync(x => x.MovementId == movementId && x.Phase == phase, ct);

    public async Task<IReadOnlyList<ChecklistView>> ListAsync(int movementId, CancellationToken ct) =>
        await db.MovementChecklists.AsNoTracking().Where(x => x.MovementId == movementId)
            .OrderBy(x => x.Phase)
            .Select(x => new ChecklistView(x.Id, x.MovementId, x.Phase, x.TiresOk, x.LightsOk,
                x.FluidsOk, x.BodyOk, x.Notes, x.CheckedAtUtc)).ToListAsync(ct);

    public async Task AddAsync(MovementChecklist checklist, CancellationToken ct) =>
        await db.MovementChecklists.AddAsync(checklist, ct);

    public async Task DeleteForMovementAsync(int movementId, CancellationToken ct)
    {
        var items = await db.MovementChecklists.Where(x => x.MovementId == movementId).ToListAsync(ct);
        db.MovementChecklists.RemoveRange(items);
    }

    public async Task SaveAsync(CancellationToken ct)
    {
        try
        {
            await db.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException
            { SqlState: PostgresErrorCodes.UniqueViolation or PostgresErrorCodes.ForeignKeyViolation })
        {
            throw new BusinessConflictException("O checklist foi alterado por outro usuário. Recarregue e tente novamente.", ex);
        }
    }
}
