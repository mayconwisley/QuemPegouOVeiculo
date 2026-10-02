using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Access;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class AuditReader(FleetDbContext db) : IAuditReader
{
    public async Task<PagedResult<AuditView>> ListAsync(PageRequest page, CancellationToken ct)
    {
        var offset = page.Offset;
        var query = db.AuditEntries.AsNoTracking();
        var total = await query.CountAsync(ct);
        var items = await query.OrderByDescending(x => x.Id).Skip(offset).Take(page.PageSize)
            .Select(x => new AuditView(x.Id, x.OccurredAtUtc, x.ActorUsername,
                x.EntityName, x.EntityId, x.Action, x.ChangesJson)).ToListAsync(ct);
        return new PagedResult<AuditView>(items, page.Page, page.PageSize, total);
    }
}
