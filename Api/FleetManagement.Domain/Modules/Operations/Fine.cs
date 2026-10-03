using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class Fine : IEntity
{
    private Fine() { }

    public Fine(Guid vehicleId, Guid driverId, DateOnly date, decimal amount, int points, string? description)
    {
        Update(vehicleId, driverId, date, amount, points, description);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid VehicleId { get; private set; }
    public Guid DriverId { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal Amount { get; private set; }
    public int Points { get; private set; }
    public string Description { get; private set; } = "";

    public void Update(Guid vehicleId, Guid driverId, DateOnly date, decimal amount, int points, string? description)
    {
        var validatedVehicleId = Guard.ValidId(vehicleId, "Veículo");
        var validatedDriverId = Guard.ValidId(driverId, "Motorista");
        var validatedDate = Guard.Date(date, "Data");
        var validatedAmount = Guard.NonNegative(amount, "Valor");
        var pointsValidados = Guard.NonNegative(points, "Pontos");
        var validatedDescription = Guard.Optional(description, "Descrição", 2000);

        VehicleId = validatedVehicleId;
        DriverId = validatedDriverId;
        Date = validatedDate;
        Amount = validatedAmount;
        Points = pointsValidados;
        Description = validatedDescription;
    }
}
