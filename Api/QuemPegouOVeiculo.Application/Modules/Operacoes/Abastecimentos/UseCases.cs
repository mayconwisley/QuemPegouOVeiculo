using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.Abastecimentos;

public sealed record AbastecimentoInput(int VeiculoId, int MotoristaId, int Quilometragem,
    DateOnly Data, decimal Valor, decimal Litros, string? Descricao);

public sealed record AbastecimentoView(int Id, int VeiculoId, int MotoristaId, int Quilometragem,
    DateOnly Data, decimal Valor, decimal Litros, string Descricao);

public sealed class AbastecimentoCommands(ICommandRepository<Abastecimento> repository)
{
    public async Task<int> CreateAsync(AbastecimentoInput input, CancellationToken cancellationToken)
    {
        var item = new Abastecimento(input.VeiculoId, input.MotoristaId, input.Quilometragem,
            input.Data, input.Valor, input.Litros, input.Descricao);
        await repository.AddAsync(item, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return item.Id;
    }

    public async Task UpdateAsync(int id, AbastecimentoInput input, CancellationToken cancellationToken)
    {
        var item = await repository.GetRequiredAsync(id, "Abastecimento", cancellationToken);
        item.Atualizar(input.VeiculoId, input.MotoristaId, input.Quilometragem,
            input.Data, input.Valor, input.Litros, input.Descricao);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Abastecimento", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }
}

public sealed class AbastecimentoQueries(IQueryRepository<Abastecimento> repository)
{
    private static readonly Expression<Func<Abastecimento, AbastecimentoView>> Projection = x =>
        new AbastecimentoView(x.Id, x.VeiculoId, x.MotoristaId, x.Quilometragem,
            x.Data, x.Valor, x.Litros, x.Descricao);

    public Task<AbastecimentoView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<AbastecimentoView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
