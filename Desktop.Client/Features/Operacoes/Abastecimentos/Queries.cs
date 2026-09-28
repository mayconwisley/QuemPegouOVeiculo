using System;
using System.Data;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;

namespace QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos
{
    public static class Query
    {
        private const string Recurso = "abastecimentos";
        public static DataTable Register(string search) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterAll() => ApiCliente.Listar(Recurso);
        public static DataTable RegisterVehicle(int idVeiculo) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo));
        public static DataTable RegisterDriver(int idMotorista) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("motoristaId", idMotorista));
        public static DataTable RegisterVehicleDriver(int idVeiculo, int idMotorista) =>
            ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo, "motoristaId", idMotorista));
        public static DataTable RegisterPeriod(DateTime inicio, DateTime fim) => Periodo(inicio, fim);
        public static DataTable RegisterPeriodVehicle(DateTime inicio, DateTime fim, int idVeiculo) => Periodo(inicio, fim, idVeiculo);
        public static DataTable RegisterPeriodDriver(DateTime inicio, DateTime fim, int idMotorista) => Periodo(inicio, fim, null, idMotorista);
        public static string UltimoKmVeiculo(int idVeiculo) => ApiCliente.UltimaQuilometragem(idVeiculo, "abastecimento");
        private static DataTable Periodo(DateTime inicio, DateTime fim, int? veiculo = null, int? motorista = null) =>
            ApiCliente.Listar(Recurso, ApiCliente.Filtros("dataDe", ApiCliente.Data(inicio), "dataAte", ApiCliente.Data(fim),
                "veiculoId", veiculo, "motoristaId", motorista));
    }
}
