using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api
{
    internal static class LegacyTableMapper
    {
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        internal static DataTable CriarTabela(string recurso)
        {
            var tabela = new DataTable(recurso);
            switch (recurso)
            {
                case "motoristas":
                    Colunas(tabela, "Id:int", "Nome", "NumCNH", "VencimentoCNH:date", "CategoriaCNH", "CPF", "RG", "Ativo"); break;
                case "veiculos":
                    Colunas(tabela, "Id:int", "Placa", "Modelo", "Chassi", "Renavam", "Status"); break;
                case "movimentacoes":
                    Colunas(tabela, "Id:int", "Id_Veiculo:int", "Modelo", "Id_Motorista:int", "Nome",
                        "DataHoraSaida:date", "DataHoraChegada:date", "Dias:int", "Horas:date", "Descricao",
                        "KmInicial", "KmFinal", "KmTotal", "Status"); break;
                case "abastecimentos":
                    Colunas(tabela, "Id:int", "Id_Veiculo:int", "Modelo", "Id_Motorista:int", "Nome", "KmInicial",
                        "Data:date", "Valor:decimal", "Litros:decimal", "Descricao"); break;
                case "multas":
                    Colunas(tabela, "Id:int", "Id_Veiculo:int", "Nome", "Modelo", "Id_Motorista:int",
                        "Data:date", "Valor:decimal", "Pontos:int", "Descricao"); break;
                case "manutencoes":
                    Colunas(tabela, "Id:int", "Id_Veiculo:int", "Modelo", "Data:date", "Valor:decimal", "Descricao"); break;
                case "status-veiculo":
                    Colunas(tabela, "Id:int", "Id_Veiculo:int", "Modelo", "DataHoraInicio:date", "DataHoraFinal:date", "Descricao"); break;
                case "vencimentos-cnh":
                    Colunas(tabela, "Id:int", "Data:date", "Id_Motorista:int", "Nome", "Status"); break;
                default: throw new ArgumentException("Recurso desconhecido: " + recurso);
            }
            return tabela;
        }

        private static void Colunas(DataTable tabela, params string[] definicoes)
        {
            foreach (var definicao in definicoes)
            {
                var partes = definicao.Split(':');
                var tipo = partes.Length == 1 ? typeof(string)
                    : partes[1] == "int" ? typeof(int)
                    : partes[1] == "decimal" ? typeof(decimal) : typeof(DateTime);
                tabela.Columns.Add(partes[0], tipo);
            }
        }

        internal static void AdicionarLinha(DataTable tabela, string recurso, Dictionary<string, object> item)
        {
            switch (recurso)
            {
                case "motoristas":
                    tabela.Rows.Add(Inteiro(item, "id"), Texto(item, "nome"), Texto(item, "cnh"), DataLocal(item, "vencimentoCnh"),
                        Texto(item, "categoriaCnh"), Texto(item, "cpf"), Texto(item, "rg"), Booleano(item, "ativo") ? "Ativo" : "Desativado"); break;
                case "veiculos":
                    tabela.Rows.Add(Inteiro(item, "id"), Texto(item, "placa"), Texto(item, "modelo"), Texto(item, "chassi"),
                        Texto(item, "renavam"), Booleano(item, "ativo") ? "Ativo" : "Desativado"); break;
                case "movimentacoes":
                    var saida = (DateTime)DataLocal(item, "saidaUtc");
                    var chegada = DataLocal(item, "chegadaUtc");
                    var duracao = chegada == DBNull.Value ? TimeSpan.Zero : (DateTime)chegada - saida;
                    var kmInicial = Inteiro(item, "kmInicial");
                    var kmFinal = item["kmFinal"] == null ? (int?)null : Inteiro(item, "kmFinal");
                    tabela.Rows.Add(Inteiro(item, "id"), Inteiro(item, "veiculoId"), Texto(item, "modelo"),
                        Inteiro(item, "motoristaId"), Texto(item, "nome"), saida, chegada, duracao.Days,
                        DateTime.MinValue.Add(duracao - TimeSpan.FromDays(duracao.Days)), Texto(item, "descricao"),
                        kmInicial.ToString(Invariant), kmFinal.HasValue ? kmFinal.Value.ToString(Invariant) : "",
                        kmFinal.HasValue ? (kmFinal.Value - kmInicial).ToString(Invariant) : "",
                        chegada == DBNull.Value ? "S" : "C"); break;
                case "abastecimentos":
                    tabela.Rows.Add(Inteiro(item, "id"), Inteiro(item, "veiculoId"), Texto(item, "modelo"),
                        Inteiro(item, "motoristaId"), Texto(item, "nome"), Inteiro(item, "quilometragem").ToString(Invariant),
                        DataLocal(item, "data"), Decimal(item, "valor"), Decimal(item, "litros"), Texto(item, "descricao")); break;
                case "multas":
                    tabela.Rows.Add(Inteiro(item, "id"), Inteiro(item, "veiculoId"), Texto(item, "nome"), Texto(item, "modelo"),
                        Inteiro(item, "motoristaId"), DataLocal(item, "data"), Decimal(item, "valor"),
                        Inteiro(item, "pontos"), Texto(item, "descricao")); break;
                case "manutencoes":
                    tabela.Rows.Add(Inteiro(item, "id"), Inteiro(item, "veiculoId"), Texto(item, "modelo"),
                        DataLocal(item, "data"), Decimal(item, "valor"), Texto(item, "descricao")); break;
                case "status-veiculo":
                    tabela.Rows.Add(Inteiro(item, "id"), Inteiro(item, "veiculoId"), Texto(item, "modelo"),
                        DataLocal(item, "inicioUtc"), DataLocal(item, "fimUtc"), Texto(item, "descricao")); break;
                case "vencimentos-cnh":
                    tabela.Rows.Add(Inteiro(item, "id"), DataLocal(item, "data"), Inteiro(item, "motoristaId"),
                        Texto(item, "nome"), Booleano(item, "vencido") ? "Vencido" : "Não Vencido"); break;
            }
        }

        private static string Texto(Dictionary<string, object> item, string nome) =>
            item[nome] == null ? "" : Convert.ToString(item[nome], Invariant);
        private static int Inteiro(Dictionary<string, object> item, string nome) => Convert.ToInt32(item[nome], Invariant);
        private static decimal Decimal(Dictionary<string, object> item, string nome) => Convert.ToDecimal(item[nome], Invariant);
        private static bool Booleano(Dictionary<string, object> item, string nome) => Convert.ToBoolean(item[nome], Invariant);
        private static object DataLocal(Dictionary<string, object> item, string nome)
        {
            if (item[nome] == null)
                return DBNull.Value;
            var data = DateTime.Parse(Texto(item, nome), Invariant, DateTimeStyles.RoundtripKind);
            return data.Kind == DateTimeKind.Utc ? data.ToLocalTime() : data;
        }
    }
}
