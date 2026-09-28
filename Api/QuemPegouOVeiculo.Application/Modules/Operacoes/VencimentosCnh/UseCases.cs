using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.VencimentosCnh;

public sealed record VencimentoCnhInput(int MotoristaId, DateOnly Data, bool Vencido);
public sealed record VencimentoCnhView(int Id, int MotoristaId, DateOnly Data, bool Vencido);

public sealed class VencimentoCnhCommands(ICommandRepository<VencimentoCnh> repository)
{
    public async Task<Result<int>> CreateAsync(VencimentoCnhInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new VencimentoCnh(input.MotoristaId, input.Data, input.Vencido);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, VencimentoCnhInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Vencimento de CNH", id));
            item.Atualizar(input.MotoristaId, input.Data, input.Vencido);
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

public sealed class VencimentoCnhQueries(IQueryRepository<VencimentoCnh> repository)
{
    private static readonly Expression<Func<VencimentoCnh, VencimentoCnhView>> Projection = x =>
        new VencimentoCnhView(x.Id, x.MotoristaId, x.Data, x.Vencido);

    public async Task<Result<VencimentoCnhView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<VencimentoCnhView>.Failure(Error.NotFound("Vencimento de CNH", id))
            : Result<VencimentoCnhView>.Success(item);
    }

    public Task<Result<PagedResult<VencimentoCnhView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
