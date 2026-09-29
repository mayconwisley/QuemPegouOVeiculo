using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Operations;

public sealed class LicenseExpiration : IEntity
{
    private LicenseExpiration() { }

    public LicenseExpiration(int driverId, DateOnly date, bool expired)
    {
        Update(driverId, date, expired);
    }

    public int Id { get; private set; }
    public int DriverId { get; private set; }
    public DateOnly Date { get; private set; }
    public bool Expired { get; private set; }

    public void Update(int driverId, DateOnly date, bool expired)
    {
        var validatedDriverId = Guard.PositiveId(driverId, "Motorista");
        var validatedDate = Guard.Date(date, "Data");

        DriverId = validatedDriverId;
        Date = validatedDate;
        Expired = expired;
    }
}
