using System.Linq.Expressions;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Operacoes;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Application.Modules.Operacoes.Movimentacoes;

public sealed record MovimentacaoInput(int VeiculoId, int MotoristaId, DateTime SaidaUtc,
    DateTime? ChegadaUtc, int KmInicial, int? KmFinal, string? Descricao);

public sealed record ConcluirMovimentacaoInput(DateTime ChegadaUtc, int KmFinal);

public sealed record MovimentacaoView(int Id, int VeiculoId, int MotoristaId, DateTime SaidaUtc,
    DateTime? ChegadaUtc, int KmInicial, int? KmFinal, string Descricao, bool EmAberto);

public sealed class MovimentacaoCommands(
    ICommandRepository<MovimentacaoVeiculo> repository,
    ICadastrosStatusReader cadastros)
{
    public async Task<Result<int>> CreateAsync(MovimentacaoInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureValueAsync<int>(async () =>
        {
            var movimentacao = new MovimentacaoVeiculo(input.VeiculoId, input.MotoristaId,
                input.SaidaUtc, input.KmInicial, input.Descricao);
            movimentacao.Atualizar(input.VeiculoId, input.MotoristaId, input.SaidaUtc,
                input.ChegadaUtc, input.KmInicial, input.KmFinal, input.Descricao);
            var references = await EnsureReferencesActiveAsync(input.VeiculoId, input.MotoristaId, cancellationToken);
            if (!references.IsSuccess)
                return Result<int>.Failure(references.Error);

            await repository.AddAsync(movimentacao, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(movimentacao.Id);
        });
    }

    public async Task<Result> UpdateAsync(int id, MovimentacaoInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var movimentacao = await repository.GetByIdAsync(id, cancellationToken);
            if (movimentacao is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            movimentacao.Atualizar(input.VeiculoId, input.MotoristaId, input.SaidaUtc,
                input.ChegadaUtc, input.KmInicial, input.KmFinal, input.Descricao);
            var references = await EnsureReferencesActiveAsync(input.VeiculoId, input.MotoristaId, cancellationToken);
            if (!references.IsSuccess)
                return references;
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    public async Task<Result> ConcludeAsync(int id, ConcluirMovimentacaoInput input, CancellationToken cancellationToken)
    {
        return await Result.CaptureAsync(async () =>
        {
            var movimentacao = await repository.GetByIdAsync(id, cancellationToken);
            if (movimentacao is null)
                return Result.Failure(Error.NotFound("Movimentação", id));
            movimentacao.Concluir(input.ChegadaUtc, input.KmFinal);
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
                return Result.Failure(Error.NotFound("Movimentação", id));
            repository.Remove(item);
            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        });
    }

    private async Task<Result> EnsureReferencesActiveAsync(int veiculoId, int motoristaId,
        CancellationToken cancellationToken)
    {
        var veiculoAtivo = await cadastros.IsVeiculoActiveAsync(veiculoId, cancellationToken);
        var motoristaAtivo = await cadastros.IsMotoristaActiveAsync(motoristaId, cancellationToken);
        if (veiculoAtivo is null)
            return Result.Failure(Error.NotFound("Veículo", veiculoId));
        if (motoristaAtivo is null)
            return Result.Failure(Error.NotFound("Motorista", motoristaId));
        if (!veiculoAtivo.Value || !motoristaAtivo.Value)
            return Result.Failure(Error.Conflict(
                "Veículo e motorista devem estar ativos para registrar movimentação."));
        return Result.Success();
    }
}

public sealed class MovimentacaoQueries(IQueryRepository<MovimentacaoVeiculo> repository)
{
    private static readonly Expression<Func<MovimentacaoVeiculo, MovimentacaoView>> Projection = x =>
        new MovimentacaoView(x.Id, x.VeiculoId, x.MotoristaId, x.SaidaUtc, x.ChegadaUtc,
            x.KmInicial, x.KmFinal, x.Descricao, x.ChegadaUtc == null);

    public async Task<Result<MovimentacaoView>> GetAsync(int id, CancellationToken cancellationToken)
    {
        var item = await repository.GetByIdAsync(id, Projection, cancellationToken);
        return item is null
            ? Result<MovimentacaoView>.Failure(Error.NotFound("Movimentação", id))
            : Result<MovimentacaoView>.Success(item);
    }

    public Task<Result<PagedResult<MovimentacaoView>>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        Result.TryAsync(() => repository.ListAsync(page, Projection, cancellationToken));
}
