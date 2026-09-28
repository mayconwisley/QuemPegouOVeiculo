using Microsoft.EntityFrameworkCore;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Consultas;
using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Infrastructure.Persistence;

internal sealed class ConsultasFrota(FleetDbContext db) : IConsultasFrota
{
    public Task<PagedResult<MotoristaConsulta>> MotoristasAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = db.Motoristas.AsNoTracking();
        if (filtro.Ativo.HasValue)
            query = query.Where(x => x.Ativo == filtro.Ativo.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            var cpfBusca = new string(busca.Where(char.IsDigit).ToArray());
            query = query.Where(x => x.Nome.ToLower().Contains(busca) || x.Cnh.ToLower().Contains(busca)
                || x.Cpf.Contains(busca) || (cpfBusca.Length > 0 && x.Cpf.Contains(cpfBusca))
                || x.Rg.ToLower().Contains(busca));
        }
        return PaginarAsync(query.OrderBy(x => x.Nome).ThenBy(x => x.Cpf)
            .Select(x => new MotoristaConsulta(x.Id, x.Nome, x.Cnh, x.VencimentoCnh,
                x.CategoriaCnh, x.Cpf, x.Rg, x.Ativo)), pagina, ct);
    }

    public Task<PagedResult<VeiculoConsulta>> VeiculosAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = db.Veiculos.AsNoTracking();
        if (filtro.Ativo.HasValue)
            query = query.Where(x => x.Ativo == filtro.Ativo.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            var placaBusca = busca.Replace("-", "").Replace(" ", "");
            query = query.Where(x => x.Placa.ToLower().Contains(busca) || x.Modelo.ToLower().Contains(busca)
                || x.Placa.ToLower().Contains(placaBusca) || x.Renavam.ToLower().Contains(busca));
        }
        return PaginarAsync(query.OrderBy(x => x.Modelo).ThenBy(x => x.Placa)
            .Select(x => new VeiculoConsulta(x.Id, x.Placa, x.Modelo, x.Chassi, x.Renavam, x.Ativo)), pagina, ct);
    }

    public Task<PagedResult<MovimentacaoConsulta>> MovimentacoesAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = from item in db.Movimentacoes.AsNoTracking()
                    join veiculo in db.Veiculos.AsNoTracking() on item.VeiculoId equals veiculo.Id
                    join motorista in db.Motoristas.AsNoTracking() on item.MotoristaId equals motorista.Id
                    select new { item, veiculo, motorista };
        if (filtro.VeiculoId.HasValue)
            query = query.Where(x => x.item.VeiculoId == filtro.VeiculoId.Value);
        if (filtro.MotoristaId.HasValue)
            query = query.Where(x => x.item.MotoristaId == filtro.MotoristaId.Value);
        if (filtro.EmAberto.HasValue)
            query = query.Where(x => (x.item.ChegadaUtc == null) == filtro.EmAberto.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            query = query.Where(x => x.veiculo.Modelo.ToLower().Contains(busca)
                || x.motorista.Nome.ToLower().Contains(busca) || x.item.Descricao.ToLower().Contains(busca));
        }
        if (filtro.InicioUtc.HasValue)
            query = filtro.CampoData == "chegada"
                ? query.Where(x => x.item.ChegadaUtc >= filtro.InicioUtc.Value)
                : query.Where(x => x.item.SaidaUtc >= filtro.InicioUtc.Value);
        if (filtro.FimUtc.HasValue)
            query = filtro.CampoData == "chegada"
                ? query.Where(x => x.item.ChegadaUtc < filtro.FimUtc.Value)
                : query.Where(x => x.item.SaidaUtc < filtro.FimUtc.Value);
        return PaginarAsync(query.OrderByDescending(x => x.item.SaidaUtc).ThenBy(x => x.motorista.Nome)
            .Select(x => new MovimentacaoConsulta(x.item.Id, x.item.VeiculoId, x.veiculo.Modelo,
                x.item.MotoristaId, x.motorista.Nome, x.item.SaidaUtc, x.item.ChegadaUtc,
                x.item.Descricao, x.item.KmInicial, x.item.KmFinal)), pagina, ct);
    }

    public Task<PagedResult<AbastecimentoConsulta>> AbastecimentosAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = from item in db.Abastecimentos.AsNoTracking()
                    join veiculo in db.Veiculos.AsNoTracking() on item.VeiculoId equals veiculo.Id
                    join motorista in db.Motoristas.AsNoTracking() on item.MotoristaId equals motorista.Id
                    select new { item, veiculo, motorista };
        if (filtro.VeiculoId.HasValue)
            query = query.Where(x => x.item.VeiculoId == filtro.VeiculoId.Value);
        if (filtro.MotoristaId.HasValue)
            query = query.Where(x => x.item.MotoristaId == filtro.MotoristaId.Value);
        if (filtro.DataDe.HasValue)
            query = query.Where(x => x.item.Data >= filtro.DataDe.Value);
        if (filtro.DataAte.HasValue)
            query = query.Where(x => x.item.Data <= filtro.DataAte.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            query = query.Where(x => x.veiculo.Modelo.ToLower().Contains(busca)
                || x.motorista.Nome.ToLower().Contains(busca) || x.item.Descricao.ToLower().Contains(busca));
        }
        return PaginarAsync(query.OrderByDescending(x => x.item.Data).ThenBy(x => x.motorista.Nome)
            .Select(x => new AbastecimentoConsulta(x.item.Id, x.item.VeiculoId, x.veiculo.Modelo,
                x.item.MotoristaId, x.motorista.Nome, x.item.Quilometragem, x.item.Data,
                x.item.Valor, x.item.Litros, x.item.Descricao)), pagina, ct);
    }

    public Task<PagedResult<MultaConsulta>> MultasAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = from item in db.Multas.AsNoTracking()
                    join veiculo in db.Veiculos.AsNoTracking() on item.VeiculoId equals veiculo.Id
                    join motorista in db.Motoristas.AsNoTracking() on item.MotoristaId equals motorista.Id
                    select new { item, veiculo, motorista };
        if (filtro.VeiculoId.HasValue)
            query = query.Where(x => x.item.VeiculoId == filtro.VeiculoId.Value);
        if (filtro.MotoristaId.HasValue)
            query = query.Where(x => x.item.MotoristaId == filtro.MotoristaId.Value);
        if (filtro.DataDe.HasValue)
            query = query.Where(x => x.item.Data >= filtro.DataDe.Value);
        if (filtro.DataAte.HasValue)
            query = query.Where(x => x.item.Data <= filtro.DataAte.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            query = query.Where(x => x.veiculo.Modelo.ToLower().Contains(busca)
                || x.motorista.Nome.ToLower().Contains(busca) || x.item.Descricao.ToLower().Contains(busca));
        }
        return PaginarAsync(query.OrderByDescending(x => x.item.Data).ThenBy(x => x.motorista.Nome)
            .Select(x => new MultaConsulta(x.item.Id, x.item.VeiculoId, x.veiculo.Modelo,
                x.item.MotoristaId, x.motorista.Nome, x.item.Data, x.item.Valor,
                x.item.Pontos, x.item.Descricao)), pagina, ct);
    }

    public Task<PagedResult<ManutencaoConsulta>> ManutencoesAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = from item in db.Manutencoes.AsNoTracking()
                    join veiculo in db.Veiculos.AsNoTracking() on item.VeiculoId equals veiculo.Id
                    select new { item, veiculo };
        if (filtro.VeiculoId.HasValue)
            query = query.Where(x => x.item.VeiculoId == filtro.VeiculoId.Value);
        if (filtro.DataDe.HasValue)
            query = query.Where(x => x.item.Data >= filtro.DataDe.Value);
        if (filtro.DataAte.HasValue)
            query = query.Where(x => x.item.Data <= filtro.DataAte.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            query = query.Where(x => x.veiculo.Modelo.ToLower().Contains(busca)
                || x.item.Descricao.ToLower().Contains(busca));
        }
        return PaginarAsync(query.OrderByDescending(x => x.item.Data).ThenBy(x => x.veiculo.Modelo)
            .Select(x => new ManutencaoConsulta(x.item.Id, x.item.VeiculoId, x.veiculo.Modelo,
                x.item.Data, x.item.Valor, x.item.Descricao)), pagina, ct);
    }

    public Task<PagedResult<StatusVeiculoConsulta>> StatusVeiculosAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = from item in db.StatusVeiculos.AsNoTracking()
                    join veiculo in db.Veiculos.AsNoTracking() on item.VeiculoId equals veiculo.Id
                    select new { item, veiculo };
        if (filtro.VeiculoId.HasValue)
            query = query.Where(x => x.item.VeiculoId == filtro.VeiculoId.Value);
        if (filtro.EmAberto.HasValue)
            query = query.Where(x => (x.item.FimUtc == null) == filtro.EmAberto.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            query = query.Where(x => x.veiculo.Modelo.ToLower().Contains(busca)
                || x.item.Descricao.ToLower().Contains(busca));
        }
        if (filtro.InicioUtc.HasValue)
            query = filtro.CampoData == "fim"
                ? query.Where(x => x.item.FimUtc >= filtro.InicioUtc.Value)
                : query.Where(x => x.item.InicioUtc >= filtro.InicioUtc.Value);
        if (filtro.FimUtc.HasValue)
            query = filtro.CampoData == "fim"
                ? query.Where(x => x.item.FimUtc < filtro.FimUtc.Value)
                : query.Where(x => x.item.InicioUtc < filtro.FimUtc.Value);
        return PaginarAsync(query.OrderBy(x => x.veiculo.Modelo).ThenByDescending(x => x.item.InicioUtc)
            .Select(x => new StatusVeiculoConsulta(x.item.Id, x.item.VeiculoId, x.veiculo.Modelo,
                x.item.InicioUtc, x.item.FimUtc, x.item.Descricao)), pagina, ct);
    }

    public Task<PagedResult<VencimentoCnhConsulta>> VencimentosCnhAsync(FiltroConsulta filtro, PageRequest pagina, CancellationToken ct)
    {
        var query = from item in db.VencimentosCnh.AsNoTracking()
                    join motorista in db.Motoristas.AsNoTracking() on item.MotoristaId equals motorista.Id
                    select new { item, motorista };
        if (filtro.MotoristaId.HasValue)
            query = query.Where(x => x.item.MotoristaId == filtro.MotoristaId.Value);
        if (!string.IsNullOrWhiteSpace(filtro.Busca))
        {
            var busca = filtro.Busca.Trim().ToLowerInvariant();
            query = query.Where(x => x.motorista.Nome.ToLower().Contains(busca));
        }
        return PaginarAsync(query.OrderBy(x => x.motorista.Nome)
            .Select(x => new VencimentoCnhConsulta(x.item.Id, x.item.MotoristaId,
                x.motorista.Nome, x.item.Data, x.item.Vencido)), pagina, ct);
    }

    public Task<int?> UltimaQuilometragemAsync(int veiculoId, string origem, CancellationToken ct) => origem switch
    {
        "abastecimento" => db.Abastecimentos.AsNoTracking().Where(x => x.VeiculoId == veiculoId)
            .OrderByDescending(x => x.Id).Select(x => (int?)x.Quilometragem).FirstOrDefaultAsync(ct),
        "movimentacao" => db.Movimentacoes.AsNoTracking().Where(x => x.VeiculoId == veiculoId && x.KmFinal != null)
            .OrderByDescending(x => x.Id).Select(x => x.KmFinal).FirstOrDefaultAsync(ct),
        _ => throw new DomainException("Origem deve ser movimentacao ou abastecimento.")
    };

    private static async Task<PagedResult<T>> PaginarAsync<T>(IQueryable<T> query, PageRequest pagina, CancellationToken ct)
    {
        var offset = pagina.Offset;
        var total = await query.CountAsync(ct);
        var items = await query.Skip(offset).Take(pagina.PageSize).ToListAsync(ct);
        return new PagedResult<T>(items, pagina.Page, pagina.PageSize, total);
    }
}
