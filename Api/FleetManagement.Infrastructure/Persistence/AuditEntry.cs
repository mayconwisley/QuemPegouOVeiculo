namespace FleetManagement.Infrastructure.Persistence;

public sealed class AuditEntry
{
    public long Id { get; private set; }
    public DateTime OccurredAtUtc { get; private set; }
    public int? ActorUserId { get; private set; }
    public string ActorUsername { get; private set; } = "";
    public string EntityName { get; private set; } = "";
    public int EntityId { get; private set; }
    public string Action { get; private set; } = "";
    public string ChangesJson { get; private set; } = "";

    private AuditEntry() { }

    public AuditEntry(int? actorUserId, string actorUsername, string entityName, int entityId,
        string action, string changesJson)
    {
        OccurredAtUtc = DateTime.UtcNow;
        ActorUserId = actorUserId;
        ActorUsername = actorUsername;
        EntityName = entityName;
        EntityId = entityId;
        Action = action;
        ChangesJson = changesJson;
    }
}
