using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using FleetManagement.Wpf.Infrastructure.Api;
using Xunit;

namespace FleetManagement.Wpf.Tests;

public sealed class FleetApiClientTests
{
    private const string UserId = "01990000-0000-7000-8000-000000000001";
    private const string VehicleId = "01990000-0000-7000-8000-000000000002";
    private const string DriverId = "01990000-0000-7000-8000-000000000003";

    [Fact]
    public async Task LoginAndQuery_UseJwtAndPagedContract()
    {
        using var handler = new StubHandler(
            Reply(HttpStatusCode.OK, """
                {"token":"token-123","expiresAtUtc":"2026-10-02T00:00:00Z",
                 "user":{"id":"01990000-0000-7000-8000-000000000001","username":"admin","role":"Administrator","isActive":true}}
                """),
            Reply(HttpStatusCode.OK, """
                {"items":[{"id":"01990000-0000-7000-8000-000000000002","plate":"ABC1D23","model":"Sedan","active":true}],
                 "total":1,"page":1,"pageSize":50}
                """));
        using var client = new FleetApiClient(handler);

        await client.LoginAsync("admin", "senha");
        var page = await client.GetPageAsync("queries/vehicles?page=1&pageSize=50");

        Assert.True(client.User?.IsAdmin);
        Assert.Equal(Guid.Parse(UserId), client.User?.Id);
        Assert.Single(page.Items);
        Assert.Equal(VehicleId, page.Items[0]["id"]?.GetValue<string>());
        Assert.Equal(1, page.Total);
        Assert.Equal("/api/v1/queries/vehicles", handler.Requests[1].Path);
        Assert.Equal("Bearer token-123", handler.Requests[1].Authorization);
    }

    [Fact]
    public async Task ValidationFailure_UsesProblemDetailsMessage()
    {
        using var handler = new StubHandler(Reply(HttpStatusCode.Conflict,
            """{"title":"Conflict","detail":"Veículo já reservado.","code":"conflict"}"""));
        using var client = new FleetApiClient(handler);

        var error = await Assert.ThrowsAsync<ApiException>(() =>
            client.SendAsync(HttpMethod.Post, "reservations", new { vehicleId = Guid.Parse(VehicleId) }));

        Assert.Equal(HttpStatusCode.Conflict, error.StatusCode);
        Assert.Equal("Veículo já reservado.", error.Message);
        Assert.Contains($"\"vehicleId\":\"{VehicleId}\"", handler.Requests[0].Body);
    }

    [Fact]
    public async Task SendAsync_SerializesEditorPayloadWithApiFieldNames()
    {
        using var handler = new StubHandler(Reply(HttpStatusCode.Created,
            """{"id":"01990000-0000-7000-8000-000000000004"}"""));
        using var client = new FleetApiClient(handler);
        var payload = new JsonObject
        {
            ["vehicleId"] = VehicleId,
            ["driverId"] = DriverId,
            ["departureUtc"] = "2026-10-01T15:00:00.0000000Z",
            ["arrivalUtc"] = null,
            ["initialMileage"] = 100,
            ["finalMileage"] = null,
            ["description"] = "Teste"
        };

        await client.SendAsync(HttpMethod.Post, "movements", payload);

        var body = JsonNode.Parse(handler.Requests[0].Body);
        Assert.Equal(VehicleId, body?["vehicleId"]?.GetValue<string>());
        Assert.Equal("2026-10-01T15:00:00.0000000Z", body?["departureUtc"]?.GetValue<string>());
        Assert.Null(body?["arrivalUtc"]);
    }

    [Fact]
    public async Task UnauthorizedResponse_RevokesLocalSessionOnce()
    {
        using var handler = new StubHandler(
            Reply(HttpStatusCode.OK, """
                {"token":"token-123","user":{"id":"01990000-0000-7000-8000-000000000001","username":"admin",
                 "role":"Administrator","isActive":true}}
                """),
            Reply(HttpStatusCode.Unauthorized, """{"detail":"Sessão expirada."}"""),
            Reply(HttpStatusCode.Unauthorized, """{"detail":"Sessão expirada."}"""));
        using var client = new FleetApiClient(handler);
        await client.LoginAsync("admin", "senha");
        var expired = 0;
        client.SessionExpired += (_, _) => expired++;

        await Assert.ThrowsAsync<ApiException>(() => client.GetAsync("dashboard"));
        await Assert.ThrowsAsync<ApiException>(() => client.GetAsync("dashboard"));

        Assert.Null(client.User);
        Assert.Equal(1, expired);
        Assert.Null(handler.Requests[2].Authorization);
    }

    private static HttpResponseMessage Reply(HttpStatusCode code, string json) => new(code)
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };

    private sealed class StubHandler(params HttpResponseMessage[] responses) : HttpMessageHandler
    {
        private readonly Queue<HttpResponseMessage> _responses = new(responses);
        public List<Request> Requests { get; } = [];

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Requests.Add(new Request(request.RequestUri?.AbsolutePath ?? "",
                request.Headers.Authorization?.ToString(),
                request.Content is null ? "" : await request.Content.ReadAsStringAsync(cancellationToken)));
            return _responses.Dequeue();
        }
    }

    private sealed record Request(string Path, string? Authorization, string Body);
}
