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
    public async Task<Result<int>> CreateAsync(MotoristaInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var motorista = new Motorista(input.Nome, input.Cnh, input.VencimentoCnh,
                input.CategoriaCnh, input.Cpf, input.Rg, input.Ativo);
            await repository.AddAsync(motorista, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return motorista.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, MotoristaInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var motorista = await repository.GetByIdAsync(id, cancellationToken);
            if (motorista is null)
                return Result.Failure(Error.NotFound("Motorista", id));
            motorista.Atualizar(input.Nome, input.Cnh, input.VencimentoCnh,
                input.CategoriaCnh, input.Cpf, input.Rg, input.Ativo);
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
                return Result.Failure(Error.NotFound("Motorista", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class MotoristaQueries(IQueryRepository<Motorista> repository)
{
    private static readonly Expression<Func<Motorista, MotoristaView>> Projection = x =>
        new MotoristaView(x.Id, x.Nome, x.Cnh, x.VencimentoCnh, x.CategoriaCnh, x.Cpf, x.Rg, x.Ativo);

    public async Task<Result<MotoristaView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<MotoristaView>.Failure(Error.NotFound("Motorista", id))
            : Result<MotoristaView>.Success(item);
    }

    public Task<Result<PagedResult<MotoristaView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
