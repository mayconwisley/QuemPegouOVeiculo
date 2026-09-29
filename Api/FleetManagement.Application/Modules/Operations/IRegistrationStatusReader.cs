namespace FleetManagement.Application.Modules.Operations;

public interface IRegistrationStatusReader
{
    Task<bool?> IsVehicleActiveAsync(int id, CancellationToken cancellationToken);
    Task<bool?> IsDriverActiveAsync(int id, CancellationToken cancellationToken);
}
