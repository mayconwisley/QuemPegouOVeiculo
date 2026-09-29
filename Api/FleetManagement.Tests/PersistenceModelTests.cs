using FleetManagement.Domain.Modules.Operations;
using FleetManagement.Domain.Modules.Registrations;
using FleetManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FleetManagement.Tests;

public sealed class PersistenceModelTests
{
    [Theory]
    [InlineData(typeof(Driver), "registrations", "drivers")]
    [InlineData(typeof(Vehicle), "registrations", "vehicles")]
    [InlineData(typeof(VehicleMovement), "operations", "vehicle_movements")]
    [InlineData(typeof(Refueling), "operations", "refuelings")]
    [InlineData(typeof(Fine), "operations", "fines")]
    [InlineData(typeof(Maintenance), "operations", "maintenance_records")]
    [InlineData(typeof(VehicleStatus), "operations", "vehicle_statuses")]
    [InlineData(typeof(LicenseExpiration), "operations", "license_expirations")]
    public void UsesEnglishSchemaAndTableNames(Type entityType, string schema, string table)
    {
        using var context = CreateContext();

        var mapping = context.Model.FindEntityType(entityType);

        Assert.NotNull(mapping);
        Assert.Equal(schema, mapping.GetSchema());
        Assert.Equal(table, mapping.GetTableName());
    }

    [Fact]
    public void MapsLicenseNumberToEnglishColumn()
    {
        using var context = CreateContext();
        var mapping = context.Model.FindEntityType(typeof(Driver));

        Assert.NotNull(mapping);
        var property = mapping.FindProperty(nameof(Driver.LicenseNumber));

        Assert.NotNull(property);
        Assert.Equal("license_number", property.GetColumnName(StoreObjectIdentifier.Table("drivers", "registrations")));
    }

    private static FleetDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<FleetDbContext>()
            .UseNpgsql("Host=localhost;Database=metadata_only;Username=unused;Password=unused")
            .Options;

        return new FleetDbContext(options);
    }
}
