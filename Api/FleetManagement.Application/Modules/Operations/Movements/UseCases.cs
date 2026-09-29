using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.Movements;

public sealed record MovementInput(int VehicleId, int DriverId, DateTime DepartureUtc,
    DateTime? ArrivalUtc, int InitialMileage, int? FinalMileage, string? Description);

public sealed record CompleteMovementInput(DateTime ArrivalUtc, int FinalMileage);

public sealed record MovementView(int Id, int VehicleId, int DriverId, DateTime DepartureUtc,
    DateTime? ArrivalUtc, int InitialMileage, int? FinalMileage, string Description, bool IsOpen);

public sealed class MovementCommands(
    ICommandRepository<VehicleMovement> repository,
    IRegistrationStatusReader registrations)
{
    public async Task<Result<int>> CreateAsync(MovementInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureValueAsync<int>(async () =>
        {
            var movement = new VehicleMovement(input.VehicleId, input.DriverId,
                input.DepartureUtc, input.InitialMileage, input.Description);
            movement.Update(input.VehicleId, input.DriverId, input.DepartureUtc,
                input.ArrivalUtc, input.InitialMileage, input.FinalMileage, input.Description);
            var references = await EnsureReferencesActiveAsync(input.VehicleId, input.DriverId, cancellationToken);
            if (!references.IsSuccess)
                return Result<int>.Failure(references.Error);

            await repository.AddAsync(movement, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(movement.Id);
        });
    }

    public async Task<Result> UpdateAsync(int id, MovementInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var movement = await repository.GetByIdAsync(id, cancellationToken);
            if (movement is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            movement.Update(input.VehicleId, input.DriverId, input.DepartureUtc,
                input.ArrivalUtc, input.InitialMileage, input.FinalMileage, input.Description);
            var references = await EnsureReferencesActiveAsync(input.VehicleId, input.DriverId, cancellationToken);
            if (!references.IsSuccess)
                return references;
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public async Task<Result> ConcludeAsync(int id, CompleteMovementInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var movement = await repository.GetByIdAsync(id, cancellationToken);
            if (movement is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            movement.Complete(input.ArrivalUtc, input.FinalMileage);
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
                return Result.Failure(Error.NotFound("Movimentação", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    private async Task<Result> EnsureReferencesActiveAsync(int vehicleId, int driverId,
        CancellationToken cancellationToken)
    {
        var vehicleActive = await registrations.IsVehicleActiveAsync(vehicleId, cancellationToken);
        var driverActive = await registrations.IsDriverActiveAsync(driverId, cancellationToken);
        if (vehicleActive is null)
            return Result.Failure(Error.NotFound("Veículo", vehicleId));
        if (driverActive is null)
            return Result.Failure(Error.NotFound("Motorista", driverId));
        if (!vehicleActive.Value || !driverActive.Value)
            return Result.Failure(Error.Conflict(
                "Veículo e motorista devem estar ativos para registrar movimentação."));
        return Result.Success();
    }
}

public sealed class MovementQueries(IQueryRepository<VehicleMovement> repository)
{
    private static readonly Expression<Func<VehicleMovement, MovementView>> Projection = x =>
        new MovementView(x.Id, x.VehicleId, x.DriverId, x.DepartureUtc, x.ArrivalUtc,
            x.InitialMileage, x.FinalMileage, x.Description, x.ArrivalUtc == null);

    public async Task<Result<MovementView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<MovementView>.Failure(Error.NotFound("Movimentação", id))
            : Result<MovementView>.Success(item);
    }

    public Task<Result<PagedResult<MovementView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
