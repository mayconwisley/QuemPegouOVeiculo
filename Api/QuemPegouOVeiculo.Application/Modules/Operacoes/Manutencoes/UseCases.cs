using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.Manutencoes;

public sealed record ManutencaoInput(int VeiculoId, DateOnly Data, decimal Valor, string? Descricao);
public sealed record ManutencaoView(int Id, int VeiculoId, DateOnly Data, decimal Valor, string Descricao);

public sealed class ManutencaoCommands(ICommandRepository<Manutencao> repository)
{
    public async Task<int> CreateAsync(ManutencaoInput input, CancellationToken cancellationToken)
    {
        var item = new Manutencao(input.VeiculoId, input.Data, input.Valor, input.Descricao);
        await repository.AddAsync(item, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return item.Id;
    }

    public async Task UpdateAsync(int id, ManutencaoInput input, CancellationToken cancellationToken)
    {
        var item = await repository.GetRequiredAsync(id, "Manutenção", cancellationToken);
        item.Atualizar(input.VeiculoId, input.Data, input.Valor, input.Descricao);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Manutenção", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }
}

public sealed class ManutencaoQueries(IQueryRepository<Manutencao> repository)
{
    private static readonly Expression<Func<Manutencao, ManutencaoView>> Projection = x =>
        new ManutencaoView(x.Id, x.VeiculoId, x.Data, x.Valor, x.Descricao);

    public Task<ManutencaoView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<ManutencaoView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
