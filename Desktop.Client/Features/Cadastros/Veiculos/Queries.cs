using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Veiculos
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("veiculos", ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiCliente.ListarAsync("veiculos", ApiCliente.Filtros("busca", ApiCliente.Busca(search)), cancellationToken);
        public static DataTable RegisterVehicleStaus(string search) => search == "%"
            ? ApiCliente.Listar("veiculos") : ApiCliente.Listar("veiculos", ApiCliente.Filtros("ativo", search == "A"));
        public static DataTable IdAndModelActive() => ApiCliente.Listar("veiculos", ApiCliente.Filtros("ativo", true));
        public static Task<DataTable> IdAndModelActiveAsync(CancellationToken cancellationToken) =>
            ApiCliente.ListarAsync("veiculos", ApiCliente.Filtros("ativo", true), cancellationToken);
        public static string UltimoKmVeiculo(int idVeiculo) => ApiCliente.UltimaQuilometragem(idVeiculo, "movimentacao");
        public static Task<string> UltimoKmVeiculoAsync(int idVeiculo, CancellationToken cancellationToken) =>
            ApiCliente.UltimaQuilometragemAsync(idVeiculo, "movimentacao", cancellationToken);
    }
}
