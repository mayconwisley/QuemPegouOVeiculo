using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.Reservations;
using FleetManagement.Domain.Modules.Operations;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class ReservationReadRepository(FleetDbContext db) : IReservationReadRepository
{
    public Task<ReservationView?> GetAsync(Guid id, CancellationToken ct) =>
        Project(db.Reservations.AsNoTracking().Where(x => x.Id == id))
            .SingleOrDefaultAsync(ct);

    public async Task<PagedResult<ReservationView>> ListAsync(ReservationFilter filter,
        PageRequest page, CancellationToken ct)
    {
        var offset = page.Offset;
        var query = db.Reservations.AsNoTracking();
        if (filter.VehicleId is Guid vehicleId)
            query = query.Where(x => x.VehicleId == vehicleId);
        if (filter.Status is not null)
            query = query.Where(x => x.Status == filter.Status);
        if (filter.FromUtc is DateTime fromUtc)
            query = query.Where(x => x.EndUtc > fromUtc);
        if (filter.ToUtc is DateTime toUtc)
            query = query.Where(x => x.StartUtc < toUtc);
        var total = await query.CountAsync(ct);
        var pageQuery = query.OrderByDescending(x => x.StartUtc).ThenBy(x => x.Id)
            .Skip(offset).Take(page.PageSize);
        var items = await Project(pageQuery).ToListAsync(ct);
        return new PagedResult<ReservationView>(items, page.Page, page.PageSize, total);
    }

    private IQueryable<ReservationView> Project(IQueryable<VehicleReservation> source) =>
        from reservation in source
        join vehicle in db.Vehicles.AsNoTracking() on reservation.VehicleId equals vehicle.Id
        join driver in db.Drivers.AsNoTracking() on reservation.DriverId equals driver.Id
        select new ReservationView(reservation.Id, reservation.VehicleId, reservation.DriverId,
            reservation.StartUtc, reservation.EndUtc, reservation.Purpose, reservation.Status,
            vehicle.Plate, vehicle.Model, driver.Name,
            db.Movements.Where(x => x.ReservationId == reservation.Id)
                .Select(x => (Guid?)x.Id).FirstOrDefault());
}
