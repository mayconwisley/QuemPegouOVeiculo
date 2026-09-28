using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Application.Modules.Consultas;

public sealed record FiltroConsulta(
    string? Busca = null,
    int? VeiculoId = null,
    int? MotoristaId = null,
    bool? Ativo = null,
    bool? EmAberto = null,
    DateOnly? DataDe = null,
    DateOnly? DataAte = null,
    DateTime? InicioUtc = null,
    DateTime? FimUtc = null,
    string? CampoData = null)
{
    public FiltroConsulta Validar()
    {
        if (VeiculoId is <= 0 || MotoristaId is <= 0)
            throw new DomainException("Os identificadores dos filtros devem ser positivos.");
        if (DataDe > DataAte || InicioUtc > FimUtc)
            throw new DomainException("O início do período deve ser anterior ao fim.");
        if (InicioUtc.HasValue && InicioUtc.Value.Kind != DateTimeKind.Utc
            || FimUtc.HasValue && FimUtc.Value.Kind != DateTimeKind.Utc)
            throw new DomainException("Os horários dos filtros devem estar em UTC.");
        if (CampoData is not null and not ("saida" or "chegada" or "inicio" or "fim"))
            throw new DomainException("CampoData inválido.");
        return this;
    }
}

public sealed record MotoristaConsulta(int Id, string Nome, string Cnh, DateOnly VencimentoCnh,
    string CategoriaCnh, string Cpf, string Rg, bool Ativo);
public sealed record VeiculoConsulta(int Id, string Placa, string Modelo, string Chassi, string Renavam, bool Ativo);
public sealed record MovimentacaoConsulta(int Id, int VeiculoId, string Modelo, int MotoristaId, string Nome,
    DateTime SaidaUtc, DateTime? ChegadaUtc, string Descricao, int KmInicial, int? KmFinal);
public sealed record AbastecimentoConsulta(int Id, int VeiculoId, string Modelo, int MotoristaId, string Nome,
    int Quilometragem, DateOnly Data, decimal Valor, decimal Litros, string Descricao);
public sealed record MultaConsulta(int Id, int VeiculoId, string Modelo, int MotoristaId, string Nome,
    DateOnly Data, decimal Valor, int Pontos, string Descricao);
public sealed record ManutencaoConsulta(int Id, int VeiculoId, string Modelo, DateOnly Data,
    decimal Valor, string Descricao);
public sealed record StatusVeiculoConsulta(int Id, int VeiculoId, string Modelo,
    DateTime InicioUtc, DateTime? FimUtc, string Descricao);
public sealed record VencimentoCnhConsulta(int Id, int MotoristaId, string Nome, DateOnly Data, bool Vencido);

public interface IConsultasFrota
{
    Task<PagedResult<MotoristaConsulta>> MotoristasAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<PagedResult<VeiculoConsulta>> VeiculosAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<PagedResult<MovimentacaoConsulta>> MovimentacoesAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<PagedResult<AbastecimentoConsulta>> AbastecimentosAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<PagedResult<MultaConsulta>> MultasAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<PagedResult<ManutencaoConsulta>> ManutencoesAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<PagedResult<StatusVeiculoConsulta>> StatusVeiculosAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<PagedResult<VencimentoCnhConsulta>> VencimentosCnhAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct);
    Task<int?> UltimaQuilometragemAsync(int veiculoId, string origem, CancellationToken ct);
}
