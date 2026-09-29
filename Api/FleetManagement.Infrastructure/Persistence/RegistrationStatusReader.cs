using Microsoft.EntityFrameworkCore;
using FleetManagement.Application.Modules.Operations;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class RegistrationStatusReader(FleetDbContext db) : IRegistrationStatusReader
{
    public Task<bool?> IsVehicleActiveAsync(int id, CancellationToken cancellationToken) =>
        db.Vehicles.AsNoTracking().Where(x => x.Id == id)
            .Select(x => (bool?)x.Active).SingleOrDefaultAsync(cancellationToken);

    public Task<bool?> IsDriverActiveAsync(int id, CancellationToken cancellationToken) =>
        db.Drivers.AsNoTracking().Where(x => x.Id == id)
            .Select(x => (bool?)x.Active).SingleOrDefaultAsync(cancellationToken);
}
