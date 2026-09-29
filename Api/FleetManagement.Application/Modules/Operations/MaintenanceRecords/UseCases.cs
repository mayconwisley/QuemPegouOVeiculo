using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.MaintenanceRecords;

public sealed record MaintenanceInput(int VehicleId, DateOnly Date, decimal Amount, string? Description);
public sealed record MaintenanceView(int Id, int VehicleId, DateOnly Date, decimal Amount, string Description);

public sealed class MaintenanceCommands(ICommandRepository<Maintenance> repository)
{
    public async Task<Result<int>> CreateAsync(MaintenanceInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new Maintenance(input.VehicleId, input.Date, input.Amount, input.Description);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, MaintenanceInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Manutenção", id));
            item.Update(input.VehicleId, input.Date, input.Amount, input.Description);
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
                return Result.Failure(Error.NotFound("Manutenção", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class MaintenanceQueries(IQueryRepository<Maintenance> repository)
{
    private static readonly Expression<Func<Maintenance, MaintenanceView>> Projection = x =>
        new MaintenanceView(x.Id, x.VehicleId, x.Date, x.Amount, x.Description);

    public async Task<Result<MaintenanceView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<MaintenanceView>.Failure(Error.NotFound("Manutenção", id))
            : Result<MaintenanceView>.Success(item);
    }

    public Task<Result<PagedResult<MaintenanceView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
