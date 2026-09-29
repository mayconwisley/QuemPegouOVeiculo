using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes
{
    public static class Query
    {
        private const string Recurso = "manutencoes";
        public static DataTable Register(string search) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiCliente.ListarAsync(Recurso, ApiCliente.Filtros("busca", ApiCliente.Busca(search)), cancellationToken);
        public static DataTable RegisterAll() => ApiCliente.Listar(Recurso);
        public static DataTable RegisterVehicle(int idVeiculo) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo));
        public static DataTable RegisterPeriod(DateTime inicio, DateTime fim) => ApiCliente.Listar(Recurso,
            ApiCliente.Filtros("dataDe", ApiCliente.Data(inicio), "dataAte", ApiCliente.Data(fim)));
    }
}
