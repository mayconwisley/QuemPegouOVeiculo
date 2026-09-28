using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.VencimentosCnh;

public sealed record VencimentoCnhInput(int MotoristaId, DateOnly Data, bool Vencido);
public sealed record VencimentoCnhView(int Id, int MotoristaId, DateOnly Data, bool Vencido);

public sealed class VencimentoCnhCommands(ICommandRepository<VencimentoCnh> repository)
{
    public async Task<int> CreateAsync(VencimentoCnhInput input, CancellationToken cancellationToken)
    {
        var item = new VencimentoCnh(input.MotoristaId, input.Data, input.Vencido);
        await repository.AddAsync(item, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return item.Id;
    }

    public async Task UpdateAsync(int id, VencimentoCnhInput input, CancellationToken cancellationToken)
    {
        var item = await repository.GetRequiredAsync(id, "Vencimento de CNH", cancellationToken);
        item.Atualizar(input.MotoristaId, input.Data, input.Vencido);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Vencimento de CNH", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }
}

public sealed class VencimentoCnhQueries(IQueryRepository<VencimentoCnh> repository)
{
    private static readonly Expression<Func<VencimentoCnh, VencimentoCnhView>> Projection = x =>
        new VencimentoCnhView(x.Id, x.MotoristaId, x.Data, x.Vencido);

    public Task<VencimentoCnhView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<VencimentoCnhView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
