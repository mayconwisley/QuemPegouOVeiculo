using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Cadastros;

namespace QuemPegouOVeiculo.Application.Modules.Cadastros.Motoristas;

public sealed record MotoristaInput(string Nome, string Cnh, DateOnly VencimentoCnh,
    string CategoriaCnh, string Cpf, string? Rg, bool Ativo);

public sealed record MotoristaView(int Id, string Nome, string Cnh, DateOnly VencimentoCnh,
    string CategoriaCnh, string Cpf, string Rg, bool Ativo);

public sealed class MotoristaCommands(ICommandRepository<Motorista> repository)
{
    public async Task<int> CreateAsync(MotoristaInput input, CancellationToken cancellationToken)
    {
        var motorista = new Motorista(input.Nome, input.Cnh, input.VencimentoCnh,
            input.CategoriaCnh, input.Cpf, input.Rg, input.Ativo);
        await repository.AddAsync(motorista, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return motorista.Id;
    }

    public async Task UpdateAsync(int id, MotoristaInput input, CancellationToken cancellationToken)
    {
        var motorista = await repository.GetRequiredAsync(id, "Motorista", cancellationToken);
        motorista.Atualizar(input.Nome, input.Cnh, input.VencimentoCnh,
            input.CategoriaCnh, input.Cpf, input.Rg, input.Ativo);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Motorista", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }
}

public sealed class MotoristaQueries(IQueryRepository<Motorista> repository)
{
    private static readonly Expression<Func<Motorista, MotoristaView>> Projection = x =>
        new MotoristaView(x.Id, x.Nome, x.Cnh, x.VencimentoCnh, x.CategoriaCnh, x.Cpf, x.Rg, x.Ativo);

    public Task<MotoristaView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<MotoristaView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
