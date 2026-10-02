using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FleetManagement.Desktop.Client.Infrastructure.Api
{
    internal static class ApiClient
    {
        private static readonly HttpClient Http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        private static readonly HttpClient DownloadHttp = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;
        private static readonly string BaseUrl = (Environment.GetEnvironmentVariable("FLEET_MANAGEMENT_API_URL")
            ?? Environment.GetEnvironmentVariable("QUEMPEGOU_API_URL")
            ?? ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "http://localhost:5000").TrimEnd('/');

        internal static Dictionary<string, string> Filters(params object[] pairs)
        {
            var filters = new Dictionary<string, string>();
            for (var i = 0; i < pairs.Length; i += 2)
            {
                if (pairs[i + 1] == null)
                    continue;
                var value = Convert.ToString(pairs[i + 1], Invariant);
                if (!string.IsNullOrWhiteSpace(value))
                    filters.Add((string)pairs[i], value);
            }
            return filters;
        }

        internal static string NormalizeSearch(string textBrush) => (textBrush ?? "").Trim('%').Trim();
        internal static string Date(DateTime data) => data.ToString("yyyy-MM-dd", Invariant);
        internal static string Utc(DateTime data) => data.Kind == DateTimeKind.Utc
            ? data.ToString("O", Invariant)
            : DateTime.SpecifyKind(data, DateTimeKind.Local).ToUniversalTime().ToString("O", Invariant);
        internal static string StartUtc(DateTime data) => Utc(data.Date);
        internal static string EndUtc(DateTime data) => Utc(data.Date.AddDays(1));

        internal static DataTable List(string resource, Dictionary<string, string> filters = null) =>
            ListAsync(resource, filters, CancellationToken.None).GetAwaiter().GetResult();

        internal static async Task<DataTable> ListAsync(string resource, Dictionary<string, string> filters,
            CancellationToken cancellationToken)
        {
            var table = LegacyTableMapper.CreateTable(resource);
            var pageNumber = 1;
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var parameters = filters == null
                    ? new Dictionary<string, string>()
                    : new Dictionary<string, string>(filters);
                parameters["page"] = pageNumber.ToString(Invariant);
                parameters["pageSize"] = "100";
                var response = await ObterAsync("queries/" + resource, parameters, cancellationToken)
                    .ConfigureAwait(false);
                var items = (object[])response["items"];
                foreach (Dictionary<string, object> item in items)
                    LegacyTableMapper.AddRow(table, resource, item);
                if (table.Rows.Count >= Convert.ToInt32(response["total"], Invariant) || items.Length == 0)
                    return table;
                pageNumber++;
            }
        }

        internal static string LatestMileage(int vehicleId, string sourceForm) =>
            LatestMileageAsync(vehicleId, sourceForm, CancellationToken.None).GetAwaiter().GetResult();

        internal static async Task<string> LatestMileageAsync(int vehicleId, string sourceForm,
            CancellationToken cancellationToken)
        {
            var response = await ObterAsync("queries/vehicles/" + vehicleId + "/latest-mileage",
                Filters("source", sourceForm), cancellationToken).ConfigureAwait(false);
            return response["mileage"] == null ? "" : Convert.ToString(response["mileage"], Invariant);
        }

        internal static bool Save(string resource, int id, char operation, Dictionary<string, object> body = null)
        {
            var path = resource + (operation == 'I' ? "" : "/" + id.ToString(Invariant));
            var method = operation == 'I' ? HttpMethod.Post : operation == 'U' ? HttpMethod.Put : HttpMethod.Delete;
            Enviar(method, path, body);
            return true;
        }

        internal static async Task<object> PostAnonymousAsync(string path, Dictionary<string, object> body,
            CancellationToken cancellationToken) =>
            await EnviarAsync(HttpMethod.Post, path, body, cancellationToken).ConfigureAwait(false);

        internal static async Task<object> GetAsync(string path, CancellationToken cancellationToken) =>
            await EnviarAsync(HttpMethod.Get, path, null, cancellationToken).ConfigureAwait(false);

        internal static async Task<object> PostAsync(string path, Dictionary<string, object> body,
            CancellationToken cancellationToken) =>
            await EnviarAsync(HttpMethod.Post, path, body, cancellationToken).ConfigureAwait(false);

        internal static async Task PutAsync(string path, Dictionary<string, object> body,
            CancellationToken cancellationToken) =>
            await EnviarAsync(HttpMethod.Put, path, body, cancellationToken).ConfigureAwait(false);

        internal static async Task DownloadAsync(string path, string destination,
            CancellationToken cancellationToken)
        {
            var directory = Path.GetDirectoryName(Path.GetFullPath(destination));
            var temporary = Path.Combine(directory, ".fleet-export-" + Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + "/api/v1/" + path))
                {
                    if (AccessClient.Token != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessClient.Token);
                    using (var response = await DownloadHttp.SendAsync(request,
                        HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            {
                                AccessClient.Expire();
                                throw new InvalidOperationException("Sessão inválida ou expirada. Entre novamente no sistema.");
                            }
                            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                                throw new InvalidOperationException("Seu perfil não permite esta operação.");
                            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var detail = "Falha na exportação: " + (int)response.StatusCode + ".";
                            try
                            {
                                var problem = (Dictionary<string, object>)new JavaScriptSerializer()
                                    .DeserializeObject(content);
                                detail = Convert.ToString(problem.ContainsKey("detail") ? problem["detail"]
                                    : problem.ContainsKey("title") ? problem["title"] : detail, Invariant);
                            }
                            catch (Exception) { }
                            throw new InvalidOperationException(detail);
                        }
                        using (var source = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var file = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write,
                            FileShare.None, 81920, true))
                            await source.CopyToAsync(file, 81920, cancellationToken).ConfigureAwait(false);
                    }
                }
                if (File.Exists(destination))
                    File.Replace(temporary, destination, null);
                else
                    File.Move(temporary, destination);
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Não foi possível conectar à API em " + BaseUrl + ".", ex);
            }
            finally
            {
                if (File.Exists(temporary)) File.Delete(temporary);
            }
        }

        internal static bool CompleteMovement(int id, DateTime? arrival, string finalMileage)
        {
            if (!arrival.HasValue)
                throw new ArgumentException("Informe a data e hora de chegada.");
            Enviar(HttpMethod.Post, "movements/" + id + "/complete", new Dictionary<string, object>
            {
                ["arrivalUtc"] = Utc(arrival.Value), ["finalMileage"] = Mileage(finalMileage, "KM final")
            });
            return true;
        }

        internal static int Mileage(string amount, string fieldName)
        {
            int number;
            if (!int.TryParse(amount, NumberStyles.Integer, Invariant, out number) || number < 0)
                throw new ArgumentException(fieldName + " deve ser um número inteiro não negativo.");
            return number;
        }

        internal static int? OptionalMileage(string amount, string fieldName) =>
            string.IsNullOrWhiteSpace(amount) ? (int?)null : Mileage(amount, fieldName);

        private static async Task<Dictionary<string, object>> ObterAsync(string path,
            Dictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            var query = parameters.Count == 0 ? "" : "?" + string.Join("&", parameters.Select(x =>
                Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value)));
            return (Dictionary<string, object>)await EnviarAsync(HttpMethod.Get, path + query, null,
                cancellationToken).ConfigureAwait(false);
        }

        private static object Enviar(HttpMethod method, string path, Dictionary<string, object> body) =>
            EnviarAsync(method, path, body, CancellationToken.None).GetAwaiter().GetResult();

        private static async Task<object> EnviarAsync(HttpMethod method, string path,
            Dictionary<string, object> body, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(method, BaseUrl + "/api/v1/" + path))
            {
                if (path != "auth/login" && AccessClient.Token != null)
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessClient.Token);
                if (body != null)
                    request.Content = new StringContent(
                        new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(body),
                        Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                try
                {
                    response = await Http.SendAsync(request, cancellationToken).ConfigureAwait(false);
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

                using (response)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            if (path == "auth/login")
                                throw new InvalidOperationException("Usuário ou senha inválidos.");
                            AccessClient.Expire();
                            throw new InvalidOperationException("Sessão inválida ou expirada. Entre novamente no sistema.");
                        }
                        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                            throw new InvalidOperationException("Seu perfil não permite esta operação.");
                        if ((int)response.StatusCode == 429 && path == "auth/login")
                            throw new InvalidOperationException("Muitas tentativas de acesso. Aguarde um minuto e tente novamente.");
                        var detail = "Falha na API: " + (int)response.StatusCode + ".";
                        try
                        {
                            var problem = (Dictionary<string, object>)
                                new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.DeserializeObject(content);
                            detail = (problem.ContainsKey("detail") ? problem["detail"] as string : null)
                                ?? (problem.ContainsKey("title") ? problem["title"] as string : null)
                                ?? detail;
                        }
                        catch (Exception) { }
                        throw new InvalidOperationException(detail);
                    }
                    return string.IsNullOrWhiteSpace(content) ? null
                        : new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.DeserializeObject(content);
                }
            }
        }

    }
}
