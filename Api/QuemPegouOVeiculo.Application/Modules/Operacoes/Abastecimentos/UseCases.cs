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
    public async Task<Result<int>> CreateAsync(AbastecimentoInput input, CancellationToken cancellationToken)
    {
        return await Result.TryAsync(async () =>
        {
            var item = new Abastecimento(input.VeiculoId, input.MotoristaId, input.Quilometragem,
                input.Data, input.Valor, input.Litros, input.Descricao);
            await repository.AddAsync(item, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return item.Id;
        });
    }

    public async Task<Result> UpdateAsync(int id, AbastecimentoInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var item = await repository.GetByIdAsync(id, cancellationToken);
            if (item is null)
                return Result.Failure(Error.NotFound("Abastecimento", id));
            item.Atualizar(input.VeiculoId, input.MotoristaId, input.Quilometragem,
                input.Data, input.Valor, input.Litros, input.Descricao);
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
                return Result.Failure(Error.NotFound("Abastecimento", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }
}

public sealed class AbastecimentoQueries(IQueryRepository<Abastecimento> repository)
{
    private static readonly Expression<Func<Abastecimento, AbastecimentoView>> Projection = x =>
        new AbastecimentoView(x.Id, x.VeiculoId, x.MotoristaId, x.Quilometragem,
            x.Data, x.Valor, x.Litros, x.Descricao);

    public async Task<Result<AbastecimentoView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<AbastecimentoView>.Failure(Error.NotFound("Abastecimento", id))
            : Result<AbastecimentoView>.Success(item);
    }

    public Task<Result<PagedResult<AbastecimentoView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
