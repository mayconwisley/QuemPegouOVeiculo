using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FleetManagement.Wpf.Infrastructure.Api;

public sealed record CurrentUser(int Id, string Username, string Role, bool IsActive)
{
    public bool CanWrite => Role is "Administrator" or "Operator";
    public bool IsAdmin => Role == "Administrator";
}

public sealed record PageResult(IReadOnlyList<JsonObject> Items, int Total, int Page, int PageSize);

public sealed class ApiException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public sealed class FleetApiClient : IDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly HttpClient _http;

    public FleetApiClient(HttpMessageHandler? handler = null)
    {
        var baseUrl = Environment.GetEnvironmentVariable("FLEET_MANAGEMENT_API_URL");
        _http = handler is null ? new HttpClient() : new HttpClient(handler);
        _http.BaseAddress = new Uri((string.IsNullOrWhiteSpace(baseUrl)
            ? "http://localhost:5000" : baseUrl.TrimEnd('/')) + "/api/v1/");
        _http.Timeout = TimeSpan.FromSeconds(30);
    }

    public CurrentUser? User { get; private set; }
    public event EventHandler? SessionExpired;

    public async Task LoginAsync(string username, string password, CancellationToken ct = default)
    {
        User = null;
        _http.DefaultRequestHeaders.Authorization = null;
        using var response = await _http.PostAsJsonAsync("auth/login", new { username, password }, JsonOptions, ct);
        var payload = await ReadAsync(response, ct);
        var token = payload?["token"]?.GetValue<string>()
            ?? throw new ApiException("Resposta de autenticação inválida.", response.StatusCode);
        var user = payload["user"]?.Deserialize<CurrentUser>(JsonOptions)
            ?? throw new ApiException("Dados do usuário inválidos.", response.StatusCode);
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        User = user;
    }

    public async Task<JsonObject> GetAsync(string path, CancellationToken ct = default)
    {
        using var response = await _http.GetAsync(path, ct);
        return await ReadAsync(response, ct) ?? new JsonObject();
    }

    public async Task<PageResult> GetPageAsync(string path, CancellationToken ct = default)
    {
        var data = await GetAsync(path, ct);
        var items = data["items"]?.AsArray().OfType<JsonObject>().ToArray() ?? [];
        return new PageResult(items, data["total"]?.GetValue<int>() ?? items.Length,
            data["page"]?.GetValue<int>() ?? 1, data["pageSize"]?.GetValue<int>() ?? 50);
    }

    public async Task<IReadOnlyList<JsonObject>> GetArrayAsync(string path, CancellationToken ct = default)
    {
        using var response = await _http.GetAsync(path, ct);
        await EnsureSuccessAsync(response, ct);
        var root = await response.Content.ReadFromJsonAsync<JsonArray>(JsonOptions, ct);
        return root?.OfType<JsonObject>().ToArray() ?? [];
    }

    public async Task SendAsync(HttpMethod method, string path, object? body = null,
        CancellationToken ct = default)
    {
        using var request = new HttpRequestMessage(method, path);
        if (body is not null)
            request.Content = JsonContent.Create(body, options: JsonOptions);
        using var response = await _http.SendAsync(request, ct);
        await EnsureSuccessAsync(response, ct);
    }

    public async Task<byte[]> DownloadAsync(string path, CancellationToken ct = default)
    {
        using var response = await _http.GetAsync(path, HttpCompletionOption.ResponseHeadersRead, ct);
        await EnsureSuccessAsync(response, ct);
        return await response.Content.ReadAsByteArrayAsync(ct);
    }

    public static string Query(params (string Name, string? Value)[] parts)
    {
        var values = parts.Where(x => !string.IsNullOrWhiteSpace(x.Value))
            .Select(x => $"{Uri.EscapeDataString(x.Name)}={Uri.EscapeDataString(x.Value!)}");
        var query = string.Join("&", values);
        return query.Length == 0 ? "" : "?" + query;
    }

    public static string Number(int value) => value.ToString(CultureInfo.InvariantCulture);

    private async Task<JsonObject?> ReadAsync(HttpResponseMessage response, CancellationToken ct)
    {
        await EnsureSuccessAsync(response, ct);
        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;
        return await response.Content.ReadFromJsonAsync<JsonObject>(JsonOptions, ct);
    }

    private async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken ct)
    {
        if (response.IsSuccessStatusCode)
            return;

        if (response.StatusCode == HttpStatusCode.Unauthorized && User is not null)
        {
            User = null;
            _http.DefaultRequestHeaders.Authorization = null;
            SessionExpired?.Invoke(this, EventArgs.Empty);
        }

        var raw = await response.Content.ReadAsStringAsync(ct);
        string? detail = null;
        try
        {
            var problem = JsonNode.Parse(raw);
            detail = problem?["detail"]?.GetValue<string>() ?? problem?["title"]?.GetValue<string>();
        }
        catch (JsonException) { }

        throw new ApiException(detail ?? $"A API retornou {(int)response.StatusCode}.", response.StatusCode);
    }

    public void Dispose() => _http.Dispose();
}
