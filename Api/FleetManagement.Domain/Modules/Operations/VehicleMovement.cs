using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class VehicleMovement : IEntity
{
    private VehicleMovement() { }

    public VehicleMovement(Guid vehicleId, Guid driverId, DateTime departureUtc, int initialMileage, string? description)
    {
        Update(vehicleId, driverId, departureUtc, null, initialMileage, null, description);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid VehicleId { get; private set; }
    public Guid DriverId { get; private set; }
    public Guid? ReservationId { get; private set; }
    public DateTime DepartureUtc { get; private set; }
    public DateTime? ArrivalUtc { get; private set; }
    public DateTime? ExpectedReturnUtc { get; private set; }
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

    public void Update(Guid vehicleId, Guid driverId, DateTime departureUtc, DateTime? arrivalUtc,
        int initialMileage, int? finalMileage, string? description)
    {
        var validatedVehicleId = Guard.ValidId(vehicleId, "Veículo");
        var validatedDriverId = Guard.ValidId(driverId, "Motorista");
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
        if (ExpectedReturnUtc < validatedDeparture)
            throw new DomainException("A previsão de retorno deve ser posterior à saída.");

        VehicleId = validatedVehicleId;
        DriverId = validatedDriverId;
        DepartureUtc = validatedDeparture;
        ArrivalUtc = validatedArrival;
        InitialMileage = validatedInitialMileage;
        FinalMileage = validatedFinalMileage;
        Description = validatedDescription;
    }

    public void ScheduleReturn(DateTime? expectedReturnUtc)
    {
        if (expectedReturnUtc is not null && Guard.Utc(expectedReturnUtc.Value, "Previsão de retorno") < DepartureUtc)
            throw new DomainException("A previsão de retorno deve ser posterior à saída.");
        ExpectedReturnUtc = expectedReturnUtc;
    }

    public void LinkToReservation(Guid reservationId)
    {
        if (ReservationId is not null)
            throw new DomainException("A movimentação já está vinculada a uma reserva.");
        ReservationId = Guard.ValidId(reservationId, "Reserva");
    }
}
