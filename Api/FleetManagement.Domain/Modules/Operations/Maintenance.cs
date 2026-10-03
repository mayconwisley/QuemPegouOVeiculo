using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class Maintenance : IEntity
{
    private Maintenance() { }

    public Maintenance(Guid vehicleId, DateOnly date, decimal amount, string? description)
    {
        Update(vehicleId, date, amount, description);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid VehicleId { get; private set; }
    public Guid? PlanId { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = "";

    public void LinkToPlan(Guid planId) => PlanId = Guard.ValidId(planId, "Plano preventivo");

    public void Update(Guid vehicleId, DateOnly date, decimal amount, string? description)
    {
        var validatedVehicleId = Guard.ValidId(vehicleId, "Veículo");
        var validatedDate = Guard.Date(date, "Data");
        var validatedAmount = Guard.NonNegative(amount, "Valor");
        var validatedDescription = Guard.Optional(description, "Descrição", 2000);

        VehicleId = validatedVehicleId;
        Date = validatedDate;
        Amount = validatedAmount;
        Description = validatedDescription;
    }
}
