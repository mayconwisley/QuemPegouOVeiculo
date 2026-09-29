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
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VehicleId).HasColumnName("vehicle_id");
        builder.Property(x => x.DriverId).HasColumnName("driver_id");
        builder.Property(x => x.DepartureUtc).HasColumnName("departure_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.ArrivalUtc).HasColumnName("arrival_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.InitialMileage).HasColumnName("initial_mileage");
        builder.Property(x => x.FinalMileage).HasColumnName("final_mileage");
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
        builder.Ignore(x => x.IsOpen);
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Driver>().WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.VehicleId).IsUnique().HasFilter("arrival_utc IS NULL");
        builder.HasIndex(x => new { x.DriverId, x.DepartureUtc });
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
        builder.Property(x => x.Date).HasColumnName("date").HasColumnType("date");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 2);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(2000).IsRequired();
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VehicleId, x.Date });
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
