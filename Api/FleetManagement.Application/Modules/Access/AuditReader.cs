using FleetManagement.Application.Common;

namespace FleetManagement.Application.Modules.Access;

public sealed record AuditView(long Id, DateTime OccurredAtUtc, string ActorUsername,
    string EntityName, int EntityId, string Action, string ChangesJson);

public interface IAuditReader
{
    Task<PagedResult<AuditView>> ListAsync(PageRequest page, CancellationToken ct);
}
