using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace Negocio.ApiGateway
{
    internal static class ApiCliente
    {
        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;
        private static readonly string BaseUrl = (Environment.GetEnvironmentVariable("QUEMPEGOU_API_URL")
            ?? ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "http://localhost:5000").TrimEnd('/');

        internal static Dictionary<string, string> Filtros(params object[] pares)
        {
            var filtros = new Dictionary<string, string>();
            for (var i = 0; i < pares.Length; i += 2)
            {
                if (pares[i + 1] == null)
                    continue;
                var valor = Convert.ToString(pares[i + 1], Invariant);
                if (!string.IsNullOrWhiteSpace(valor))
                    filtros.Add((string)pares[i], valor);
            }
            return filtros;
        }

        internal static string Busca(string texto) => (texto ?? "").Trim('%').Trim();
        internal static string Data(DateTime data) => data.ToString("yyyy-MM-dd", Invariant);
        internal static string Utc(DateTime data) => data.Kind == DateTimeKind.Utc
            ? data.ToString("O", Invariant)
            : DateTime.SpecifyKind(data, DateTimeKind.Local).ToUniversalTime().ToString("O", Invariant);
        internal static string InicioUtc(DateTime data) => Utc(data.Date);
        internal static string FimUtc(DateTime data) => Utc(data.Date.AddDays(1));

        internal static DataTable Listar(string recurso, Dictionary<string, string> filtros = null)
        {
            var tabela = CriarTabela(recurso);
            var pagina = 1;
            while (true)
            {
                var parametros = filtros == null
                    ? new Dictionary<string, string>()
                    : new Dictionary<string, string>(filtros);
                parametros["page"] = pagina.ToString(Invariant);
                parametros["pageSize"] = "100";
                var resposta = Obter("consultas/" + recurso, parametros);
                var itens = (object[])resposta["items"];
                foreach (Dictionary<string, object> item in itens)
                    AdicionarLinha(tabela, recurso, item);
                if (tabela.Rows.Count >= Inteiro(resposta, "total") || itens.Length == 0)
                    return tabela;
                pagina++;
            }
        }

        internal static string UltimaQuilometragem(int veiculoId, string origem)
        {
            var resposta = Obter("consultas/veiculos/" + veiculoId + "/ultima-quilometragem",
                Filtros("origem", origem));
            return resposta["quilometragem"] == null ? "" : Convert.ToString(resposta["quilometragem"], Invariant);
        }

        internal static bool Gravar(string recurso, int id, char operacao, Dictionary<string, object> corpo = null)
        {
            var caminho = recurso + (operacao == 'I' ? "" : "/" + id.ToString(Invariant));
            var metodo = operacao == 'I' ? HttpMethod.Post : operacao == 'U' ? HttpMethod.Put : HttpMethod.Delete;
            Enviar(metodo, caminho, corpo);
            return true;
        }

        internal static bool ConcluirMovimentacao(int id, DateTime? chegada, string kmFinal)
        {
            if (!chegada.HasValue)
                throw new ArgumentException("Informe a data e hora de chegada.");
            Enviar(HttpMethod.Post, "movimentacoes/" + id + "/concluir", new Dictionary<string, object>
            {
                ["chegadaUtc"] = Utc(chegada.Value), ["kmFinal"] = Quilometragem(kmFinal, "KM final")
            });
            return true;
        }

        internal static int Quilometragem(string valor, string campo)
        {
            int numero;
            if (!int.TryParse(valor, NumberStyles.Integer, Invariant, out numero) || numero < 0)
                throw new ArgumentException(campo + " deve ser um número inteiro não negativo.");
            return numero;
        }

        internal static int? QuilometragemOpcional(string valor, string campo) =>
            string.IsNullOrWhiteSpace(valor) ? (int?)null : Quilometragem(valor, campo);

        private static Dictionary<string, object> Obter(string caminho, Dictionary<string, string> parametros)
        {
            var consulta = parametros.Count == 0 ? "" : "?" + string.Join("&", parametros.Select(x =>
                Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value)));
            return (Dictionary<string, object>)Enviar(HttpMethod.Get, caminho + consulta, null);
        }

        private static object Enviar(HttpMethod metodo, string caminho, Dictionary<string, object> corpo)
        {
            using (var requisicao = new HttpRequestMessage(metodo, BaseUrl + "/api/v1/" + caminho))
            {
                if (corpo != null)
                    requisicao.Content = new StringContent(Json.Serialize(corpo), Encoding.UTF8, "application/json");
                using (var resposta = Http.SendAsync(requisicao).GetAwaiter().GetResult())
                {
                    var conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!resposta.IsSuccessStatusCode)
                    {
                        var detalhe = "Falha na API: " + (int)resposta.StatusCode + ".";
                        try
                        {
                            var problema = (Dictionary<string, object>)Json.DeserializeObject(conteudo);
                            detalhe = Texto(problema, "detail") ?? Texto(problema, "title") ?? detalhe;
                        }
                        catch (Exception) { }
                        throw new InvalidOperationException(detalhe);
                    }
                    return string.IsNullOrWhiteSpace(conteudo) ? null : Json.DeserializeObject(conteudo);
                }
            }
        }

        private static DataTable CriarTabela(string recurso)
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

        private static void AdicionarLinha(DataTable tabela, string recurso, Dictionary<string, object> item)
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
