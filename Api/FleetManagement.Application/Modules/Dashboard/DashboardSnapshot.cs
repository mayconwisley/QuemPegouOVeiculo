namespace FleetManagement.Application.Modules.Dashboard;

public sealed record DashboardAttention(string Kind, Guid RecordId, string Description, DateTime? OccurredAtUtc,
    DateOnly? DueDate);

public sealed record DashboardSnapshot(int ActiveVehicles, int ActiveDrivers, int OpenMovements,
    int OpenVehicleStatuses, int ExpiringLicenses, int OverdueReturns,
    int PendingChecklists, int DueMaintenancePlans, IReadOnlyList<DashboardAttention> Attention);

public interface IDashboardReader
{
    Task<DashboardSnapshot> ReadAsync(DateTime nowUtc, DateOnly today, CancellationToken cancellationToken);
}
