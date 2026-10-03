using FleetManagement.Application.Common;
using FleetManagement.Domain.Common;

namespace FleetManagement.Application.Modules.Queries;

public sealed record FleetQueryFilter(
    string? Search = null,
    Guid? VehicleId = null,
    Guid? DriverId = null,
    bool? Active = null,
    bool? IsOpen = null,
    DateOnly? FromDate = null,
    DateOnly? ToDate = null,
    DateTime? StartUtc = null,
    DateTime? EndUtc = null,
    string? DateField = null)
{
    public FleetQueryFilter Validate()
    {
        if (VehicleId == Guid.Empty || DriverId == Guid.Empty)
            throw new DomainException("Os identificadores dos filtros devem ser válidos.");
        if (FromDate > ToDate || StartUtc > EndUtc)
            throw new DomainException("O início do período deve ser anterior ao fim.");
        if (StartUtc.HasValue && StartUtc.Value.Kind != DateTimeKind.Utc
            || EndUtc.HasValue && EndUtc.Value.Kind != DateTimeKind.Utc)
            throw new DomainException("Os horários dos filtros devem estar em UTC.");
        if (DateField is not null and not ("departure" or "arrival" or "start" or "end"))
            throw new DomainException("Campo de data inválido.");
        return this;
    }
}

public sealed record DriverQuery(Guid Id, string Name, string LicenseNumber, DateOnly LicenseExpiration,
    string LicenseCategory, string Cpf, string Rg, bool Active);
public sealed record VehicleQuery(Guid Id, string Plate, string Model, string Chassis, string Renavam, bool Active);
public sealed record MovementQuery(Guid Id, Guid VehicleId, string Model, Guid DriverId, string Name,
    DateTime DepartureUtc, DateTime? ArrivalUtc, string Description, int InitialMileage, int? FinalMileage,
    DateTime? ExpectedReturnUtc, string Plate);
public sealed record RefuelingQuery(Guid Id, Guid VehicleId, string Model, Guid DriverId, string Name,
    int Mileage, DateOnly Date, decimal Amount, decimal Liters, string Description);
public sealed record FineQuery(Guid Id, Guid VehicleId, string Model, Guid DriverId, string Name,
    DateOnly Date, decimal Amount, int Points, string Description);
public sealed record MaintenanceQuery(Guid Id, Guid VehicleId, string Model, DateOnly Date,
    decimal Amount, string Description);
public sealed record VehicleStatusQuery(Guid Id, Guid VehicleId, string Model,
    DateTime StartUtc, DateTime? EndUtc, string Description);
public sealed record LicenseExpirationQuery(Guid Id, Guid DriverId, string Name, DateOnly Date, bool Expired);

public interface IFleetReadRepository
{
    Task<PagedResult<DriverQuery>> DriversAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<PagedResult<VehicleQuery>> VehiclesAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<PagedResult<MovementQuery>> MovementsAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<PagedResult<RefuelingQuery>> RefuelingsAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<PagedResult<FineQuery>> FinesAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<PagedResult<MaintenanceQuery>> MaintenanceAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<PagedResult<VehicleStatusQuery>> VehicleStatusesAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<PagedResult<LicenseExpirationQuery>> LicenseExpirationsAsync(FleetQueryFilter filter, PageRequest page, CancellationToken ct);
    Task<int?> LatestMileageAsync(Guid vehicleId, string source, CancellationToken ct);
}
