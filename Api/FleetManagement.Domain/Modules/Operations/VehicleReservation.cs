using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public static class ReservationStatuses
{
    public const string Confirmed = "Confirmed";
    public const string InUse = "InUse";
    public const string Completed = "Completed";
    public const string Cancelled = "Cancelled";
}

public sealed class VehicleReservation : IEntity
{
    private VehicleReservation() { }

    public VehicleReservation(int vehicleId, int driverId, DateTime startUtc, DateTime endUtc,
        string? purpose)
    {
        VehicleId = Guard.PositiveId(vehicleId, "Veículo");
        UpdateSchedule(driverId, startUtc, endUtc, purpose);
        Status = ReservationStatuses.Confirmed;
    }

    public int Id { get; private set; }
    public int VehicleId { get; private set; }
    public int DriverId { get; private set; }
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public string Purpose { get; private set; } = "";
    public string Status { get; private set; } = "";
    public int Revision { get; private set; }

    public void UpdateSchedule(int driverId, DateTime startUtc, DateTime endUtc, string? purpose)
    {
        if (Status is not ("" or ReservationStatuses.Confirmed))
            throw new DomainException("Somente reservas confirmadas podem ser alteradas.");
        var validatedDriverId = Guard.PositiveId(driverId, "Motorista");
        var validatedStart = Guard.Utc(startUtc, "Início da reserva");
        var validatedEnd = Guard.Utc(endUtc, "Fim da reserva");
        if (validatedEnd <= validatedStart)
            throw new DomainException("O fim da reserva deve ser posterior ao início.");
        var validatedPurpose = Guard.Required(purpose, "Finalidade", 500);
        DriverId = validatedDriverId;
        StartUtc = validatedStart;
        EndUtc = validatedEnd;
        Purpose = validatedPurpose;
        Revision++;
    }

    public void Start(DateTime nowUtc)
    {
        Guard.Utc(nowUtc, "Início da utilização");
        if (Status != ReservationStatuses.Confirmed)
            throw new DomainException("Somente reservas confirmadas podem iniciar uma saída.");
        if (nowUtc < StartUtc.AddMinutes(-15) || nowUtc >= EndUtc)
            throw new DomainException("A reserva pode iniciar até 15 minutos antes do horário previsto e antes do fim.");
        Status = ReservationStatuses.InUse;
        Revision++;
    }

    public void Complete()
    {
        if (Status != ReservationStatuses.InUse)
            throw new DomainException("A reserva não está em utilização.");
        Status = ReservationStatuses.Completed;
        Revision++;
    }

    public void Cancel()
    {
        if (Status != ReservationStatuses.Confirmed)
            throw new DomainException("Somente reservas confirmadas podem ser canceladas.");
        Status = ReservationStatuses.Cancelled;
        Revision++;
    }
}
