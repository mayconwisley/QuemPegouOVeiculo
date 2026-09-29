using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.Refuelings;

public sealed record RefuelingInput(int VehicleId, int DriverId, int Mileage,
    DateOnly Date, decimal Amount, decimal Liters, string? Description);

public sealed record RefuelingView(int Id, int VehicleId, int DriverId, int Mileage,
    DateOnly Date, decimal Amount, decimal Liters, string Description);

public sealed class RefuelingCommands(ICommandRepository<Refueling> repository)
{
    public async Task<Result<int>> CreateAsync(RefuelingInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new Refueling(input.VehicleId, input.DriverId, input.Mileage,
                input.Date, input.Amount, input.Liters, input.Description);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, RefuelingInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Abastecimento", id));
            item.Update(input.VehicleId, input.DriverId, input.Mileage,
                input.Date, input.Amount, input.Liters, input.Description);
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
                return Result.Failure(Error.NotFound("Abastecimento", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class RefuelingQueries(IQueryRepository<Refueling> repository)
{
    private static readonly Expression<Func<Refueling, RefuelingView>> Projection = x =>
        new RefuelingView(x.Id, x.VehicleId, x.DriverId, x.Mileage,
            x.Date, x.Amount, x.Liters, x.Description);

    public async Task<Result<RefuelingView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<RefuelingView>.Failure(Error.NotFound("Abastecimento", id))
            : Result<RefuelingView>.Success(item);
    }

    public Task<Result<PagedResult<RefuelingView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
