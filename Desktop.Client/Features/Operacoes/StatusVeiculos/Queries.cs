using System;
using System.Data;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos
{
    public static class Query
    {
        private const string Recurso = "status-veiculo";
        public static DataTable Register(string search) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterAll() => ApiCliente.Listar(Recurso);
        public static DataTable RegisterVehicle(int idVeiculo) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo));
        public static DataTable RegisterDateStart(DateTime inicio, DateTime fim) => Periodo(inicio, fim, "inicio");
        public static DataTable RegisterDateFinal(DateTime inicio, DateTime fim) => Periodo(inicio, fim, "fim");
        public static DataTable RegisterDateFinalNull() => ApiCliente.Listar(Recurso, ApiCliente.Filtros("emAberto", true));
        private static DataTable Periodo(DateTime inicio, DateTime fim, string campo) => ApiCliente.Listar(Recurso,
            ApiCliente.Filtros("inicioUtc", ApiCliente.InicioUtc(inicio), "fimUtc", ApiCliente.FimUtc(fim), "campoData", campo));
    }
}
