using Microsoft.EntityFrameworkCore;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Queries;
using FleetManagement.Domain.Common;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class FleetReadRepository(FleetDbContext db) : IFleetReadRepository
{
    public Task<PagedResult<DriverQuery>> DriversAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = db.Drivers.AsNoTracking();
        if (filter.Active.HasValue)
            query = query.Where(x => x.Active == filter.Active.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            var normalizedCpf = new string(search.Where(char.IsDigit).ToArray());
            query = query.Where(x => x.Name.ToLower().Contains(search) || x.LicenseNumber.ToLower().Contains(search)
                || x.Cpf.Contains(search) || (normalizedCpf.Length > 0 && x.Cpf.Contains(normalizedCpf))
                || x.Rg.ToLower().Contains(search));
        }
        return PaginateAsync(query.OrderBy(x => x.Name).ThenBy(x => x.Cpf)
            .Select(x => new DriverQuery(x.Id, x.Name, x.LicenseNumber, x.LicenseExpiration,
                x.LicenseCategory, x.Cpf, x.Rg, x.Active)), page, ct);
    }

    public Task<PagedResult<VehicleQuery>> VehiclesAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = db.Vehicles.AsNoTracking();
        if (filter.Active.HasValue)
            query = query.Where(x => x.Active == filter.Active.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            var normalizedPlate = search.Replace("-", "").Replace(" ", "");
            query = query.Where(x => x.Plate.ToLower().Contains(search) || x.Model.ToLower().Contains(search)
                || x.Plate.ToLower().Contains(normalizedPlate) || x.Renavam.ToLower().Contains(search));
        }
        return PaginateAsync(query.OrderBy(x => x.Model).ThenBy(x => x.Plate)
            .Select(x => new VehicleQuery(x.Id, x.Plate, x.Model, x.Chassis, x.Renavam, x.Active)), page, ct);
    }

    public Task<PagedResult<MovementQuery>> MovementsAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = from item in db.Movements.AsNoTracking()
                    join vehicle in db.Vehicles.AsNoTracking() on item.VehicleId equals vehicle.Id
                    join driver in db.Drivers.AsNoTracking() on item.DriverId equals driver.Id
                    select new { item, vehicle, driver };
        if (filter.VehicleId.HasValue)
            query = query.Where(x => x.item.VehicleId == filter.VehicleId.Value);
        if (filter.DriverId.HasValue)
            query = query.Where(x => x.item.DriverId == filter.DriverId.Value);
        if (filter.IsOpen.HasValue)
            query = query.Where(x => (x.item.ArrivalUtc == null) == filter.IsOpen.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            query = query.Where(x => x.vehicle.Model.ToLower().Contains(search)
                || x.driver.Name.ToLower().Contains(search) || x.item.Description.ToLower().Contains(search));
        }
        if (filter.StartUtc.HasValue)
            query = filter.DateField == "arrival"
                ? query.Where(x => x.item.ArrivalUtc >= filter.StartUtc.Value)
                : query.Where(x => x.item.DepartureUtc >= filter.StartUtc.Value);
        if (filter.EndUtc.HasValue)
            query = filter.DateField == "arrival"
                ? query.Where(x => x.item.ArrivalUtc < filter.EndUtc.Value)
                : query.Where(x => x.item.DepartureUtc < filter.EndUtc.Value);
        return PaginateAsync(query.OrderByDescending(x => x.item.DepartureUtc).ThenBy(x => x.driver.Name)
            .Select(x => new MovementQuery(x.item.Id, x.item.VehicleId, x.vehicle.Model,
                x.item.DriverId, x.driver.Name, x.item.DepartureUtc, x.item.ArrivalUtc,
                x.item.Description, x.item.InitialMileage, x.item.FinalMileage)), page, ct);
    }

    public Task<PagedResult<RefuelingQuery>> RefuelingsAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = from item in db.Refuelings.AsNoTracking()
                    join vehicle in db.Vehicles.AsNoTracking() on item.VehicleId equals vehicle.Id
                    join driver in db.Drivers.AsNoTracking() on item.DriverId equals driver.Id
                    select new { item, vehicle, driver };
        if (filter.VehicleId.HasValue)
            query = query.Where(x => x.item.VehicleId == filter.VehicleId.Value);
        if (filter.DriverId.HasValue)
            query = query.Where(x => x.item.DriverId == filter.DriverId.Value);
        if (filter.FromDate.HasValue)
            query = query.Where(x => x.item.Date >= filter.FromDate.Value);
        if (filter.ToDate.HasValue)
            query = query.Where(x => x.item.Date <= filter.ToDate.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            query = query.Where(x => x.vehicle.Model.ToLower().Contains(search)
                || x.driver.Name.ToLower().Contains(search) || x.item.Description.ToLower().Contains(search));
        }
        return PaginateAsync(query.OrderByDescending(x => x.item.Date).ThenBy(x => x.driver.Name)
            .Select(x => new RefuelingQuery(x.item.Id, x.item.VehicleId, x.vehicle.Model,
                x.item.DriverId, x.driver.Name, x.item.Mileage, x.item.Date,
                x.item.Amount, x.item.Liters, x.item.Description)), page, ct);
    }

    public Task<PagedResult<FineQuery>> FinesAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = from item in db.Fines.AsNoTracking()
                    join vehicle in db.Vehicles.AsNoTracking() on item.VehicleId equals vehicle.Id
                    join driver in db.Drivers.AsNoTracking() on item.DriverId equals driver.Id
                    select new { item, vehicle, driver };
        if (filter.VehicleId.HasValue)
            query = query.Where(x => x.item.VehicleId == filter.VehicleId.Value);
        if (filter.DriverId.HasValue)
            query = query.Where(x => x.item.DriverId == filter.DriverId.Value);
        if (filter.FromDate.HasValue)
            query = query.Where(x => x.item.Date >= filter.FromDate.Value);
        if (filter.ToDate.HasValue)
            query = query.Where(x => x.item.Date <= filter.ToDate.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            query = query.Where(x => x.vehicle.Model.ToLower().Contains(search)
                || x.driver.Name.ToLower().Contains(search) || x.item.Description.ToLower().Contains(search));
        }
        return PaginateAsync(query.OrderByDescending(x => x.item.Date).ThenBy(x => x.driver.Name)
            .Select(x => new FineQuery(x.item.Id, x.item.VehicleId, x.vehicle.Model,
                x.item.DriverId, x.driver.Name, x.item.Date, x.item.Amount,
                x.item.Points, x.item.Description)), page, ct);
    }

    public Task<PagedResult<MaintenanceQuery>> MaintenanceAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = from item in db.Maintenance.AsNoTracking()
                    join vehicle in db.Vehicles.AsNoTracking() on item.VehicleId equals vehicle.Id
                    select new { item, vehicle };
        if (filter.VehicleId.HasValue)
            query = query.Where(x => x.item.VehicleId == filter.VehicleId.Value);
        if (filter.FromDate.HasValue)
            query = query.Where(x => x.item.Date >= filter.FromDate.Value);
        if (filter.ToDate.HasValue)
            query = query.Where(x => x.item.Date <= filter.ToDate.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            query = query.Where(x => x.vehicle.Model.ToLower().Contains(search)
                || x.item.Description.ToLower().Contains(search));
        }
        return PaginateAsync(query.OrderByDescending(x => x.item.Date).ThenBy(x => x.vehicle.Model)
            .Select(x => new MaintenanceQuery(x.item.Id, x.item.VehicleId, x.vehicle.Model,
                x.item.Date, x.item.Amount, x.item.Description)), page, ct);
    }

    public Task<PagedResult<VehicleStatusQuery>> VehicleStatusesAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = from item in db.VehicleStatuses.AsNoTracking()
                    join vehicle in db.Vehicles.AsNoTracking() on item.VehicleId equals vehicle.Id
                    select new { item, vehicle };
        if (filter.VehicleId.HasValue)
            query = query.Where(x => x.item.VehicleId == filter.VehicleId.Value);
        if (filter.IsOpen.HasValue)
            query = query.Where(x => (x.item.EndUtc == null) == filter.IsOpen.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            query = query.Where(x => x.vehicle.Model.ToLower().Contains(search)
                || x.item.Description.ToLower().Contains(search));
        }
        if (filter.StartUtc.HasValue)
            query = filter.DateField == "end"
                ? query.Where(x => x.item.EndUtc >= filter.StartUtc.Value)
                : query.Where(x => x.item.StartUtc >= filter.StartUtc.Value);
        if (filter.EndUtc.HasValue)
            query = filter.DateField == "end"
                ? query.Where(x => x.item.EndUtc < filter.EndUtc.Value)
                : query.Where(x => x.item.StartUtc < filter.EndUtc.Value);
        return PaginateAsync(query.OrderBy(x => x.vehicle.Model).ThenByDescending(x => x.item.StartUtc)
            .Select(x => new VehicleStatusQuery(x.item.Id, x.item.VehicleId, x.vehicle.Model,
                x.item.StartUtc, x.item.EndUtc, x.item.Description)), page, ct);
    }

    public Task<PagedResult<LicenseExpirationQuery>> LicenseExpirationsAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct)
    {
        var query = from item in db.LicenseExpirations.AsNoTracking()
                    join driver in db.Drivers.AsNoTracking() on item.DriverId equals driver.Id
                    select new { item, driver };
        if (filter.DriverId.HasValue)
            query = query.Where(x => x.item.DriverId == filter.DriverId.Value);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim().ToLowerInvariant();
            query = query.Where(x => x.driver.Name.ToLower().Contains(search));
        }
        return PaginateAsync(query.OrderBy(x => x.driver.Name)
            .Select(x => new LicenseExpirationQuery(x.item.Id, x.item.DriverId,
                x.driver.Name, x.item.Date, x.item.Expired)), page, ct);
    }

    public Task<int?> LatestMileageAsync(int vehicleId, string source, CancellationToken ct) => source switch
    {
        "refueling" => db.Refuelings.AsNoTracking().Where(x => x.VehicleId == vehicleId)
            .OrderByDescending(x => x.Id).Select(x => (int?)x.Mileage).FirstOrDefaultAsync(ct),
        "movement" => db.Movements.AsNoTracking().Where(x => x.VehicleId == vehicleId && x.FinalMileage != null)
            .OrderByDescending(x => x.Id).Select(x => x.FinalMileage).FirstOrDefaultAsync(ct),
        _ => throw new DomainException("A origem deve ser movimentação ou abastecimento.")
    };

    private static async Task<PagedResult<T>> PaginateAsync<T>(IQueryable<T> query, PageRequest page, CancellationToken ct)
    {
        var offset = page.Offset;
        var total = await query.CountAsync(ct);
        var items = await query.Skip(offset).Take(page.PageSize).ToListAsync(ct);
        return new PagedResult<T>(items, page.Page, page.PageSize, total);
    }
}
