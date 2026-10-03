using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Registrations;

namespace FleetManagement.Application.Modules.Registrations.Drivers;

public sealed record DriverInput(string Name, string LicenseNumber, DateOnly LicenseExpiration,
    string LicenseCategory, string Cpf, string? Rg, bool Active);

public sealed record DriverView(Guid Id, string Name, string LicenseNumber, DateOnly LicenseExpiration,
    string LicenseCategory, string Cpf, string Rg, bool Active);

public sealed class DriverCommands(ICommandRepository<Driver> repository)
{
    public async Task<Result<Guid>> CreateAsync(DriverInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var driver = new Driver(input.Name, input.LicenseNumber, input.LicenseExpiration,
                input.LicenseCategory, input.Cpf, input.Rg, input.Active);
            await repository.AddAsync(driver, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return driver.Id;
        });
    }

    public async Task<Result> UpdateAsync(Guid id, DriverInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var driver = await repository.GetByIdAsync(id, cancellationToken);
            if (driver is null)
                return Result.Failure(Error.NotFound("Motorista", id));
            driver.Update(input.Name, input.LicenseNumber, input.LicenseExpiration,
                input.LicenseCategory, input.Cpf, input.Rg, input.Active);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Motorista", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class DriverQueries(IQueryRepository<Driver> repository)
{
    private static readonly Expression<Func<Driver, DriverView>> Projection = x =>
        new DriverView(x.Id, x.Name, x.LicenseNumber, x.LicenseExpiration, x.LicenseCategory, x.Cpf, x.Rg, x.Active);

    public async Task<Result<DriverView>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<DriverView>.Failure(Error.NotFound("Motorista", id))
            : Result<DriverView>.Success(item);
    }

    public Task<Result<PagedResult<DriverView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
