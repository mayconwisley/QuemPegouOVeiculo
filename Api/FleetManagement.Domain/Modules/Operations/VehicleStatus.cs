using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class VehicleStatus : IEntity
{
    private VehicleStatus() { }

    public VehicleStatus(Guid vehicleId, DateTime startUtc, DateTime? endUtc, string description)
    {
        Update(vehicleId, startUtc, endUtc, description);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid VehicleId { get; private set; }
    public DateTime StartUtc { get; private set; }
    public DateTime? EndUtc { get; private set; }
    public string Description { get; private set; } = "";

    public void Update(Guid vehicleId, DateTime startUtc, DateTime? endUtc, string description)
    {
        var validatedVehicleId = Guard.ValidId(vehicleId, "Veículo");
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
