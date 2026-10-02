using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Tests;

public sealed class OperationsPlanningTests
{
    [Fact]
    public void Movement_RejectsReturnBeforeDeparture()
    {
        var departure = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);
        var movement = new VehicleMovement(1, 2, departure, 100, null);

        Assert.Throws<DomainException>(() => movement.ScheduleReturn(departure.AddMinutes(-1)));
    }

    [Fact]
    public void Checklist_RequiresValidPhaseAndUtcTime()
    {
        var now = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<DomainException>(() => new MovementChecklist(1, "inspection",
            true, true, true, true, null, now));
        Assert.Throws<DomainException>(() => new MovementChecklist(1, ChecklistPhases.Departure,
            true, true, true, true, null, DateTime.SpecifyKind(now, DateTimeKind.Local)));
    }

    [Fact]
    public void MaintenancePlan_AdvancesDateAndMileageWhenCompleted()
    {
        var plan = new MaintenancePlan(1, "Troca de óleo", 180, 10000,
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
        Assert.Throws<DomainException>(() => new MaintenancePlan(1, "Freios", 180, null,
            null, null));
        Assert.Throws<DomainException>(() => new MaintenancePlan(1, "Freios", null, null,
            null, null));
    }

    [Fact]
    public void MaintenancePlan_RejectedUpdateKeepsPreviousState()
    {
        var plan = new MaintenancePlan(1, "Freios", 180, null,
            new DateOnly(2027, 1, 1), null);

        Assert.Throws<DomainException>(() => plan.Update(1, "Pneus", 0, null,
            new DateOnly(2027, 2, 1), null, true));

        Assert.Equal("Freios", plan.Name);
        Assert.Equal(180, plan.IntervalDays);
    }
}
