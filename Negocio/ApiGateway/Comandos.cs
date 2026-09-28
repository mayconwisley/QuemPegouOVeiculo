using System;
using System.Collections.Generic;
using Negocio.ApiGateway;
using Objeto;

namespace Negocio.ApiGateway
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

namespace Negocio.Motorista
{
    public static class Insert { public static bool Register(MotoristaObj x) => ApiCliente.Gravar("motoristas", 0, 'I', Corpos.Motorista(x)); }
    public static class Update { public static bool Register(MotoristaObj x) => ApiCliente.Gravar("motoristas", x.Id, 'U', Corpos.Motorista(x)); }
    public static class Delete { public static bool Register(MotoristaObj x) => ApiCliente.Gravar("motoristas", x.Id, 'D'); }
}

namespace Negocio.Veiculo
{
    public static class Insert { public static bool Register(VeiculoObj x) => ApiCliente.Gravar("veiculos", 0, 'I', Corpos.Veiculo(x)); }
    public static class Update { public static bool Register(VeiculoObj x) => ApiCliente.Gravar("veiculos", x.Id, 'U', Corpos.Veiculo(x)); }
    public static class Delete { public static bool Register(VeiculoObj x) => ApiCliente.Gravar("veiculos", x.Id, 'D'); }
}

namespace Negocio.Controle.Veiculo
{
    public static class Insert { public static bool Register(ControleVeiculoObj x) => ApiCliente.Gravar("movimentacoes", 0, 'I', Corpos.Movimentacao(x)); }
    public static class Update
    {
        public static bool Register(ControleVeiculoObj x) => ApiCliente.Gravar("movimentacoes", x.Id, 'U', Corpos.Movimentacao(x));
        public static bool RegisterControl(ControleVeiculoObj x) => ApiCliente.ConcluirMovimentacao(x.Id, x.DataHoraChegada, x.KmFinal);
    }
    public static class Delete { public static bool Register(ControleVeiculoObj x) => ApiCliente.Gravar("movimentacoes", x.Id, 'D'); }
}

namespace Negocio.Controle.Combustivel
{
    public static class Insert { public static bool Register(ControleAbastecimentoObj x) => ApiCliente.Gravar("abastecimentos", 0, 'I', Corpos.Abastecimento(x)); }
    public static class Update { public static bool Register(ControleAbastecimentoObj x) => ApiCliente.Gravar("abastecimentos", x.Id, 'U', Corpos.Abastecimento(x)); }
    public static class Delete { public static bool Register(ControleAbastecimentoObj x) => ApiCliente.Gravar("abastecimentos", x.Id, 'D'); }
}

namespace Negocio.Controle.Multa
{
    public static class Insert { public static bool Register(ControleMultaObj x) => ApiCliente.Gravar("multas", 0, 'I', Corpos.Multa(x)); }
    public static class Update { public static bool Register(ControleMultaObj x) => ApiCliente.Gravar("multas", x.Id, 'U', Corpos.Multa(x)); }
    public static class Delete { public static bool Register(ControleMultaObj x) => ApiCliente.Gravar("multas", x.Id, 'D'); }
}

namespace Negocio.Controle.Mecanica
{
    public static class Insert { public static bool Register(ControleManutencaoObj x) => ApiCliente.Gravar("manutencoes", 0, 'I', Corpos.Manutencao(x)); }
    public static class Update { public static bool Register(ControleManutencaoObj x) => ApiCliente.Gravar("manutencoes", x.Id, 'U', Corpos.Manutencao(x)); }
    public static class Delete { public static bool Register(ControleManutencaoObj x) => ApiCliente.Gravar("manutencoes", x.Id, 'D'); }
}

namespace Negocio.Controle.StatusVeiculo
{
    public static class Insert { public static bool Register(StatusVeiculoObj x) => ApiCliente.Gravar("status-veiculo", 0, 'I', Corpos.StatusVeiculo(x)); }
    public static class Update { public static bool Register(StatusVeiculoObj x) => ApiCliente.Gravar("status-veiculo", x.Id, 'U', Corpos.StatusVeiculo(x)); }
    public static class Delete { public static bool Register(StatusVeiculoObj x) => ApiCliente.Gravar("status-veiculo", x.Id, 'D'); }
}

namespace Negocio.Controle.CNH
{
    public static class Insert { public static bool Register(VencimentoCNHObj x) => ApiCliente.Gravar("vencimentos-cnh", 0, 'I', Corpos.VencimentoCnh(x)); }
    public static class Update { public static bool Register(VencimentoCNHObj x) => ApiCliente.Gravar("vencimentos-cnh", x.Id, 'U', Corpos.VencimentoCnh(x)); }
    public static class Delete { public static bool Register(VencimentoCNHObj x) => ApiCliente.Gravar("vencimentos-cnh", x.Id, 'D'); }
}
