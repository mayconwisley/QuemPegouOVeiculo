using System.Security.Claims;
using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Access;
using FleetManagement.Application.Modules.Dashboard;
using FleetManagement.Domain.Modules.Access;

namespace FleetManagement.Api.Modules.Access;

public static class Endpoints
{
    public static void MapAccess(this IEndpointRouteBuilder routes)
    {
        var auth = routes.MapGroup("/auth").WithTags("Acesso");
        auth.MapPost("/login", async (LoginInput input, AccessService access, TokenService tokens,
            CancellationToken ct) =>
        {
            var user = await access.AuthenticateAsync(input.Username, input.Password, ct);
            if (user is null)
                return Results.Problem(statusCode: 401, detail: "Usuário ou senha inválidos.");
            var (token, expires) = tokens.Create(user);
            return Results.Ok(new { token, expiresAtUtc = expires, user = new UserView(user.Id,
                user.Username, user.Role, user.IsActive) });
        }).AllowAnonymous().RequireRateLimiting("login");

        auth.MapGet("/me", (ClaimsPrincipal principal) => Results.Ok(new
        {
            id = int.Parse(principal.FindFirstValue("sub")!),
            username = principal.FindFirstValue("name"),
            role = principal.FindFirstValue("role")
        }));

        var users = routes.MapGroup("/users").WithTags("Usuários").RequireAuthorization("admin");
        users.MapGet("/", async (AccessService access, CancellationToken ct) =>
            Results.Ok(await access.ListAsync(ct)));
        users.MapPost("/", async (UserInput input, AccessService access, CancellationToken ct) =>
            (await access.CreateAsync(input, ct)).ToHttpResult());
        users.MapPut("/{id:int:min(1)}", async (int id, UserUpdate input, ClaimsPrincipal principal,
            AccessService access, CancellationToken ct) =>
            (await access.UpdateAsync(id, input, int.Parse(principal.FindFirstValue("sub")!), ct)).ToHttpResult());
        users.MapPut("/{id:int:min(1)}/password", async (int id, PasswordInput input,
            AccessService access, CancellationToken ct) =>
            (await access.ResetPasswordAsync(id, input.Password, ct)).ToHttpResult());

        routes.MapGet("/audit", async (IAuditReader reader, int page = 1, int pageSize = 50,
            CancellationToken ct = default) =>
            (await Result.TryAsync(() => reader.ListAsync(new PageRequest(page, pageSize), ct))).ToHttpResult())
            .RequireAuthorization("admin").WithTags("Auditoria");

        routes.MapGet("/dashboard", async (IDashboardReader reader, CancellationToken ct) =>
        {
            var now = DateTime.UtcNow;
            var today = DateOnly.FromDateTime(TimeZoneInfo.ConvertTimeFromUtc(now,
                TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo")));
            return Results.Ok(await reader.ReadAsync(now, today, ct));
        }).WithTags("Painel");
    }
}

public sealed record LoginInput(string Username, string Password);
public sealed record PasswordInput(string Password);
