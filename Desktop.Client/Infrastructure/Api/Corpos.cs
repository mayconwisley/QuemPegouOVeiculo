using System;
using System.Collections.Generic;
using QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api;
using QuemPegouOVeiculo.Desktop.Models;

namespace QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api
{
    internal static class Corpos
    {
        internal static Dictionary<string, object> Motorista(MotoristaObj x) => new Dictionary<string, object>
        {
            ["nome"] = x.Nome, ["cnh"] = x.CNH, ["vencimentoCnh"] = ApiCliente.Data(x.VencimentoCNH),
            ["categoriaCnh"] = x.CategoriaCNH, ["cpf"] = x.CPF, ["rg"] = x.RG, ["ativo"] = x.Ativo == 'A'
        };
        internal static Dictionary<string, object> Veiculo(VeiculoObj x) => new Dictionary<string, object>
        {
            ["placa"] = x.Placa, ["modelo"] = x.Modelo, ["chassi"] = x.Chassi,
            ["renavam"] = x.Renavam, ["ativo"] = x.Status == 'A'
        };
        internal static Dictionary<string, object> Movimentacao(ControleVeiculoObj x)
        {
            if (!x.DataHoraSaida.HasValue)
                throw new ArgumentException("Informe a data e hora de saída.");
            return new Dictionary<string, object>
            {
                ["veiculoId"] = x.Veiculo.Id, ["motoristaId"] = x.Motorista.Id,
                ["saidaUtc"] = ApiCliente.Utc(x.DataHoraSaida.Value),
                ["chegadaUtc"] = x.DataHoraChegada.HasValue ? (object)ApiCliente.Utc(x.DataHoraChegada.Value) : null,
                ["kmInicial"] = ApiCliente.Quilometragem(x.KmInicial, "KM inicial"),
                ["kmFinal"] = ApiCliente.QuilometragemOpcional(x.KmFinal, "KM final"),
                ["descricao"] = x.Descricao
            };
        }
        internal static Dictionary<string, object> Abastecimento(ControleAbastecimentoObj x) => new Dictionary<string, object>
        {
            ["veiculoId"] = x.Veiculo.Id, ["motoristaId"] = x.Motorista.Id,
            ["quilometragem"] = ApiCliente.Quilometragem(x.KmInicio, "KM inicial"),
            ["data"] = ApiCliente.Data(x.Data), ["valor"] = x.Valor, ["litros"] = x.Litros,
            ["descricao"] = x.Descricao
        };
        internal static Dictionary<string, object> Multa(ControleMultaObj x) => new Dictionary<string, object>
        {
            ["veiculoId"] = x.Veiculo.Id, ["motoristaId"] = x.Motorista.Id,
            ["data"] = ApiCliente.Data(x.Data), ["valor"] = x.Valor, ["pontos"] = x.Pontos,
            ["descricao"] = x.Descricao
        };
        internal static Dictionary<string, object> Manutencao(ControleManutencaoObj x) => new Dictionary<string, object>
        {
            ["veiculoId"] = x.Veiculo.Id, ["data"] = ApiCliente.Data(x.Data),
            ["valor"] = x.Valor, ["descricao"] = x.Descricao
        };
        internal static Dictionary<string, object> StatusVeiculo(StatusVeiculoObj x)
        {
            if (!x.DataHoraInicial.HasValue)
                throw new ArgumentException("Informe a data e hora inicial do status.");
            return new Dictionary<string, object>
            {
                ["veiculoId"] = x.Veiculo.Id,
                ["inicioUtc"] = ApiCliente.Utc(x.DataHoraInicial.Value),
                ["fimUtc"] = x.DataHoraFinal.HasValue ? (object)ApiCliente.Utc(x.DataHoraFinal.Value) : null,
                ["descricao"] = x.Descricao
            };
        }
        internal static Dictionary<string, object> VencimentoCnh(VencimentoCNHObj x) => new Dictionary<string, object>
        {
            ["motoristaId"] = x.Motorista.Id, ["data"] = ApiCliente.Data(x.Data),
            ["vencido"] = x.Status == 'V'
        };
    }
}
