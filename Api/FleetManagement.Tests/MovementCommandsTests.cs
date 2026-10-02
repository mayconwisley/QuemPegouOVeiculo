using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations;
using FleetManagement.Application.Modules.Operations.Movements;
using FleetManagement.Application.Modules.Operations.Reservations;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Tests;

public sealed class MovementCommandsTests
{
    [Fact]
    public async Task CreateAsync_RejectsInactiveVehicle()
    {
        var repository = new FakeRepository();
        var commands = CreateCommands(repository, false, true);
        var input = new MovementInput(1, 2,
            new DateTime(2026, 9, 27, 12, 0, 0, DateTimeKind.Utc), null, 100, null, null);

        var result = await commands.CreateAsync(input, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.Conflict, result.Error.Type);
        Assert.False(repository.AddCalled);
    }

    [Fact]
    public async Task ConcludeAsync_ReturnsNotFound()
    {
        var repository = new FakeRepository();
        var commands = CreateCommands(repository, true, true);

        var result = await commands.ConcludeAsync(99,
            new CompleteMovementInput(new DateTime(2026, 9, 27, 13, 0, 0, DateTimeKind.Utc), 120),
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.Error.Type);
        Assert.Equal("Movimentação 99 não encontrado.", result.Error.Message);
    }

    [Fact]
    public async Task CreateAsync_ReturnsMissingReference()
    {
        var repository = new FakeRepository();
        var commands = CreateCommands(repository, null, true);
        var input = new MovementInput(99, 2,
            new DateTime(2026, 9, 27, 12, 0, 0, DateTimeKind.Utc), null, 100, null, null);

        var result = await commands.CreateAsync(input, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(ErrorType.NotFound, result.Error.Type);
        Assert.Equal("Veículo 99 não encontrado.", result.Error.Message);
        Assert.False(repository.AddCalled);
    }

    private sealed class FakeRegistrationsStatusReader(bool? vehicleActive, bool? driverActive) : IRegistrationStatusReader
    {
        public Task<bool?> IsVehicleActiveAsync(int id, CancellationToken cancellationToken) =>
            Task.FromResult<bool?>(vehicleActive);

        public Task<bool?> IsDriverActiveAsync(int id, CancellationToken cancellationToken) =>
            Task.FromResult<bool?>(driverActive);
    }

    private static MovementCommands CreateCommands(FakeRepository repository, bool? vehicle, bool? driver) =>
        new(repository, new FakeRegistrationsStatusReader(vehicle, driver), new FakeChecklistStore(),
            new FakeReservationRepository(), new FakeAvailability(), new FakeScheduleGuard());

    private sealed class FakeReservationRepository : ICommandRepository<VehicleReservation>
    {
        public Task<VehicleReservation?> GetByIdAsync(int id, CancellationToken ct) => Task.FromResult<VehicleReservation?>(null);
        public Task AddAsync(VehicleReservation entity, CancellationToken ct) => Task.CompletedTask;
        public void Remove(VehicleReservation entity) { }
        public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;
    }

    private sealed class FakeAvailability : IReservationAvailabilityReader
    {
        public Task<bool> HasOpenMovementConflictAsync(int vehicleId, DateTime startUtc,
            DateTime endUtc, CancellationToken ct) => Task.FromResult(false);
        public Task<bool> HasReservationConflictAsync(int vehicleId, DateTime startUtc,
            DateTime? endUtc, int? excludeReservationId, CancellationToken ct) => Task.FromResult(false);
    }

    private sealed class FakeScheduleGuard : IVehicleScheduleGuard
    {
        public Task<Result<T>> ExecuteAsync<T>(int vehicleId, Func<Task<Result<T>>> operation,
            CancellationToken ct) => operation();
        public Task<Result> ExecuteAsync(int vehicleId, Func<Task<Result>> operation,
            CancellationToken ct) => operation();
    }

    private sealed class FakeRepository : ICommandRepository<VehicleMovement>
    {
        public bool AddCalled { get; private set; }

        public Task<VehicleMovement?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            Task.FromResult<VehicleMovement?>(null);

        public Task AddAsync(VehicleMovement entity, CancellationToken cancellationToken)
        {
            AddCalled = true;
            return Task.CompletedTask;
        }

        public void Remove(VehicleMovement entity) { }

        public Task SaveChangesAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

    private sealed class FakeChecklistStore : IMovementChecklistStore
    {
        public Task<MovementChecklist?> FindAsync(int movementId, string phase, CancellationToken ct) =>
            Task.FromResult<MovementChecklist?>(null);
        public Task<IReadOnlyList<ChecklistView>> ListAsync(int movementId, CancellationToken ct) =>
            Task.FromResult<IReadOnlyList<ChecklistView>>([]);
        public Task AddAsync(MovementChecklist checklist, CancellationToken ct) => Task.CompletedTask;
        public Task DeleteForMovementAsync(int movementId, CancellationToken ct) => Task.CompletedTask;
        public Task SaveAsync(CancellationToken ct) => Task.CompletedTask;
    }
}
