namespace FleetManagement.Application.Modules.Operations;

public interface IRegistrationStatusReader
{
    Task<bool?> IsVehicleActiveAsync(Guid id, CancellationToken cancellationToken);
    Task<bool?> IsDriverActiveAsync(Guid id, CancellationToken cancellationToken);
}
