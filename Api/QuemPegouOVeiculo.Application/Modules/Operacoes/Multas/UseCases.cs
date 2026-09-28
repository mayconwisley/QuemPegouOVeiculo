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
    public async Task<Result<int>> CreateAsync(MultaInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new Multa(input.VeiculoId, input.MotoristaId, input.Data,
                input.Valor, input.Pontos, input.Descricao);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, MultaInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Multa", id));
            item.Atualizar(input.VeiculoId, input.MotoristaId, input.Data,
                input.Valor, input.Pontos, input.Descricao);
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
                return Result.Failure(Error.NotFound("Multa", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class MultaQueries(IQueryRepository<Multa> repository)
{
    private static readonly Expression<Func<Multa, MultaView>> Projection = x =>
        new MultaView(x.Id, x.VeiculoId, x.MotoristaId, x.Data, x.Valor, x.Pontos, x.Descricao);

    public async Task<Result<MultaView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<MultaView>.Failure(Error.NotFound("Multa", id))
            : Result<MultaView>.Success(item);
    }

    public Task<Result<PagedResult<MultaView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
