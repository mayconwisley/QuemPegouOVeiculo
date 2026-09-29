using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class Refueling : IEntity
{
    private Refueling() { }

    public Refueling(int vehicleId, int driverId, int mileage, DateOnly date, decimal amount, decimal liters, string? description)
    {
        Update(vehicleId, driverId, mileage, date, amount, liters, description);
    }

    public int Id { get; private set; }
    public int VehicleId { get; private set; }
    public int DriverId { get; private set; }
    public int Mileage { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Liters { get; private set; }
    public string Description { get; private set; } = "";

    public void Update(int vehicleId, int driverId, int mileage, DateOnly date, decimal amount, decimal liters, string? description)
    {
        var validatedVehicleId = Guard.PositiveId(vehicleId, "Veículo");
        var validatedDriverId = Guard.PositiveId(driverId, "Motorista");
        var validatedMileage = Guard.NonNegative(mileage, "Quilometragem");
        var validatedDate = Guard.Date(date, "Data");
        var validatedAmount = Guard.NonNegative(amount, "Valor");
        if (liters <= 0)
            throw new DomainException("Litros deve ser maior que zero.");
        var validatedDescription = Guard.Optional(description, "Descrição", 2000);

        VehicleId = validatedVehicleId;
        DriverId = validatedDriverId;
        Mileage = validatedMileage;
        Date = validatedDate;
        Amount = validatedAmount;
        Liters = liters;
        Description = validatedDescription;
    }
}
