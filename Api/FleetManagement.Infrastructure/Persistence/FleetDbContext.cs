using Microsoft.EntityFrameworkCore;
using FleetManagement.Domain.Modules.Registrations;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Infrastructure.Persistence;

public sealed class FleetDbContext(DbContextOptions<FleetDbContext> options) : DbContext(options)
{
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleMovement> Movements => Set<VehicleMovement>();
    public DbSet<Refueling> Refuelings => Set<Refueling>();
    public DbSet<Fine> Fines => Set<Fine>();
    public DbSet<Maintenance> Maintenance => Set<Maintenance>();
    public DbSet<VehicleStatus> VehicleStatuses => Set<VehicleStatus>();
    public DbSet<LicenseExpiration> LicenseExpirations => Set<LicenseExpiration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetDbContext).Assembly);
}
