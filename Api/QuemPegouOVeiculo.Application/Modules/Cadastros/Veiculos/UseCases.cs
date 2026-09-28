using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Cadastros;

namespace QuemPegouOVeiculo.Application.Modules.Cadastros.Veiculos;

public sealed record VeiculoInput(string Placa, string Modelo, string? Chassi, string? Renavam, bool Ativo);
public sealed record VeiculoView(int Id, string Placa, string Modelo, string Chassi, string Renavam, bool Ativo);

public sealed class VeiculoCommands(ICommandRepository<Veiculo> repository)
{
    public async Task<int> CreateAsync(VeiculoInput input, CancellationToken cancellationToken)
    {
        var veiculo = new Veiculo(input.Placa, input.Modelo, input.Chassi, input.Renavam, input.Ativo);
        await repository.AddAsync(veiculo, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return veiculo.Id;
    }

    public async Task UpdateAsync(int id, VeiculoInput input, CancellationToken cancellationToken)
    {
        var veiculo = await repository.GetRequiredAsync(id, "Veículo", cancellationToken);
        veiculo.Atualizar(input.Placa, input.Modelo, input.Chassi, input.Renavam, input.Ativo);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Veículo", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }
}

public sealed class VeiculoQueries(IQueryRepository<Veiculo> repository)
{
    private static readonly Expression<Func<Veiculo, VeiculoView>> Projection = x =>
        new VeiculoView(x.Id, x.Placa, x.Modelo, x.Chassi, x.Renavam, x.Ativo);

    public Task<VeiculoView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<VeiculoView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
