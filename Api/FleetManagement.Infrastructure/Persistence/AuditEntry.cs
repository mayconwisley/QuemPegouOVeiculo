namespace FleetManagement.Infrastructure.Persistence;

public sealed class AuditEntry
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public DateTime OccurredAtUtc { get; private set; }
    public Guid? ActorUserId { get; private set; }
    public string ActorUsername { get; private set; } = "";
    public string EntityName { get; private set; } = "";
    public Guid EntityId { get; private set; }
    public string Action { get; private set; } = "";
    public string ChangesJson { get; private set; } = "";

    private AuditEntry() { }

    public AuditEntry(Guid? actorUserId, string actorUsername, string entityName, Guid entityId,
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
