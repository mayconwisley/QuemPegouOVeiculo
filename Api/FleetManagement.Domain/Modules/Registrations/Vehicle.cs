using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Registrations;

public sealed class Vehicle : IEntity
{
    private Vehicle() { }

    public Vehicle(string plate, string model, string? chassis, string? renavam, bool active)
    {
        Update(plate, model, chassis, renavam, active);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public string Plate { get; private set; } = "";
    public string Model { get; private set; } = "";
    public string Chassis { get; private set; } = "";
    public string Renavam { get; private set; } = "";
    public bool Active { get; private set; }

    public void Update(string plate, string model, string? chassis, string? renavam, bool active)
    {
        var validatedPlate = Guard.Plate(plate);
        var validatedModel = Guard.Required(model, "Modelo", 150);
        var validatedChassis = Guard.Optional(chassis, "Chassi", 30);
        var validatedRenavam = Guard.Optional(renavam, "Renavam", 20);

        Plate = validatedPlate;
        Model = validatedModel;
        Chassis = validatedChassis;
        Renavam = validatedRenavam;
        Active = active;
    }
}
