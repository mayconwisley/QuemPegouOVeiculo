using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class Maintenance : IEntity
{
    private Maintenance() { }

    public Maintenance(int vehicleId, DateOnly date, decimal amount, string? description)
    {
        Update(vehicleId, date, amount, description);
    }

    public int Id { get; private set; }
    public int VehicleId { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; } = "";

    public void Update(int vehicleId, DateOnly date, decimal amount, string? description)
    {
        var validatedVehicleId = Guard.PositiveId(vehicleId, "Veículo");
        var validatedDate = Guard.Date(date, "Data");
        var validatedAmount = Guard.NonNegative(amount, "Valor");
        var validatedDescription = Guard.Optional(description, "Descrição", 2000);

        VehicleId = validatedVehicleId;
        Date = validatedDate;
        Amount = validatedAmount;
        Description = validatedDescription;
    }
}
