using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class VehicleStatus : IEntity
{
    private VehicleStatus() { }

    public VehicleStatus(int vehicleId, DateTime startUtc, DateTime? endUtc, string description)
    {
        Update(vehicleId, startUtc, endUtc, description);
    }

    public int Id { get; private set; }
    public int VehicleId { get; private set; }
    public DateTime StartUtc { get; private set; }
    public DateTime? EndUtc { get; private set; }
    public string Description { get; private set; } = "";

    public void Update(int vehicleId, DateTime startUtc, DateTime? endUtc, string description)
    {
        var validatedVehicleId = Guard.PositiveId(vehicleId, "Veículo");
        var validatedStart = Guard.Utc(startUtc, "Início");
        DateTime? validatedEnd = endUtc is null ? null : Guard.Utc(endUtc.Value, "Fim");
        var validatedDescription = Guard.Required(description, "Descrição", 2000);
        if (validatedEnd < validatedStart)
            throw new DomainException("Fim deve ser posterior ao início.");

        VehicleId = validatedVehicleId;
        StartUtc = validatedStart;
        EndUtc = validatedEnd;
        Description = validatedDescription;
    }
}
