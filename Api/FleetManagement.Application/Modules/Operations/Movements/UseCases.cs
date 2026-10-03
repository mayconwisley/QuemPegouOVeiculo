using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations;
using FleetManagement.Domain.Modules.Operations;
using FleetManagement.Application.Modules.Operations.Reservations;

namespace FleetManagement.Application.Modules.Operations.Movements;

public sealed record MovementInput(Guid VehicleId, Guid DriverId, DateTime DepartureUtc,
    DateTime? ArrivalUtc, int InitialMileage, int? FinalMileage, string? Description,
    DateTime? ExpectedReturnUtc = null);

public sealed record CompleteMovementInput(DateTime ArrivalUtc, int FinalMileage);

public sealed record MovementView(Guid Id, Guid VehicleId, Guid DriverId, DateTime DepartureUtc,
    DateTime? ArrivalUtc, int InitialMileage, int? FinalMileage, string Description, bool IsOpen,
    DateTime? ExpectedReturnUtc, Guid? ReservationId);

public sealed class MovementCommands(
    ICommandRepository<VehicleMovement> repository,
    IRegistrationStatusReader registrations,
    IMovementChecklistStore checklists,
    ICommandRepository<VehicleReservation> reservations,
    IReservationAvailabilityReader availability,
    IVehicleScheduleGuard guard)
{
    public Task<Result<Guid>> CreateAsync(MovementInput input, CancellationToken cancellationToken) =>
        Result.CaptureValueAsync(() => guard.ExecuteAsync(input.VehicleId, async () =>
        {
            var movement = new VehicleMovement(input.VehicleId, input.DriverId,
                input.DepartureUtc, input.InitialMileage, input.Description);
            movement.Update(input.VehicleId, input.DriverId, input.DepartureUtc,
                input.ArrivalUtc, input.InitialMileage, input.FinalMileage, input.Description);
            movement.ScheduleReturn(input.ExpectedReturnUtc);
            var references = await EnsureReferencesActiveAsync(input.VehicleId, input.DriverId, cancellationToken);
            if (!references.IsSuccess)
                return Result<Guid>.Failure(references.Error);
            if (await availability.HasReservationConflictAsync(input.VehicleId, input.DepartureUtc,
                    input.ArrivalUtc ?? input.ExpectedReturnUtc, null, cancellationToken))
                return Result<Guid>.Failure(Error.Conflict(
                    "Existe uma reserva confirmada para o veículo nesse período."));

            await repository.AddAsync(movement, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(movement.Id);
        }, cancellationToken));

    public async Task<Result> UpdateAsync(Guid id, MovementInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var movement = await repository.GetByIdAsync(id, cancellationToken);
            if (movement is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            return await guard.ExecuteAsync(input.VehicleId, async () =>
            {
                if (movement.ReservationId is not null &&
                    (input.VehicleId != movement.VehicleId || input.DriverId != movement.DriverId ||
                     input.DepartureUtc != movement.DepartureUtc || input.ArrivalUtc != movement.ArrivalUtc))
                    return Result.Failure(Error.Conflict(
                        "A saída vinculada à reserva só permite alterar quilometragem e descrição."));
                movement.Update(input.VehicleId, input.DriverId, input.DepartureUtc,
                    input.ArrivalUtc, input.InitialMileage, input.FinalMileage, input.Description);
                if (input.ExpectedReturnUtc is not null && movement.ReservationId is null)
                    movement.ScheduleReturn(input.ExpectedReturnUtc);
                var references = await EnsureReferencesActiveAsync(input.VehicleId, input.DriverId, cancellationToken);
                if (!references.IsSuccess)
                    return references;
                if (movement.ReservationId is null && await availability.HasReservationConflictAsync(
                        input.VehicleId, input.DepartureUtc,
                        input.ArrivalUtc ?? movement.ExpectedReturnUtc, null, cancellationToken))
                    return Result.Failure(Error.Conflict(
                        "Existe uma reserva confirmada para o veículo nesse período."));
                await repository.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }, cancellationToken);
        });
    }

    public async Task<Result> ConcludeAsync(Guid id, CompleteMovementInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var movement = await repository.GetByIdAsync(id, cancellationToken);
            if (movement is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            movement.Complete(input.ArrivalUtc, input.FinalMileage);
            if (movement.ReservationId is Guid reservationId)
            {
                var reservation = await reservations.GetByIdAsync(reservationId, cancellationToken);
                if (reservation is null)
                    return Result.Failure(Error.NotFound("Reserva", reservationId));
                reservation.Complete();
            }
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public Task<Result> ScheduleReturnAsync(Guid id, DateTime? expectedReturnUtc, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            var movement = await repository.GetByIdAsync(id, ct);
            if (movement is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            if (!movement.IsOpen)
                return Result.Failure(Error.Conflict("A movimentação já foi concluída."));
            if (movement.ReservationId is not null)
                return Result.Failure(Error.Conflict("A previsão está definida pela reserva vinculada."));
            return await guard.ExecuteAsync(movement.VehicleId, async () =>
            {
                movement.ScheduleReturn(expectedReturnUtc);
                if (await availability.HasReservationConflictAsync(movement.VehicleId,
                        movement.DepartureUtc, expectedReturnUtc, null, ct))
                    return Result.Failure(Error.Conflict(
                        "Existe uma reserva confirmada para o veículo nesse período."));
                await repository.SaveChangesAsync(ct);
                return Result.Success();
            }, ct);
        });

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            if (item.ReservationId is not null)
                return Result.Failure(Error.Conflict("Uma saída vinculada à reserva não pode ser excluída."));
            await checklists.DeleteForMovementAsync(id, cancellationToken);
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    private async Task<Result> EnsureReferencesActiveAsync(Guid vehicleId, Guid driverId,
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
            x.InitialMileage, x.FinalMileage, x.Description, x.ArrivalUtc == null,
            x.ExpectedReturnUtc, x.ReservationId);

    public async Task<Result<MovementView>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<MovementView>.Failure(Error.NotFound("Movimentação", id))
            : Result<MovementView>.Success(item);
    }

    public Task<Result<PagedResult<MovementView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
