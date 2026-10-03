using FleetManagement.Application.Modules.Operations.MaintenanceRecords;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class VehicleMileageReader(FleetDbContext db) : IVehicleMileageReader
{
    public async Task<int> ReadMaximumAsync(Guid vehicleId, CancellationToken ct)
    {
        var movement = await db.Movements.AsNoTracking().Where(x => x.VehicleId == vehicleId)
            .MaxAsync(x => (int?)(x.FinalMileage ?? x.InitialMileage), ct) ?? 0;
        var refueling = await db.Refuelings.AsNoTracking().Where(x => x.VehicleId == vehicleId)
            .MaxAsync(x => (int?)x.Mileage, ct) ?? 0;
        return Math.Max(movement, refueling);
    }
}
