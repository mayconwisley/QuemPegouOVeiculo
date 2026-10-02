using FleetManagement.Application.Modules.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class DashboardReader(FleetDbContext db) : IDashboardReader
{
    public async Task<DashboardSnapshot> ReadAsync(DateTime nowUtc, DateOnly today, CancellationToken ct)
    {
        var licenseLimit = today.AddDays(30);
        var olderThan = nowUtc.AddHours(-24);
        var activeVehicles = await db.Vehicles.AsNoTracking().CountAsync(x => x.Active, ct);
        var activeDrivers = await db.Drivers.AsNoTracking().CountAsync(x => x.Active, ct);
        var openMovements = await db.Movements.AsNoTracking().CountAsync(x => x.ArrivalUtc == null, ct);
        var openStatuses = await db.VehicleStatuses.AsNoTracking().CountAsync(x => x.EndUtc == null, ct);
        var expiringLicenses = await db.Drivers.AsNoTracking().CountAsync(x =>
            x.Active && x.LicenseExpiration <= licenseLimit, ct);
        var overdueReturns = await db.Movements.AsNoTracking().CountAsync(x =>
            x.ArrivalUtc == null && x.ExpectedReturnUtc < nowUtc, ct);
        var pendingChecklists = await db.Movements.AsNoTracking().CountAsync(x =>
            !db.MovementChecklists.Any(c => c.MovementId == x.Id && c.Phase == "departure") ||
            x.ArrivalUtc != null && !db.MovementChecklists.Any(c => c.MovementId == x.Id && c.Phase == "arrival"), ct);

        var licenses = await db.Drivers.AsNoTracking()
            .Where(x => x.Active && x.LicenseExpiration <= licenseLimit)
            .OrderBy(x => x.LicenseExpiration).Take(10)
            .Select(x => new DashboardAttention("license", x.Id, x.Name, null, x.LicenseExpiration))
            .ToListAsync(ct);
        var movements = await db.Movements.AsNoTracking()
            .Where(x => x.ArrivalUtc == null && x.ExpectedReturnUtc == null && x.DepartureUtc <= olderThan)
            .OrderBy(x => x.DepartureUtc).Take(10)
            .Join(db.Vehicles.AsNoTracking(), movement => movement.VehicleId, vehicle => vehicle.Id,
                (movement, vehicle) => new DashboardAttention("open-movement", movement.Id,
                    vehicle.Plate + " - " + vehicle.Model, movement.DepartureUtc, null))
            .ToListAsync(ct);
        var overdue = await db.Movements.AsNoTracking()
            .Where(x => x.ArrivalUtc == null && x.ExpectedReturnUtc < nowUtc)
            .OrderBy(x => x.ExpectedReturnUtc).Take(10)
            .Join(db.Vehicles.AsNoTracking(), movement => movement.VehicleId, vehicle => vehicle.Id,
                (movement, vehicle) => new DashboardAttention("overdue-return", movement.Id,
                    vehicle.Plate + " - " + vehicle.Model, movement.ExpectedReturnUtc, null))
            .ToListAsync(ct);
        var departureChecklists = await db.Movements.AsNoTracking()
            .Where(x => x.ArrivalUtc == null && !db.MovementChecklists.Any(c =>
                c.MovementId == x.Id && c.Phase == "departure"))
            .OrderBy(x => x.DepartureUtc).Take(5)
            .Join(db.Vehicles.AsNoTracking(), movement => movement.VehicleId, vehicle => vehicle.Id,
                (movement, vehicle) => new DashboardAttention("departure-checklist", movement.Id,
                    vehicle.Plate + " - " + vehicle.Model, movement.DepartureUtc, null))
            .ToListAsync(ct);
        var arrivalChecklists = await db.Movements.AsNoTracking()
            .Where(x => x.ArrivalUtc != null && !db.MovementChecklists.Any(c =>
                c.MovementId == x.Id && c.Phase == "arrival"))
            .OrderByDescending(x => x.ArrivalUtc).Take(5)
            .Join(db.Vehicles.AsNoTracking(), movement => movement.VehicleId, vehicle => vehicle.Id,
                (movement, vehicle) => new DashboardAttention("arrival-checklist", movement.Id,
                    vehicle.Plate + " - " + vehicle.Model, movement.ArrivalUtc, null))
            .ToListAsync(ct);
        var plansWithMileage = db.MaintenancePlans.AsNoTracking().Select(plan => new
        {
            plan.Id, plan.Name, plan.VehicleId, plan.IsActive, plan.NextDueDate, plan.NextDueMileage,
            CurrentMileage = Math.Max(
                db.Movements.Where(x => x.VehicleId == plan.VehicleId)
                    .Max(x => (int?)(x.FinalMileage ?? x.InitialMileage)) ?? 0,
                db.Refuelings.Where(x => x.VehicleId == plan.VehicleId)
                    .Max(x => (int?)x.Mileage) ?? 0)
        });
        var duePlans = plansWithMileage.Where(x => x.IsActive &&
            (x.NextDueDate != null && x.NextDueDate <= licenseLimit ||
             x.NextDueMileage != null && x.CurrentMileage >= x.NextDueMileage));
        var dueMaintenancePlans = await duePlans.CountAsync(ct);
        var planItems = await duePlans.OrderBy(x => x.NextDueDate).Take(10)
            .Join(db.Vehicles.AsNoTracking(), plan => plan.VehicleId, vehicle => vehicle.Id,
                (plan, vehicle) => new DashboardAttention("maintenance-plan", plan.Id,
                    vehicle.Plate + " - " + plan.Name, null, plan.NextDueDate))
            .ToListAsync(ct);
        return new DashboardSnapshot(activeVehicles, activeDrivers, openMovements, openStatuses,
            expiringLicenses, overdueReturns, pendingChecklists, dueMaintenancePlans,
            overdue.Concat(departureChecklists).Concat(arrivalChecklists).Concat(planItems)
                .Concat(licenses).Concat(movements).Take(40).ToArray());
    }
}
