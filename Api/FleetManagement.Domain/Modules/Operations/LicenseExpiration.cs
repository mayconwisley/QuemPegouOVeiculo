using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class LicenseExpiration : IEntity
{
    private LicenseExpiration() { }

    public LicenseExpiration(Guid driverId, DateOnly date, bool expired)
    {
        Update(driverId, date, expired);
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public Guid DriverId { get; private set; }
    public DateOnly Date { get; private set; }
    public bool Expired { get; private set; }

    public void Update(Guid driverId, DateOnly date, bool expired)
    {
        var validatedDriverId = Guard.ValidId(driverId, "Motorista");
        var validatedDate = Guard.Date(date, "Data");

        DriverId = validatedDriverId;
        Date = validatedDate;
        Expired = expired;
    }
}
