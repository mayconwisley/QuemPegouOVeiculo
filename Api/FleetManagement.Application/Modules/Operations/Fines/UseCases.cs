using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.Fines;

public sealed record FineInput(Guid VehicleId, Guid DriverId, DateOnly Date,
    decimal Amount, int Points, string? Description);

public sealed record FineView(Guid Id, Guid VehicleId, Guid DriverId, DateOnly Date,
    decimal Amount, int Points, string Description);

public sealed class FineCommands(ICommandRepository<Fine> repository)
{
    public async Task<Result<Guid>> CreateAsync(FineInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new Fine(input.VehicleId, input.DriverId, input.Date,
                input.Amount, input.Points, input.Description);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(Guid id, FineInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Multa", id));
            item.Update(input.VehicleId, input.DriverId, input.Date,
                input.Amount, input.Points, input.Description);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Multa", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class FineQueries(IQueryRepository<Fine> repository)
{
    private static readonly Expression<Func<Fine, FineView>> Projection = x =>
        new FineView(x.Id, x.VehicleId, x.DriverId, x.Date, x.Amount, x.Points, x.Description);

    public async Task<Result<FineView>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<FineView>.Failure(Error.NotFound("Multa", id))
            : Result<FineView>.Success(item);
    }

    public Task<Result<PagedResult<FineView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
