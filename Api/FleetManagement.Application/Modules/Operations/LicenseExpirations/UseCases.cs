using System.Linq.Expressions;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Application.Modules.Operations.LicenseExpirations;

public sealed record LicenseExpirationInput(int DriverId, DateOnly Date, bool Expired);
public sealed record LicenseExpirationView(int Id, int DriverId, DateOnly Date, bool Expired);

public sealed class LicenseExpirationCommands(ICommandRepository<LicenseExpiration> repository)
{
    public async Task<Result<int>> CreateAsync(LicenseExpirationInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new LicenseExpiration(input.DriverId, input.Date, input.Expired);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, LicenseExpirationInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Vencimento de CNH", id));
            item.Update(input.DriverId, input.Date, input.Expired);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Vencimento de CNH", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class LicenseExpirationQueries(IQueryRepository<LicenseExpiration> repository)
{
    private static readonly Expression<Func<LicenseExpiration, LicenseExpirationView>> Projection = x =>
        new LicenseExpirationView(x.Id, x.DriverId, x.Date, x.Expired);

    public async Task<Result<LicenseExpirationView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<LicenseExpirationView>.Failure(Error.NotFound("Vencimento de CNH", id))
            : Result<LicenseExpirationView>.Success(item);
    }

    public Task<Result<PagedResult<LicenseExpirationView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
