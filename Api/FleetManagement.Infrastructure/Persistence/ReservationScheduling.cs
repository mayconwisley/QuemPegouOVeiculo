using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Reservations;
using FleetManagement.Domain.Modules.Operations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class ReservationAvailabilityReader(FleetDbContext db) : IReservationAvailabilityReader
{
    public Task<bool> HasOpenMovementConflictAsync(int vehicleId, DateTime startUtc,
        DateTime endUtc, CancellationToken ct) =>
        db.Movements.AsNoTracking().AnyAsync(x => x.VehicleId == vehicleId &&
            x.ArrivalUtc == null && x.DepartureUtc < endUtc &&
            (x.ExpectedReturnUtc == null || x.ExpectedReturnUtc > startUtc), ct);

    public Task<bool> HasReservationConflictAsync(int vehicleId, DateTime startUtc,
        DateTime? endUtc, int? excludeReservationId, CancellationToken ct) =>
        db.Reservations.AsNoTracking().AnyAsync(x => x.VehicleId == vehicleId &&
            (x.Status == ReservationStatuses.Confirmed || x.Status == ReservationStatuses.InUse) &&
            (excludeReservationId == null || x.Id != excludeReservationId) &&
            (endUtc == null || x.StartUtc < endUtc) && x.EndUtc > startUtc, ct);
}

internal sealed class VehicleScheduleGuard(FleetDbContext db) : IVehicleScheduleGuard
{
    public async Task<Result<T>> ExecuteAsync<T>(int vehicleId,
        Func<Task<Result<T>>> operation, CancellationToken ct)
    {
        await using var transaction = await db.Database.BeginTransactionAsync(ct);
        await LockVehicleAsync(vehicleId, ct);
        var result = await operation();
        if (result.IsSuccess)
            await transaction.CommitAsync(ct);
        return result;
    }

    public async Task<Result> ExecuteAsync(int vehicleId,
        Func<Task<Result>> operation, CancellationToken ct)
    {
        await using var transaction = await db.Database.BeginTransactionAsync(ct);
        await LockVehicleAsync(vehicleId, ct);
        var result = await operation();
        if (result.IsSuccess)
            await transaction.CommitAsync(ct);
        return result;
    }

    private async Task LockVehicleAsync(int vehicleId, CancellationToken ct)
    {
        await db.Vehicles.FromSqlInterpolated(
                $"SELECT * FROM registrations.vehicles WHERE id = {vehicleId} FOR UPDATE")
            .AsNoTracking().ToListAsync(ct);
    }
}
