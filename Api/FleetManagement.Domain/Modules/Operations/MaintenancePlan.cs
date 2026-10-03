using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class MaintenancePlan : IEntity
{
    private MaintenancePlan() { }

    public MaintenancePlan(Guid vehicleId, string name, int? intervalDays, int? intervalMileage,
        DateOnly? nextDueDate, int? nextDueMileage)
    {
        Update(vehicleId, name, intervalDays, intervalMileage, nextDueDate, nextDueMileage, true);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid VehicleId { get; private set; }
    public string Name { get; private set; } = "";
    public int? IntervalDays { get; private set; }
    public int? IntervalMileage { get; private set; }
    public DateOnly? NextDueDate { get; private set; }
    public int? NextDueMileage { get; private set; }
    public DateOnly? LastCompletedOn { get; private set; }
    public int? LastCompletedMileage { get; private set; }
    public bool IsActive { get; private set; }
    public int Revision { get; private set; }

    public void Update(Guid vehicleId, string name, int? intervalDays, int? intervalMileage,
        DateOnly? nextDueDate, int? nextDueMileage, bool isActive)
    {
        var validatedVehicleId = Guard.ValidId(vehicleId, "Veículo");
        if (VehicleId != Guid.Empty && VehicleId != validatedVehicleId)
            throw new DomainException("O veículo do plano não pode ser alterado. Crie um novo plano.");
        var validatedName = Guard.Required(name, "Serviço preventivo", 120);
        if (intervalDays is not null && intervalDays <= 0 || intervalMileage is not null && intervalMileage <= 0)
            throw new DomainException("Os intervalos preventivos devem ser maiores que zero.");
        if (intervalDays.HasValue != nextDueDate.HasValue || intervalMileage.HasValue != nextDueMileage.HasValue ||
            intervalDays is null && intervalMileage is null)
            throw new DomainException("Informe pelo menos um intervalo e sua próxima data ou quilometragem.");
        if (nextDueDate is not null) Guard.Date(nextDueDate.Value, "Próxima manutenção");
        if (nextDueMileage is not null) Guard.NonNegative(nextDueMileage.Value, "Próxima quilometragem");
        VehicleId = validatedVehicleId;
        Name = validatedName;
        IntervalDays = intervalDays;
        IntervalMileage = intervalMileage;
        NextDueDate = nextDueDate;
        NextDueMileage = nextDueMileage;
        IsActive = isActive;
        Revision++;
    }

    public void Complete(DateOnly date, int mileage)
    {
        if (!IsActive)
            throw new DomainException("O plano preventivo está inativo.");
        Guard.Date(date, "Data da manutenção");
        Guard.NonNegative(mileage, "Quilometragem da manutenção");
        if (LastCompletedOn is not null && date < LastCompletedOn)
            throw new DomainException("A manutenção não pode ser anterior à última conclusão.");
        if (LastCompletedMileage is not null && mileage < LastCompletedMileage)
            throw new DomainException("A quilometragem não pode ser menor que a última conclusão.");
        if (LastCompletedOn == date && LastCompletedMileage == mileage)
            throw new DomainException("Esta manutenção já foi concluída.");
        if (IntervalMileage is not null && mileage > int.MaxValue - IntervalMileage.Value)
            throw new DomainException("A próxima quilometragem excede o limite permitido.");
        if (IntervalDays is not null && date.DayNumber > DateOnly.MaxValue.DayNumber - IntervalDays.Value)
            throw new DomainException("A próxima data excede o limite permitido.");
        LastCompletedOn = date;
        LastCompletedMileage = mileage;
        NextDueDate = IntervalDays is null ? null : date.AddDays(IntervalDays.Value);
        NextDueMileage = IntervalMileage is null ? null : checked(mileage + IntervalMileage.Value);
        Revision++;
    }
}
