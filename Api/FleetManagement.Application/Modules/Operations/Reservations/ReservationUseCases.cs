using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations;
using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.Reservations;

public sealed record ReservationInput(Guid VehicleId, Guid DriverId, DateTime StartUtc,
    DateTime EndUtc, string? Purpose);

public sealed record ReservationView(Guid Id, Guid VehicleId, Guid DriverId, DateTime StartUtc,
    DateTime EndUtc, string Purpose, string Status, string Plate, string VehicleModel,
    string DriverName, Guid? MovementId);

public sealed record ReservationFilter(Guid? VehicleId = null, string? Status = null,
    DateTime? FromUtc = null, DateTime? ToUtc = null)
{
    public ReservationFilter Validate()
    {
        if (VehicleId == Guid.Empty)
            throw new DomainException("O identificador do veículo deve ser válido.");
        if (Status is not null and not (ReservationStatuses.Confirmed or ReservationStatuses.InUse
            or ReservationStatuses.Completed or ReservationStatuses.Cancelled))
            throw new DomainException("Status da reserva inválido.");
        if (FromUtc?.Kind is not null and not DateTimeKind.Utc ||
            ToUtc?.Kind is not null and not DateTimeKind.Utc)
            throw new DomainException("Os horários dos filtros devem estar em UTC.");
        if (FromUtc >= ToUtc)
            throw new DomainException("O início do período deve ser anterior ao fim.");
        return this;
    }
}

public interface IReservationReadRepository
{
    Task<ReservationView?> GetAsync(Guid id, CancellationToken ct);
    Task<PagedResult<ReservationView>> ListAsync(ReservationFilter filter, PageRequest page,
        CancellationToken ct);
}

public sealed record StartReservationInput(int InitialMileage, string? Description);

public interface IReservationAvailabilityReader
{
    Task<bool> HasOpenMovementConflictAsync(Guid vehicleId, DateTime startUtc, DateTime endUtc,
        CancellationToken ct);
    Task<bool> HasReservationConflictAsync(Guid vehicleId, DateTime startUtc, DateTime? endUtc,
        Guid? excludeReservationId, CancellationToken ct);
}

public interface IVehicleScheduleGuard
{
    Task<Result<T>> ExecuteAsync<T>(Guid vehicleId, Func<Task<Result<T>>> operation,
        CancellationToken ct);
    Task<Result> ExecuteAsync(Guid vehicleId, Func<Task<Result>> operation, CancellationToken ct);
}

public sealed class ReservationCommands(ICommandRepository<VehicleReservation> reservations,
    ICommandRepository<VehicleMovement> movements, IRegistrationStatusReader registrations,
    IReservationAvailabilityReader availability, IVehicleScheduleGuard guard)
{
    public Task<Result<Guid>> CreateAsync(ReservationInput input, CancellationToken ct) =>
        Result.CaptureValueAsync(() => guard.ExecuteAsync(input.VehicleId, async () =>
        {
            var reservation = new VehicleReservation(input.VehicleId, input.DriverId,
                input.StartUtc, input.EndUtc, input.Purpose);
            var references = await EnsureReferencesActiveAsync(input.VehicleId, input.DriverId, ct);
            if (!references.IsSuccess)
                return Result<Guid>.Failure(references.Error);
            if (await availability.HasOpenMovementConflictAsync(input.VehicleId,
                    input.StartUtc, input.EndUtc, ct))
                return Result<Guid>.Failure(Error.Conflict(
                    "O veículo está em uma movimentação aberta nesse período."));
            if (await availability.HasReservationConflictAsync(input.VehicleId,
                    input.StartUtc, input.EndUtc, null, ct))
                return Result<Guid>.Failure(Error.Conflict("Já existe uma reserva para o veículo nesse período."));
            await reservations.AddAsync(reservation, ct);
            await reservations.SaveChangesAsync(ct);
            return Result<Guid>.Success(reservation.Id);
        }, ct));

    public Task<Result> UpdateAsync(Guid id, ReservationInput input, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            var reservation = await reservations.GetByIdAsync(id, ct);
            if (reservation is null)
                return Result.Failure(Error.NotFound("Reserva", id));
            if (reservation.VehicleId != input.VehicleId)
                return Result.Failure(Error.Conflict("O veículo da reserva não pode ser alterado."));
            return await guard.ExecuteAsync(reservation.VehicleId, async () =>
            {
                reservation.UpdateSchedule(input.DriverId, input.StartUtc, input.EndUtc, input.Purpose);
                var references = await EnsureReferencesActiveAsync(input.VehicleId, input.DriverId, ct);
                if (!references.IsSuccess)
                    return references;
                if (await availability.HasOpenMovementConflictAsync(input.VehicleId,
                        input.StartUtc, input.EndUtc, ct))
                    return Result.Failure(Error.Conflict(
                        "O veículo está em uma movimentação aberta nesse período."));
                if (await availability.HasReservationConflictAsync(input.VehicleId,
                        input.StartUtc, input.EndUtc, id, ct))
                    return Result.Failure(Error.Conflict("Já existe uma reserva para o veículo nesse período."));
                await reservations.SaveChangesAsync(ct);
                return Result.Success();
            }, ct);
        });

    public Task<Result> CancelAsync(Guid id, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            var reservation = await reservations.GetByIdAsync(id, ct);
            if (reservation is null)
                return Result.Failure(Error.NotFound("Reserva", id));
            reservation.Cancel();
            await reservations.SaveChangesAsync(ct);
            return Result.Success();
        });

    public Task<Result<Guid>> StartAsync(Guid id, StartReservationInput input, CancellationToken ct) =>
        Result.CaptureValueAsync(async () =>
        {
            var reservation = await reservations.GetByIdAsync(id, ct);
            if (reservation is null)
                return Result<Guid>.Failure(Error.NotFound("Reserva", id));
            return await guard.ExecuteAsync(reservation.VehicleId, async () =>
            {
                var now = DateTime.UtcNow;
                reservation.Start(now);
                var references = await EnsureReferencesActiveAsync(reservation.VehicleId,
                    reservation.DriverId, ct);
                if (!references.IsSuccess)
                    return Result<Guid>.Failure(references.Error);
                if (await availability.HasOpenMovementConflictAsync(reservation.VehicleId,
                        now, reservation.EndUtc, ct))
                    return Result<Guid>.Failure(Error.Conflict("O veículo já está em utilização."));
                if (await availability.HasReservationConflictAsync(reservation.VehicleId,
                        now, reservation.EndUtc, reservation.Id, ct))
                    return Result<Guid>.Failure(Error.Conflict(
                        "Existe outra reserva para o veículo no horário de início da saída."));
                var movement = new VehicleMovement(reservation.VehicleId, reservation.DriverId,
                    now, input.InitialMileage, input.Description);
                movement.ScheduleReturn(reservation.EndUtc);
                movement.LinkToReservation(id);
                await movements.AddAsync(movement, ct);
                await movements.SaveChangesAsync(ct);
                return Result<Guid>.Success(movement.Id);
            }, ct);
        });

    private async Task<Result> EnsureReferencesActiveAsync(Guid vehicleId, Guid driverId, CancellationToken ct)
    {
        var vehicle = await registrations.IsVehicleActiveAsync(vehicleId, ct);
        var driver = await registrations.IsDriverActiveAsync(driverId, ct);
        if (vehicle is null) return Result.Failure(Error.NotFound("Veículo", vehicleId));
        if (driver is null) return Result.Failure(Error.NotFound("Motorista", driverId));
        return vehicle.Value && driver.Value ? Result.Success()
            : Result.Failure(Error.Conflict("Veículo e motorista devem estar ativos."));
    }
}

public sealed class ReservationQueries(IReservationReadRepository reservations)
{
    public Task<Result<ReservationView>> GetAsync(Guid id, CancellationToken ct) =>
        Result.CaptureValueAsync(async () =>
        {
            var reservation = await reservations.GetAsync(id, ct);
            return reservation is null
                ? Result<ReservationView>.Failure(Error.NotFound("Reserva", id))
                : Result<ReservationView>.Success(reservation);
        });

    public Task<Result<PagedResult<ReservationView>>> ListAsync(ReservationFilter filter,
        PageRequest page, CancellationToken ct) =>
        Result.TryAsync(() => reservations.ListAsync(filter.Validate(), page, ct));
}
