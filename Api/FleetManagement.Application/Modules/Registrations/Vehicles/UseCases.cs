using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Registrations;

namespace FleetManagement.Application.Modules.Registrations.Vehicles;

public sealed record VehicleInput(string Plate, string Model, string? Chassis, string? Renavam, bool Active);
public sealed record VehicleView(int Id, string Plate, string Model, string Chassis, string Renavam, bool Active);

public sealed class VehicleCommands(ICommandRepository<Vehicle> repository)
{
    public async Task<Result<int>> CreateAsync(VehicleInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var vehicle = new Vehicle(input.Plate, input.Model, input.Chassis, input.Renavam, input.Active);
            await repository.AddAsync(vehicle, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return vehicle.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, VehicleInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var vehicle = await repository.GetByIdAsync(id, cancellationToken);
            if (vehicle is null)
                return Result.Failure(Error.NotFound("Veículo", id));
            vehicle.Update(input.Plate, input.Model, input.Chassis, input.Renavam, input.Active);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Veículo", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class VehicleQueries(IQueryRepository<Vehicle> repository)
{
    private static readonly Expression<Func<Vehicle, VehicleView>> Projection = x =>
        new VehicleView(x.Id, x.Plate, x.Model, x.Chassis, x.Renavam, x.Active);

    public async Task<Result<VehicleView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<VehicleView>.Failure(Error.NotFound("Veículo", id))
            : Result<VehicleView>.Success(item);
    }

    public Task<Result<PagedResult<VehicleView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
