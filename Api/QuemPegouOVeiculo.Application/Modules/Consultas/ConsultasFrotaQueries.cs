using QuemPegouOVeiculo.Application.Common;

namespace QuemPegouOVeiculo.Application.Modules.Consultas;

public sealed record UltimaQuilometragemView(int? Quilometragem);

public sealed class ConsultasFrotaQueries(IConsultasFrota consultas)
{
    public Task<Result<PagedResult<MotoristaConsulta>>> MotoristasAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.MotoristasAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<PagedResult<VeiculoConsulta>>> VeiculosAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.VeiculosAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<PagedResult<MovimentacaoConsulta>>> MovimentacoesAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.MovimentacoesAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<PagedResult<AbastecimentoConsulta>>> AbastecimentosAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.AbastecimentosAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<PagedResult<MultaConsulta>>> MultasAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.MultasAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<PagedResult<ManutencaoConsulta>>> ManutencoesAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.ManutencoesAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<PagedResult<StatusVeiculoConsulta>>> StatusVeiculosAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.StatusVeiculosAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<PagedResult<VencimentoCnhConsulta>>> VencimentosCnhAsync(
        FiltroConsulta filtro, PageRequest pagina, CancellationToken cancellationToken) =>
        Result.TryAsync(() => consultas.VencimentosCnhAsync(filtro.Validar(), pagina, cancellationToken));

    public Task<Result<UltimaQuilometragemView>> UltimaQuilometragemAsync(
        int veiculoId, string origem, CancellationToken cancellationToken) =>
        Result.TryAsync(async () => new UltimaQuilometragemView(
            await consultas.UltimaQuilometragemAsync(veiculoId, origem, cancellationToken)));
}
