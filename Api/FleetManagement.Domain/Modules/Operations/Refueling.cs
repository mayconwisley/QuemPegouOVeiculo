using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class Refueling : IEntity
{
    private Refueling() { }

    public Refueling(Guid vehicleId, Guid driverId, int mileage, DateOnly date, decimal amount, decimal liters, string? description)
    {
        Update(vehicleId, driverId, mileage, date, amount, liters, description);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid VehicleId { get; private set; }
    public Guid DriverId { get; private set; }
    public int Mileage { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Liters { get; private set; }
    public string Description { get; private set; } = "";

    public void Update(Guid vehicleId, Guid driverId, int mileage, DateOnly date, decimal amount, decimal liters, string? description)
    {
        var validatedVehicleId = Guard.ValidId(vehicleId, "Veículo");
        var validatedDriverId = Guard.ValidId(driverId, "Motorista");
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
