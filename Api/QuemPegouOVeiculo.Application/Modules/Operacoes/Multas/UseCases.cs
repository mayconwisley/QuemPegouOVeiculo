using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.Multas;

public sealed record MultaInput(int VeiculoId, int MotoristaId, DateOnly Data,
    decimal Valor, int Pontos, string? Descricao);

public sealed record MultaView(int Id, int VeiculoId, int MotoristaId, DateOnly Data,
    decimal Valor, int Pontos, string Descricao);

public sealed class MultaCommands(ICommandRepository<Multa> repository)
{
    public async Task<int> CreateAsync(MultaInput input, CancellationToken cancellationToken)
    {
        var item = new Multa(input.VeiculoId, input.MotoristaId, input.Data,
            input.Valor, input.Pontos, input.Descricao);
        await repository.AddAsync(item, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return item.Id;
    }

    public async Task UpdateAsync(int id, MultaInput input, CancellationToken cancellationToken)
    {
        var item = await repository.GetRequiredAsync(id, "Multa", cancellationToken);
        item.Atualizar(input.VeiculoId, input.MotoristaId, input.Data,
            input.Valor, input.Pontos, input.Descricao);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Multa", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }
}

public sealed class MultaQueries(IQueryRepository<Multa> repository)
{
    private static readonly Expression<Func<Multa, MultaView>> Projection = x =>
        new MultaView(x.Id, x.VeiculoId, x.MotoristaId, x.Data, x.Valor, x.Pontos, x.Descricao);

    public Task<MultaView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<MultaView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
