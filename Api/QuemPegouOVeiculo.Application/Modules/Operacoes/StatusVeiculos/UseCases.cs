using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.StatusVeiculos;

public sealed record StatusVeiculoInput(int VeiculoId, DateTime InicioUtc, DateTime? FimUtc, string Descricao);
public sealed record StatusVeiculoView(int Id, int VeiculoId, DateTime InicioUtc, DateTime? FimUtc, string Descricao);

public sealed class StatusVeiculoCommands(ICommandRepository<StatusVeiculo> repository)
{
    public async Task<Result<int>> CreateAsync(StatusVeiculoInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new StatusVeiculo(input.VeiculoId, input.InicioUtc, input.FimUtc, input.Descricao);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, StatusVeiculoInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Status do veículo", id));
            item.Atualizar(input.VeiculoId, input.InicioUtc, input.FimUtc, input.Descricao);
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
                return Result.Failure(Error.NotFound("Status do veículo", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class StatusVeiculoQueries(IQueryRepository<StatusVeiculo> repository)
{
    private static readonly Expression<Func<StatusVeiculo, StatusVeiculoView>> Projection = x =>
        new StatusVeiculoView(x.Id, x.VeiculoId, x.InicioUtc, x.FimUtc, x.Descricao);

    public async Task<Result<StatusVeiculoView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<StatusVeiculoView>.Failure(Error.NotFound("Status do veículo", id))
            : Result<StatusVeiculoView>.Success(item);
    }

    public Task<Result<PagedResult<StatusVeiculoView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
