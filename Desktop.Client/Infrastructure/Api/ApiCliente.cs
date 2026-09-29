using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace QuemPegouOVeiculo.Desktop.Client.Infrastructure.Api
{
    internal static class ApiCliente
    {
        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
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

        internal static DataTable Listar(string recurso, Dictionary<string, string> filtros = null) =>
            ListarAsync(recurso, filtros, CancellationToken.None).GetAwaiter().GetResult();

        internal static async Task<DataTable> ListarAsync(string recurso, Dictionary<string, string> filtros,
            CancellationToken cancellationToken)
        {
            var tabela = LegacyTableMapper.CriarTabela(recurso);
            var pagina = 1;
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var parametros = filtros == null
                    ? new Dictionary<string, string>()
                    : new Dictionary<string, string>(filtros);
                parametros["page"] = pagina.ToString(Invariant);
                parametros["pageSize"] = "100";
                var resposta = await ObterAsync("consultas/" + recurso, parametros, cancellationToken)
                    .ConfigureAwait(false);
                var itens = (object[])resposta["items"];
                foreach (Dictionary<string, object> item in itens)
                    LegacyTableMapper.AdicionarLinha(tabela, recurso, item);
                if (tabela.Rows.Count >= Convert.ToInt32(resposta["total"], Invariant) || itens.Length == 0)
                    return tabela;
                pagina++;
            }
        }

        internal static string UltimaQuilometragem(int veiculoId, string origem) =>
            UltimaQuilometragemAsync(veiculoId, origem, CancellationToken.None).GetAwaiter().GetResult();

        internal static async Task<string> UltimaQuilometragemAsync(int veiculoId, string origem,
            CancellationToken cancellationToken)
        {
            var resposta = await ObterAsync("consultas/veiculos/" + veiculoId + "/ultima-quilometragem",
                Filtros("origem", origem), cancellationToken).ConfigureAwait(false);
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

        private static async Task<Dictionary<string, object>> ObterAsync(string caminho,
            Dictionary<string, string> parametros, CancellationToken cancellationToken)
        {
            var consulta = parametros.Count == 0 ? "" : "?" + string.Join("&", parametros.Select(x =>
                Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value)));
            return (Dictionary<string, object>)await EnviarAsync(HttpMethod.Get, caminho + consulta, null,
                cancellationToken).ConfigureAwait(false);
        }

        private static object Enviar(HttpMethod metodo, string caminho, Dictionary<string, object> corpo) =>
            EnviarAsync(metodo, caminho, corpo, CancellationToken.None).GetAwaiter().GetResult();

        private static async Task<object> EnviarAsync(HttpMethod metodo, string caminho,
            Dictionary<string, object> corpo, CancellationToken cancellationToken)
        {
            using (var requisicao = new HttpRequestMessage(metodo, BaseUrl + "/api/v1/" + caminho))
            {
                if (corpo != null)
                    requisicao.Content = new StringContent(
                        new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(corpo),
                        Encoding.UTF8, "application/json");
                HttpResponseMessage resposta;
                try
                {
                    resposta = await Http.SendAsync(requisicao, cancellationToken).ConfigureAwait(false);
                }
                catch (HttpRequestException ex)
                {
                    throw new InvalidOperationException(
                        "Não foi possível conectar à API em " + BaseUrl + ". Inicie a API e confira a URL configurada no desktop.", ex);
                }
                catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (TaskCanceledException ex)
                {
                    throw new InvalidOperationException(
                        "A API em " + BaseUrl + " não respondeu dentro de 30 segundos. Verifique a API e o PostgreSQL.", ex);
                }

                using (resposta)
                {
                    var conteudo = await resposta.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!resposta.IsSuccessStatusCode)
                    {
                        var detalhe = "Falha na API: " + (int)resposta.StatusCode + ".";
                        try
                        {
                            var problema = (Dictionary<string, object>)
                                new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.DeserializeObject(conteudo);
                            detalhe = (problema.ContainsKey("detail") ? problema["detail"] as string : null)
                                ?? (problema.ContainsKey("title") ? problema["title"] as string : null)
                                ?? detalhe;
                        }
                        catch (Exception) { }
                        throw new InvalidOperationException(detalhe);
                    }
                    return string.IsNullOrWhiteSpace(conteudo) ? null
                        : new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.DeserializeObject(conteudo);
                }
            }
        }

    }
}
