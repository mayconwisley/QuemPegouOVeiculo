using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.VehicleStatuses;

public sealed record VehicleStatusInput(Guid VehicleId, DateTime StartUtc, DateTime? EndUtc, string Description);
public sealed record VehicleStatusView(Guid Id, Guid VehicleId, DateTime StartUtc, DateTime? EndUtc, string Description);

public sealed class VehicleStatusCommands(ICommandRepository<VehicleStatus> repository)
{
    public async Task<Result<Guid>> CreateAsync(VehicleStatusInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new VehicleStatus(input.VehicleId, input.StartUtc, input.EndUtc, input.Description);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(Guid id, VehicleStatusInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Status do veículo", id));
            item.Update(input.VehicleId, input.StartUtc, input.EndUtc, input.Description);
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
                return Result.Failure(Error.NotFound("Status do veículo", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class VehicleStatusQueries(IQueryRepository<VehicleStatus> repository)
{
    private static readonly Expression<Func<VehicleStatus, VehicleStatusView>> Projection = x =>
        new VehicleStatusView(x.Id, x.VehicleId, x.StartUtc, x.EndUtc, x.Description);

    public async Task<Result<VehicleStatusView>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<VehicleStatusView>.Failure(Error.NotFound("Status do veículo", id))
            : Result<VehicleStatusView>.Success(item);
    }

    public Task<Result<PagedResult<VehicleStatusView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
