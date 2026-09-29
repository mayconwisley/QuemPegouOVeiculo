using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Tests;

public sealed class MovementVehicleTests
{
    private static readonly DateTime Departure = new(2026, 9, 27, 12, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void Complete_RequiresConsistentArrivalAndMileage()
    {
        var movement = new VehicleMovement(1, 2, Departure, 100, null);

        Assert.Throws<DomainException>(() => movement.Complete(Departure.AddMinutes(-1), 110));
        Assert.Throws<DomainException>(() => movement.Complete(Departure.AddMinutes(1), 99));
    }

    [Fact]
    public void Complete_RejectsSecondCompletion()
    {
        var movement = new VehicleMovement(1, 2, Departure, 100, null);

        movement.Complete(Departure.AddHours(1), 120);

        Assert.False(movement.IsOpen);
        Assert.Throws<DomainException>(() => movement.Complete(Departure.AddHours(2), 130));
    }

    [Fact]
    public void Update_RequiresArrivalAndFinalMileageTogether()
    {
        var movement = new VehicleMovement(1, 2, Departure, 100, null);

        Assert.Throws<DomainException>(() => movement.Update(1, 2, Departure, Departure.AddHours(1), 100, null, null));
        Assert.Throws<DomainException>(() => movement.Update(1, 2, Departure, null, 100, 120, null));
        Assert.True(movement.IsOpen);
        Assert.Null(movement.FinalMileage);
    }

    [Fact]
    public void Create_RejectsNonUtcTime()
    {
        var horarioLocal = DateTime.SpecifyKind(Departure, DateTimeKind.Local);

        Assert.Throws<DomainException>(() => new VehicleMovement(1, 2, horarioLocal, 100, null));
    }
}
