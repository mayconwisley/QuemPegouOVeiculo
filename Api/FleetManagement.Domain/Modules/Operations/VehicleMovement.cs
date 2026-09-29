using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class VehicleMovement : IEntity
{
    private VehicleMovement() { }

    public VehicleMovement(int vehicleId, int driverId, DateTime departureUtc, int initialMileage, string? description)
    {
        Update(vehicleId, driverId, departureUtc, null, initialMileage, null, description);
    }

    public int Id { get; private set; }
    public int VehicleId { get; private set; }
    public int DriverId { get; private set; }
    public DateTime DepartureUtc { get; private set; }
    public DateTime? ArrivalUtc { get; private set; }
    public int InitialMileage { get; private set; }
    public int? FinalMileage { get; private set; }
    public string Description { get; private set; } = "";
    public bool IsOpen => ArrivalUtc is null;

    public void Complete(DateTime arrivalUtc, int finalMileage)
    {
        if (!IsOpen)
            throw new DomainException("A movimentação já foi concluída.");
        Update(VehicleId, DriverId, DepartureUtc, arrivalUtc, InitialMileage, finalMileage, Description);
    }

    public void Update(int vehicleId, int driverId, DateTime departureUtc, DateTime? arrivalUtc,
        int initialMileage, int? finalMileage, string? description)
    {
        var validatedVehicleId = Guard.PositiveId(vehicleId, "Veículo");
        var validatedDriverId = Guard.PositiveId(driverId, "Motorista");
        var validatedDeparture = Guard.Utc(departureUtc, "Saída");
        DateTime? validatedArrival = arrivalUtc is null ? null : Guard.Utc(arrivalUtc.Value, "Chegada");
        var validatedInitialMileage = Guard.NonNegative(initialMileage, "Quilometragem inicial");
        int? validatedFinalMileage = finalMileage is null ? null : Guard.NonNegative(finalMileage.Value, "Quilometragem final");
        var validatedDescription = Guard.Optional(description, "Descrição", 2000);

        if (validatedArrival.HasValue != validatedFinalMileage.HasValue)
            throw new DomainException("Chegada e quilometragem final devem ser informadas juntas.");
        if (validatedArrival < validatedDeparture)
            throw new DomainException("Chegada deve ser posterior à saída.");
        if (validatedFinalMileage < validatedInitialMileage)
            throw new DomainException("Quilometragem final deve ser maior ou igual à inicial.");

        VehicleId = validatedVehicleId;
        DriverId = validatedDriverId;
        DepartureUtc = validatedDeparture;
        ArrivalUtc = validatedArrival;
        InitialMileage = validatedInitialMileage;
        FinalMileage = validatedFinalMileage;
        Description = validatedDescription;
    }
}
