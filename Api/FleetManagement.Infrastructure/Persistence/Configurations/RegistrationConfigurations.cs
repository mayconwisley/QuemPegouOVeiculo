using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FleetManagement.Domain.Modules.Registrations;

namespace FleetManagement.Infrastructure.Persistence.Configurations;

internal sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("drivers", "registrations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
        builder.Property(x => x.LicenseNumber).HasColumnName("license_number").HasMaxLength(20).IsRequired();
        builder.Property(x => x.LicenseExpiration).HasColumnName("license_expiration").HasColumnType("date");
        builder.Property(x => x.LicenseCategory).HasColumnName("license_category").HasMaxLength(10).IsRequired();
        builder.Property(x => x.Cpf).HasColumnName("cpf").HasMaxLength(11).IsRequired();
        builder.Property(x => x.Rg).HasColumnName("rg").HasMaxLength(20).IsRequired();
        builder.Property(x => x.Active).HasColumnName("active");
        builder.HasIndex(x => x.Cpf).IsUnique();
    }
}

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles", "registrations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Plate).HasColumnName("plate").HasMaxLength(7).IsRequired();
        builder.Property(x => x.Model).HasColumnName("model").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Chassis).HasColumnName("chassis").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Renavam).HasColumnName("renavam").HasMaxLength(20).IsRequired();
        builder.Property(x => x.Active).HasColumnName("active");
        builder.HasIndex(x => x.Plate).IsUnique();
    }
}
