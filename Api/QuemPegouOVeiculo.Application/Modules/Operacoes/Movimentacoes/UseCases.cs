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
    public async Task<int> CreateAsync(MovimentacaoInput input, CancellationToken cancellationToken)
    {
        var movimentacao = new MovimentacaoVeiculo(input.VeiculoId, input.MotoristaId,
            input.SaidaUtc, input.KmInicial, input.Descricao);
        movimentacao.Atualizar(input.VeiculoId, input.MotoristaId, input.SaidaUtc,
            input.ChegadaUtc, input.KmInicial, input.KmFinal, input.Descricao);
        await EnsureReferencesActiveAsync(input.VeiculoId, input.MotoristaId, cancellationToken);

        await repository.AddAsync(movimentacao, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return movimentacao.Id;
    }

    public async Task UpdateAsync(int id, MovimentacaoInput input, CancellationToken cancellationToken)
    {
        var movimentacao = await repository.GetRequiredAsync(id, "Movimentação", cancellationToken);
        movimentacao.Atualizar(input.VeiculoId, input.MotoristaId, input.SaidaUtc,
            input.ChegadaUtc, input.KmInicial, input.KmFinal, input.Descricao);
        await EnsureReferencesActiveAsync(input.VeiculoId, input.MotoristaId, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task ConcludeAsync(int id, ConcluirMovimentacaoInput input, CancellationToken cancellationToken)
    {
        var movimentacao = await repository.GetRequiredAsync(id, "Movimentação", cancellationToken);
        movimentacao.Concluir(input.ChegadaUtc, input.KmFinal);
        await repository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        repository.Remove(await repository.GetRequiredAsync(id, "Movimentação", cancellationToken));
        await repository.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureReferencesActiveAsync(int veiculoId, int motoristaId, CancellationToken cancellationToken)
    {
        var veiculoAtivo = await cadastros.IsVeiculoActiveAsync(veiculoId, cancellationToken);
        var motoristaAtivo = await cadastros.IsMotoristaActiveAsync(motoristaId, cancellationToken);
        if (veiculoAtivo is null)
            throw new EntityNotFoundException("Veículo", veiculoId);
        if (motoristaAtivo is null)
            throw new EntityNotFoundException("Motorista", motoristaId);
        if (!veiculoAtivo.Value || !motoristaAtivo.Value)
            throw new BusinessConflictException("Veículo e motorista devem estar ativos para registrar movimentação.");
    }
}

public sealed class MovimentacaoQueries(IQueryRepository<MovimentacaoVeiculo> repository)
{
    private static readonly Expression<Func<MovimentacaoVeiculo, MovimentacaoView>> Projection = x =>
        new MovimentacaoView(x.Id, x.VeiculoId, x.MotoristaId, x.SaidaUtc, x.ChegadaUtc,
            x.KmInicial, x.KmFinal, x.Descricao, x.ChegadaUtc == null);

    public Task<MovimentacaoView?> GetAsync(int id, CancellationToken cancellationToken) =>
        repository.GetByIdAsync(id, Projection, cancellationToken);

    public Task<PagedResult<MovimentacaoView>> ListAsync(PageRequest page, CancellationToken cancellationToken) =>
        repository.ListAsync(page, Projection, cancellationToken);
}
