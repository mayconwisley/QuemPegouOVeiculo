using FleetManagement.Domain.Modules.Operations;
using FleetManagement.Domain.Modules.Registrations;
using FleetManagement.Domain.Modules.Access;
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

    [Fact]
    public void AllPersistedEntitiesUseApplicationGeneratedUuidKeysAndUuidReferences()
    {
        using var context = CreateContext();

        foreach (var entity in context.Model.GetEntityTypes())
        {
            var key = Assert.Single(entity.FindPrimaryKey()!.Properties);
            Assert.Equal(typeof(Guid), key.ClrType);
            Assert.Equal(ValueGenerated.Never, key.ValueGenerated);

            foreach (var property in entity.GetProperties().Where(p => p.Name.EndsWith("Id", StringComparison.Ordinal)))
            {
                var actualType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
                Assert.Equal(typeof(Guid), actualType);
            }
        }
    }

    [Fact]
    public void NewEntitiesHaveVersionSevenIdsBeforePersistence()
    {
        var vehicle = new Vehicle("ABC1D23", "Sedan", null, null, true);
        var user = new UserAccount("admin", "hash", UserRoles.Administrator);

        Assert.Equal('7', vehicle.Id.ToString("D")[14]);
        Assert.Equal('7', user.Id.ToString("D")[14]);
        Assert.NotEqual(Guid.Empty, vehicle.Id);
        Assert.NotEqual(Guid.Empty, user.Id);
    }

    private static FleetDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<FleetDbContext>()
            .UseNpgsql("Host=localhost;Database=metadata_only;Username=unused;Password=unused")
            .Options;

        return new FleetDbContext(options);
    }
}
