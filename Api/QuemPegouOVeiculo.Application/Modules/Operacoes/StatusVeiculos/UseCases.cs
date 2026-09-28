using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.StatusVeiculos;

public sealed record StatusVeiculoInput(int VeiculoId, DateTime InicioUtc, DateTime? FimUtc, string Descricao);
public sealed record StatusVeiculoView(int Id, int VeiculoId, DateTime InicioUtc, DateTime? FimUtc, string Descricao);

public sealed class StatusVeiculoCommands(ICommandRepository<StatusVeiculo> repository)
{
    public async Task<int> CreateAsync(StatusVeiculoInput input, CancellationToken cancellationToken)
    {
        var item = new StatusVeiculo(input.VeiculoId, input.InicioUtc, input.FimUtc, input.Descricao);
        await repository.AddAsync(item, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return item.Id;
    }

    public async Task UpdateAsync(int id, StatusVeiculoInput input, CancellationToken cancellationToken)
    {
        var item = await repository.GetRequiredAsync(id, "Status do veículo", cancellationToken);
        item.Atualizar(input.VeiculoId, input.InicioUtc, input.FimUtc, input.Descricao);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Status do veículo", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }
}

public sealed class StatusVeiculoQueries(IQueryRepository<StatusVeiculo> repository)
{
    private static readonly Expression<Func<StatusVeiculo, StatusVeiculoView>> Projection = x =>
        new StatusVeiculoView(x.Id, x.VeiculoId, x.InicioUtc, x.FimUtc, x.Descricao);

    public Task<StatusVeiculoView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<StatusVeiculoView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
