using FleetManagement.Application.Common;

namespace FleetManagement.Application.Modules.Access;

public sealed record AuditView(Guid Id, DateTime OccurredAtUtc, string ActorUsername,
    string EntityName, Guid EntityId, string Action, string ChangesJson);

public interface IAuditReader
{
    Task<PagedResult<AuditView>> ListAsync(PageRequest page, CancellationToken ct);
}
