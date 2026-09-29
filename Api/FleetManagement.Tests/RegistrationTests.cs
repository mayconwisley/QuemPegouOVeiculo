using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Registrations;

namespace FleetManagement.Tests;

public sealed class RegistrationTests
{
    [Fact]
    public void Driver_NormalizesCpf()
    {
        var driver = new Driver("Ana", "12345678901", new DateOnly(2028, 1, 1),
            "B", "529.982.247-25", null, true);

        Assert.Equal("52998224725", driver.Cpf);
    }

    [Fact]
    public void Driver_RejectsInvalidCpf()
    {
        Assert.Throws<DomainException>(() => new Driver("Ana", "12345678901",
            new DateOnly(2028, 1, 1), "B", "11111111111", null, true));
    }

    [Fact]
    public void Vehicle_NormalizesPlate()
    {
        var vehicle = new Vehicle("abc-1d23", "Modelo", null, null, true);

        Assert.Equal("ABC1D23", vehicle.Plate);
    }
}
