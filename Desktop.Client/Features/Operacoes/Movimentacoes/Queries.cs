using System;
using System.Data;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes
{
    public static class Query
    {
        private const string Recurso = "movimentacoes";
        public static DataTable Register(string search) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterAll() => ApiCliente.Listar(Recurso);
        public static DataTable RegisterArrivalNull() => ApiCliente.Listar(Recurso, ApiCliente.Filtros("emAberto", true));
        public static DataTable RegisterVehicle(int idVeiculo) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo));
        public static DataTable RegisterDriver(int idMotorista) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("motoristaId", idMotorista));
        public static DataTable RegisterVehicleDriver(int idVeiculo, int idMotorista) =>
            ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo, "motoristaId", idMotorista));
        public static DataTable RegisterDateExit(DateTime inicio, DateTime fim) => Periodo(inicio, fim, "saida");
        public static DataTable RegisterDateArrival(DateTime inicio, DateTime fim) => Periodo(inicio, fim, "chegada");
        public static DataTable RegisterDateExitVehicle(DateTime inicio, DateTime fim, int idVeiculo) => Periodo(inicio, fim, "saida", idVeiculo);
        public static DataTable RegisterDateArrivalVehicle(DateTime inicio, DateTime fim, int idVeiculo) => Periodo(inicio, fim, "chegada", idVeiculo);
        public static DataTable RegisterDateExitDriver(DateTime inicio, DateTime fim, int idMotorista) => Periodo(inicio, fim, "saida", null, idMotorista);
        public static DataTable RegisterDateArrivalDriver(DateTime inicio, DateTime fim, int idMotorista) => Periodo(inicio, fim, "chegada", null, idMotorista);
        public static DataTable RegisterDateExitNull() => RegisterArrivalNull();
        private static DataTable Periodo(DateTime inicio, DateTime fim, string campo, int? veiculo = null, int? motorista = null) =>
            ApiCliente.Listar(Recurso, ApiCliente.Filtros("inicioUtc", ApiCliente.InicioUtc(inicio),
                "fimUtc", ApiCliente.FimUtc(fim), "campoData", campo, "veiculoId", veiculo, "motoristaId", motorista));
    }
}
