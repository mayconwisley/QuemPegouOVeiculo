using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManagement.Domain.Modules.Registrations;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Infrastructure.Persistence.Configurations;

internal sealed class MovementConfiguration : IEntityTypeConfiguration<VehicleMovement>
{
    public void Configure(EntityTypeBuilder<VehicleMovement> builder)
    {
        builder.ToTable("vehicle_movements", "operations", table =>
        {
            table.HasCheckConstraint("ck_vehicle_movements_mileage", "initial_mileage >= 0 AND (final_mileage IS NULL OR final_mileage >= initial_mileage)");
            table.HasCheckConstraint("ck_vehicle_movements_arrival", "(arrival_utc IS NULL) = (final_mileage IS NULL) AND (arrival_utc IS NULL OR arrival_utc >= departure_utc)");
            table.HasCheckConstraint("ck_vehicle_movements_expected_return", "expected_return_utc IS NULL OR expected_return_utc >= departure_utc");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.DriverId).HasColumnName("driver_id");
        builder.Property(x => x.ReservationId).HasColumnName("reservation_id");
        builder.Property(x => x.DepartureUtc).HasColumnName("departure_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.ArrivalUtc).HasColumnName("arrival_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.ExpectedReturnUtc).HasColumnName("expected_return_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.InitialMileage).HasColumnName("initial_mileage");
        builder.Property(x => x.FinalMileage).HasColumnName("final_mileage");
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
        builder.Ignore(x => x.IsOpen);
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<VehicleReservation>().WithMany().HasForeignKey(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.VehicleId).IsUnique().HasFilter("arrival_utc IS NULL");
        builder.HasIndex(x => new { x.DriverId, x.DepartureUtc });
        builder.HasIndex(x => x.ReservationId).IsUnique().HasFilter("reservation_id IS NOT NULL");
        builder.HasIndex(x => x.ExpectedReturnUtc).HasFilter("arrival_utc IS NULL AND expected_return_utc IS NOT NULL");
    }
}

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
{
    public void Configure(EntityTypeBuilder<VehicleReservation> builder)
    {
        builder.ToTable("vehicle_reservations", "operations", table =>
        {
            table.HasCheckConstraint("ck_vehicle_reservations_dates", "end_utc > start_utc");
            table.HasCheckConstraint("ck_vehicle_reservations_status",
                "status IN ('Confirmed', 'InUse', 'Completed', 'Cancelled')");
            table.HasCheckConstraint("ck_vehicle_reservations_revision", "revision > 0");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.DriverId).HasColumnName("driver_id");
        builder.Property(x => x.StartUtc).HasColumnName("start_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.EndUtc).HasColumnName("end_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.Purpose).HasColumnName("purpose").HasMaxLength(500).IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(x => x.Revision).HasColumnName("revision").IsConcurrencyToken();
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VehicleId, x.StartUtc });
        builder.HasIndex(x => new { x.DriverId, x.StartUtc });
    }
}

internal sealed class MovementChecklistConfiguration : IEntityTypeConfiguration<MovementChecklist>
{
    public void Configure(EntityTypeBuilder<MovementChecklist> builder)
    {
        builder.ToTable("movement_checklists", "operations", table =>
            table.HasCheckConstraint("ck_movement_checklists_phase", "phase IN ('departure', 'arrival')"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.MovementId).HasColumnName("movement_id");
        builder.Property(x => x.Phase).HasColumnName("phase").HasMaxLength(12).IsRequired();
        builder.Property(x => x.TiresOk).HasColumnName("tires_ok");
        builder.Property(x => x.LightsOk).HasColumnName("lights_ok");
        builder.Property(x => x.FluidsOk).HasColumnName("fluids_ok");
        builder.Property(x => x.BodyOk).HasColumnName("body_ok");
        builder.Property(x => x.Notes).HasColumnName("notes").HasMaxLength(2000).IsRequired();
        builder.Property(x => x.CheckedAtUtc).HasColumnName("checked_at_utc").HasColumnType("timestamp with time zone");
        builder.HasOne<VehicleMovement>().WithMany().HasForeignKey(x => x.MovementId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.MovementId, x.Phase }).IsUnique();
    }
}

internal sealed class RefuelingConfiguration : IEntityTypeConfiguration<Refueling>
{
    public void Configure(EntityTypeBuilder<Refueling> builder)
    {
        builder.ToTable("refuelings", "operations", table =>
            table.HasCheckConstraint("ck_refuelings_values", "mileage >= 0 AND amount >= 0 AND liters > 0"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.DriverId).HasColumnName("driver_id");
        builder.Property(x => x.Mileage).HasColumnName("mileage");
        builder.Property(x => x.Date).HasColumnName("date").HasColumnType("date");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 2);
        builder.Property(x => x.Liters).HasColumnName("liters").HasPrecision(18, 3);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VehicleId, x.Date });
    }
}

internal sealed class FineConfiguration : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder.ToTable("fines", "operations", table =>
            table.HasCheckConstraint("ck_fines_values", "amount >= 0 AND points >= 0"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.DriverId).HasColumnName("driver_id");
        builder.Property(x => x.Date).HasColumnName("date").HasColumnType("date");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 2);
        builder.Property(x => x.Points).HasColumnName("points");
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VehicleId, x.Date });
    }
}

internal sealed class MaintenanceConfiguration : IEntityTypeConfiguration<Maintenance>
{
    public void Configure(EntityTypeBuilder<Maintenance> builder)
    {
        builder.ToTable("maintenance_records", "operations", table =>
            table.HasCheckConstraint("ck_maintenance_records_amount", "amount >= 0"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.PlanId).HasColumnName("plan_id");
        builder.Property(x => x.Date).HasColumnName("date").HasColumnType("date");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 2);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<MaintenancePlan>().WithMany().HasForeignKey(x => x.PlanId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VehicleId, x.Date });
    }
}

internal sealed class MaintenancePlanConfiguration : IEntityTypeConfiguration<MaintenancePlan>
{
    public void Configure(EntityTypeBuilder<MaintenancePlan> builder)
    {
        builder.ToTable("maintenance_plans", "operations", table =>
        {
            table.HasCheckConstraint("ck_maintenance_plans_interval", "((interval_days IS NULL AND next_due_date IS NULL) OR (interval_days > 0 AND next_due_date IS NOT NULL)) AND ((interval_mileage IS NULL AND next_due_mileage IS NULL) OR (interval_mileage > 0 AND next_due_mileage >= 0)) AND (interval_days IS NOT NULL OR interval_mileage IS NOT NULL)");
            table.HasCheckConstraint("ck_maintenance_plans_revision", "revision > 0");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(120).IsRequired();
        builder.Property(x => x.IntervalDays).HasColumnName("interval_days");
        builder.Property(x => x.IntervalMileage).HasColumnName("interval_mileage");
        builder.Property(x => x.NextDueDate).HasColumnName("next_due_date").HasColumnType("date");
        builder.Property(x => x.NextDueMileage).HasColumnName("next_due_mileage");
        builder.Property(x => x.LastCompletedOn).HasColumnName("last_completed_on").HasColumnType("date");
        builder.Property(x => x.LastCompletedMileage).HasColumnName("last_completed_mileage");
        builder.Property(x => x.IsActive).HasColumnName("is_active");
        builder.Property(x => x.Revision).HasColumnName("revision").IsConcurrencyToken();
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VehicleId, x.IsActive });
        builder.HasIndex(x => x.NextDueDate).HasFilter("is_active = true AND next_due_date IS NOT NULL");
    }
}

internal sealed class VehicleStatusConfiguration : IEntityTypeConfiguration<VehicleStatus>
{
    public void Configure(EntityTypeBuilder<VehicleStatus> builder)
    {
        builder.ToTable("vehicle_statuses", "operations", table =>
            table.HasCheckConstraint("ck_vehicle_statuses_dates", "end_utc IS NULL OR end_utc >= start_utc"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.StartUtc).HasColumnName("start_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.EndUtc).HasColumnName("end_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VehicleId, x.StartUtc });
    }
}

internal sealed class LicenseExpirationConfiguration : IEntityTypeConfiguration<LicenseExpiration>
{
    public void Configure(EntityTypeBuilder<LicenseExpiration> builder)
    {
        builder.ToTable("license_expirations", "operations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.DriverId).HasColumnName("driver_id");
        builder.Property(x => x.Date).HasColumnName("date").HasColumnType("date");
        builder.Property(x => x.Expired).HasColumnName("expired");
        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.DriverId, x.Date });
    }
}
