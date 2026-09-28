using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.Manutencoes;

public sealed record ManutencaoInput(int VeiculoId, DateOnly Data, decimal Valor, string? Descricao);
public sealed record ManutencaoView(int Id, int VeiculoId, DateOnly Data, decimal Valor, string Descricao);

public sealed class ManutencaoCommands(ICommandRepository<Manutencao> repository)
{
    public async Task<Result<int>> CreateAsync(ManutencaoInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new Manutencao(input.VeiculoId, input.Data, input.Valor, input.Descricao);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, ManutencaoInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Manutenção", id));
            item.Atualizar(input.VeiculoId, input.Data, input.Valor, input.Descricao);
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
                return Result.Failure(Error.NotFound("Manutenção", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class ManutencaoQueries(IQueryRepository<Manutencao> repository)
{
    private static readonly Expression<Func<Manutencao, ManutencaoView>> Projection = x =>
        new ManutencaoView(x.Id, x.VeiculoId, x.Data, x.Valor, x.Descricao);

    public async Task<Result<ManutencaoView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<ManutencaoView>.Failure(Error.NotFound("Manutenção", id))
            : Result<ManutencaoView>.Success(item);
    }

    public Task<Result<PagedResult<ManutencaoView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
