using QuemPegouOVeiculo.Api.Common;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Consultas;

namespace QuemPegouOVeiculo.Api.Modules.Consultas;

public sealed class ConsultaParametros
{
    public string? Busca { get; set; }
    public int? VeiculoId { get; set; }
    public int? MotoristaId { get; set; }
    public bool? Ativo { get; set; }
    public bool? EmAberto { get; set; }
    public DateOnly? DataDe { get; set; }
    public DateOnly? DataAte { get; set; }
    public DateTime? InicioUtc { get; set; }
    public DateTime? FimUtc { get; set; }
    public string? CampoData { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public FiltroConsulta Filtro => new(Busca, VeiculoId, MotoristaId, Ativo, EmAberto,
        DataDe, DataAte, InicioUtc, FimUtc, CampoData);
    public PageRequest Pagina => new(Page ?? 1, PageSize ?? 50);
}

public static class Endpoints
{
    public static void MapConsultas(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/consultas").WithTags("Consultas da frota");

        group.MapGet("/motoristas", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.MotoristasAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());
        group.MapGet("/veiculos", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.VeiculosAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());
        group.MapGet("/movimentacoes", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.MovimentacoesAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());
        group.MapGet("/abastecimentos", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.AbastecimentosAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());
        group.MapGet("/multas", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.MultasAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());
        group.MapGet("/manutencoes", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.ManutencoesAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());
        group.MapGet("/status-veiculo", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.StatusVeiculosAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());
        group.MapGet("/vencimentos-cnh", async ([AsParameters] ConsultaParametros p, ConsultasFrotaQueries consultas,
            CancellationToken ct) =>
            (await consultas.VencimentosCnhAsync(p.Filtro, p.Pagina, ct)).ToHttpResult());

        group.MapGet("/veiculos/{id:int:min(1)}/ultima-quilometragem",
            async (int id, string origem, ConsultasFrotaQueries consultas, CancellationToken ct) =>
                (await consultas.UltimaQuilometragemAsync(id, origem, ct)).ToHttpResult());
    }
}
