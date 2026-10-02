using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.MaintenanceRecords;

public sealed record MaintenancePlanInput(int VehicleId, string Name, int? IntervalDays,
    int? IntervalMileage, DateOnly? NextDueDate, int? NextDueMileage, bool IsActive);

public sealed record MaintenancePlanView(int Id, int VehicleId, string Name, int? IntervalDays,
    int? IntervalMileage, DateOnly? NextDueDate, int? NextDueMileage,
    DateOnly? LastCompletedOn, int? LastCompletedMileage, bool IsActive);

public sealed record CompletePlanInput(DateOnly Date, int Mileage, decimal Amount, string? Notes);

public interface IVehicleMileageReader
{
    Task<int> ReadMaximumAsync(int vehicleId, CancellationToken ct);
}

public sealed class MaintenancePlanCommands(ICommandRepository<MaintenancePlan> plans,
    ICommandRepository<Maintenance> records, IRegistrationStatusReader registrations,
    IVehicleMileageReader mileageReader)
{
    public Task<Result<int>> CreateAsync(MaintenancePlanInput input, CancellationToken ct) =>
        Result.CaptureValueAsync(async () =>
        {
            var plan = new MaintenancePlan(input.VehicleId, input.Name, input.IntervalDays,
                input.IntervalMileage, input.NextDueDate, input.NextDueMileage);
            if (!input.IsActive)
                plan.Update(input.VehicleId, input.Name, input.IntervalDays,
                    input.IntervalMileage, input.NextDueDate, input.NextDueMileage, false);
            if (await registrations.IsVehicleActiveAsync(input.VehicleId, ct) is not true)
                return Result<int>.Failure(Error.Conflict("O veículo deve existir e estar ativo."));
            await plans.AddAsync(plan, ct);
            await plans.SaveChangesAsync(ct);
            return Result<int>.Success(plan.Id);
        });

    public Task<Result> UpdateAsync(int id, MaintenancePlanInput input, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            var plan = await plans.GetByIdAsync(id, ct);
            if (plan is null)
                return Result.Failure(Error.NotFound("Plano preventivo", id));
            var vehicleActive = await registrations.IsVehicleActiveAsync(input.VehicleId, ct);
            if (vehicleActive is null)
                return Result.Failure(Error.NotFound("Veículo", input.VehicleId));
            if (input.IsActive && !vehicleActive.Value)
                return Result.Failure(Error.Conflict("O veículo deve estar ativo para manter o plano ativo."));
            plan.Update(input.VehicleId, input.Name, input.IntervalDays, input.IntervalMileage,
                input.NextDueDate, input.NextDueMileage, input.IsActive);
            await plans.SaveChangesAsync(ct);
            return Result.Success();
        });

    public Task<Result> CompleteAsync(int id, CompletePlanInput input, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            var plan = await plans.GetByIdAsync(id, ct);
            if (plan is null)
                return Result.Failure(Error.NotFound("Plano preventivo", id));
            if (input.Mileage < await mileageReader.ReadMaximumAsync(plan.VehicleId, ct))
                return Result.Failure(Error.Validation(
                    "A quilometragem da manutenção não pode ser menor que a já registrada para o veículo."));
            plan.Complete(input.Date, input.Mileage);
            var record = new Maintenance(plan.VehicleId, input.Date, input.Amount,
                string.IsNullOrWhiteSpace(input.Notes) ? plan.Name : plan.Name + " — " + input.Notes.Trim());
            record.LinkToPlan(plan.Id);
            await records.AddAsync(record, ct);
            await plans.SaveChangesAsync(ct);
            return Result.Success();
        });
}

public sealed class MaintenancePlanQueries(IQueryRepository<MaintenancePlan> plans)
{
    private static readonly Expression<Func<MaintenancePlan, MaintenancePlanView>> Projection = x =>
        new MaintenancePlanView(x.Id, x.VehicleId, x.Name, x.IntervalDays, x.IntervalMileage,
            x.NextDueDate, x.NextDueMileage, x.LastCompletedOn, x.LastCompletedMileage, x.IsActive);

    public Task<Result<MaintenancePlanView>> GetAsync(int id, CancellationToken ct) =>
        Result.CaptureValueAsync(async () =>
        {
            var plan = await plans.GetByIdAsync(id, Projection, ct);
            return plan is null ? Result<MaintenancePlanView>.Failure(Error.NotFound("Plano preventivo", id))
                : Result<MaintenancePlanView>.Success(plan);
        });

    public Task<Result<PagedResult<MaintenancePlanView>>> ListAsync(PageRequest page, CancellationToken ct) =>
        Result.TryAsync(() => plans.ListAsync(page, Projection, ct));
}
