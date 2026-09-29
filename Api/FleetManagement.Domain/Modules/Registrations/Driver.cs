using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Registrations;

public sealed class Driver : IEntity
{
    private Driver() { }

    public Driver(string name, string licenseNumber, DateOnly licenseExpiration, string licenseCategory, string cpf, string? rg, bool active)
    {
        Update(name, licenseNumber, licenseExpiration, licenseCategory, cpf, rg, active);
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = "";
    public string LicenseNumber { get; private set; } = "";
    public DateOnly LicenseExpiration { get; private set; }
    public string LicenseCategory { get; private set; } = "";
    public string Cpf { get; private set; } = "";
    public string Rg { get; private set; } = "";
    public bool Active { get; private set; }

    public void Update(string name, string licenseNumber, DateOnly licenseExpiration, string licenseCategory, string cpf, string? rg, bool active)
    {
        var validatedName = Guard.Required(name, "Nome", 150);
        var validatedLicenseNumber = Guard.Required(licenseNumber, "CNH", 20);
        var validatedLicenseExpiration = Guard.Date(licenseExpiration, "Vencimento da CNH");
        var validatedLicenseCategory = Guard.Required(licenseCategory, "Categoria da CNH", 10);
        var validatedCpf = Guard.Cpf(cpf);
        var validatedRg = Guard.Optional(rg, "RG", 20);

        Name = validatedName;
        LicenseNumber = validatedLicenseNumber;
        LicenseExpiration = validatedLicenseExpiration;
        LicenseCategory = validatedLicenseCategory;
        Cpf = validatedCpf;
        Rg = validatedRg;
        Active = active;
    }
}
