using FleetManagement.Application.Common;

namespace FleetManagement.Application.Modules.Queries;

public sealed record LatestMileageView(int? Mileage);

public sealed class FleetQueries(IFleetReadRepository queries)
{
    public Task<Result<PagedResult<DriverQuery>>> DriversAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.DriversAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<PagedResult<VehicleQuery>>> VehiclesAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.VehiclesAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<PagedResult<MovementQuery>>> MovementsAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.MovementsAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<PagedResult<RefuelingQuery>>> RefuelingsAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.RefuelingsAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<PagedResult<FineQuery>>> FinesAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.FinesAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<PagedResult<MaintenanceQuery>>> MaintenanceAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.MaintenanceAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<PagedResult<VehicleStatusQuery>>> VehicleStatusesAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.VehicleStatusesAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<PagedResult<LicenseExpirationQuery>>> LicenseExpirationsAsync(
        FleetQueryFilter filter, PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => queries.LicenseExpirationsAsync(filter.Validate(), page, cancellationToken));

    public Task<Result<LatestMileageView>> LatestMileageAsync(
        Guid vehicleId, string source, CancellationToken cancellationToken) =>
        Result.TryAsync(async () => new LatestMileageView(
            await queries.LatestMileageAsync(vehicleId, source, cancellationToken)));
}
