using System;
using System.Data;
using Negocio.ApiGateway;

namespace Negocio.Motorista
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("motoristas", ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterDriverActive(string search)
        {
            var tabela = search == "%" ? ApiCliente.Listar("motoristas")
                : ApiCliente.Listar("motoristas", ApiCliente.Filtros("ativo", search == "A"));
            tabela.Columns["NumCNH"].ColumnName = "CNH";
            return tabela;
        }
        public static DataTable IdAndNameActive() => ApiCliente.Listar("motoristas", ApiCliente.Filtros("ativo", true));
    }
}

namespace Negocio.Veiculo
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("veiculos", ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterVehicleStaus(string search) => search == "%"
            ? ApiCliente.Listar("veiculos") : ApiCliente.Listar("veiculos", ApiCliente.Filtros("ativo", search == "A"));
        public static DataTable IdAndModelActive() => ApiCliente.Listar("veiculos", ApiCliente.Filtros("ativo", true));
        public static string UltimoKmVeiculo(int idVeiculo) => ApiCliente.UltimaQuilometragem(idVeiculo, "movimentacao");
    }
}

namespace Negocio.Controle.Veiculo
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

namespace Negocio.Controle.Combustivel
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

namespace Negocio.Controle.Multa
{
    public static class Query
    {
        private const string Recurso = "multas";
        public static DataTable Register(string search) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterAll() => ApiCliente.Listar(Recurso);
        public static DataTable RegisterVehicle(int idVeiculo) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo));
        public static DataTable RegisterDriver(int idMotorista) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("motoristaId", idMotorista));
        public static DataTable RegisterVehicleDriver(int idVeiculo, int idMotorista) =>
            ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo, "motoristaId", idMotorista));
        public static DataTable RegisterPeriod(DateTime inicio, DateTime fim) => Periodo(inicio, fim);
        public static DataTable RegisterPeriodVehicle(DateTime inicio, DateTime fim, int idVeiculo) => Periodo(inicio, fim, idVeiculo);
        public static DataTable RegisterPeriodDriver(DateTime inicio, DateTime fim, int idMotorista) => Periodo(inicio, fim, null, idMotorista);
        private static DataTable Periodo(DateTime inicio, DateTime fim, int? veiculo = null, int? motorista = null) =>
            ApiCliente.Listar(Recurso, ApiCliente.Filtros("dataDe", ApiCliente.Data(inicio), "dataAte", ApiCliente.Data(fim),
                "veiculoId", veiculo, "motoristaId", motorista));
    }
}

namespace Negocio.Controle.Mecanica
{
    public static class Query
    {
        private const string Recurso = "manutencoes";
        public static DataTable Register(string search) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
        public static DataTable RegisterAll() => ApiCliente.Listar(Recurso);
        public static DataTable RegisterVehicle(int idVeiculo) => ApiCliente.Listar(Recurso, ApiCliente.Filtros("veiculoId", idVeiculo));
        public static DataTable RegisterPeriod(DateTime inicio, DateTime fim) => ApiCliente.Listar(Recurso,
            ApiCliente.Filtros("dataDe", ApiCliente.Data(inicio), "dataAte", ApiCliente.Data(fim)));
    }
}

namespace Negocio.Controle.StatusVeiculo
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

namespace Negocio.Controle.CNH
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiCliente.Listar("vencimentos-cnh",
            ApiCliente.Filtros("busca", ApiCliente.Busca(search)));
    }
}
