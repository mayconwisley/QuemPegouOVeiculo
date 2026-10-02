using FleetManagement.Application.Modules.Operations.Reservations;
using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Tests;

public sealed class VehicleReservationTests
{
    [Fact]
    public void Reservation_RequiresUtcValidPeriodAndPurpose()
    {
        var start = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);

        Assert.Throws<DomainException>(() => new VehicleReservation(1, 2,
            start, start, "Visita"));
        Assert.Throws<DomainException>(() => new VehicleReservation(1, 2,
            DateTime.SpecifyKind(start, DateTimeKind.Local), start.AddHours(1), "Visita"));
        Assert.Throws<DomainException>(() => new VehicleReservation(1, 2,
            start, start.AddHours(1), " "));
    }

    [Fact]
    public void Reservation_StartAndCompleteFollowLifecycle()
    {
        var start = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);
        var reservation = new VehicleReservation(1, 2, start, start.AddHours(1), "Visita");

        Assert.Throws<DomainException>(() => reservation.Start(start.AddMinutes(-16)));
        reservation.Start(start.AddMinutes(-15));
        Assert.Equal(ReservationStatuses.InUse, reservation.Status);
        Assert.Throws<DomainException>(() => reservation.Cancel());
        reservation.Complete();
        Assert.Equal(ReservationStatuses.Completed, reservation.Status);
        Assert.Throws<DomainException>(() => reservation.Start(start));
    }

    [Fact]
    public void CancelledReservationCannotBeEditedOrStarted()
    {
        var start = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc);
        var reservation = new VehicleReservation(1, 2, start, start.AddHours(1), "Visita");

        reservation.Cancel();

        Assert.Equal(ReservationStatuses.Cancelled, reservation.Status);
        Assert.Throws<DomainException>(() => reservation.UpdateSchedule(2,
            start.AddHours(1), start.AddHours(2), "Outro destino"));
        Assert.Throws<DomainException>(() => reservation.Start(start));
    }

    [Fact]
    public void ReservationFilterRejectsLocalTimesAndInvalidStatus()
    {
        Assert.Throws<DomainException>(() => new ReservationFilter(Status: "Unknown").Validate());
        Assert.Throws<DomainException>(() => new ReservationFilter(
            FromUtc: DateTime.Now, ToUtc: DateTime.UtcNow.AddDays(1)).Validate());
    }
}
