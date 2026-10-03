using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Tests;

public sealed class OperationsPlanningTests
{
    [Fact]
    public void Movement_RejectsReturnBeforeDeparture()
    {
        var departure = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);
        var movement = new VehicleMovement(TestIds.Vehicle, TestIds.Driver, departure, 100, null);

        Assert.Throws<DomainException>(() => movement.ScheduleReturn(departure.AddMinutes(-1)));
    }

    [Fact]
    public void Checklist_RequiresValidPhaseAndUtcTime()
    {
        var now = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<DomainException>(() => new MovementChecklist(TestIds.Vehicle, "inspection",
            true, true, true, true, null, now));
        Assert.Throws<DomainException>(() => new MovementChecklist(TestIds.Vehicle, ChecklistPhases.Departure,
            true, true, true, true, null, DateTime.SpecifyKind(now, DateTimeKind.Local)));
    }

    [Fact]
    public void MaintenancePlan_AdvancesDateAndMileageWhenCompleted()
    {
        var plan = new MaintenancePlan(TestIds.Vehicle, "Troca de óleo", 180, 10000,
            new DateOnly(2026, 10, 1), 20000);
        var revision = plan.Revision;

        plan.Complete(new DateOnly(2026, 10, 2), 20500);

        Assert.Equal(new DateOnly(2027, 3, 31), plan.NextDueDate);
        Assert.Equal(30500, plan.NextDueMileage);
        Assert.True(plan.Revision > revision);
        Assert.Throws<DomainException>(() => plan.Complete(new DateOnly(2026, 10, 2), 20500));
    }

    [Fact]
    public void MaintenancePlan_RequiresMatchingIntervalsAndTargets()
    {
        Assert.Throws<DomainException>(() => new MaintenancePlan(TestIds.Vehicle, "Freios", 180, null,
            null, null));
        Assert.Throws<DomainException>(() => new MaintenancePlan(TestIds.Vehicle, "Freios", null, null,
            null, null));
    }

    [Fact]
    public void MaintenancePlan_RejectedUpdateKeepsPreviousState()
    {
        var plan = new MaintenancePlan(TestIds.Vehicle, "Freios", 180, null,
            new DateOnly(2027, 1, 1), null);

        Assert.Throws<DomainException>(() => plan.Update(TestIds.Vehicle, "Pneus", 0, null,
            new DateOnly(2027, 2, 1), null, true));

        Assert.Equal("Freios", plan.Name);
        Assert.Equal(180, plan.IntervalDays);
    }
}
