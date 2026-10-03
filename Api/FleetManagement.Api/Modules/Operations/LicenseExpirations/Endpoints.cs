using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Operations.LicenseExpirations;

namespace FleetManagement.Api.Modules.Operations.LicenseExpirations;

public static class Endpoints
{
    public static void MapLicenseExpirations(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/license-expirations").WithTags("Vencimentos de CNH");

        group.MapGet("/", async (LicenseExpirationQueries queries, int page = 1, int pageSize = 50,
            CancellationToken cancellationToken = default) =>
            (await queries.ListAsync(new PageRequest(page, pageSize), cancellationToken)).ToHttpResult());

        group.MapGet("/{id:guid}", async (Guid id, LicenseExpirationQueries queries, CancellationToken cancellationToken) =>
        {
            return (await queries.GetAsync(id, cancellationToken)).ToHttpResult();
        });

        group.MapPost("/", async (LicenseExpirationInput input, LicenseExpirationCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.CreateAsync(input, cancellationToken))
                .ToCreatedHttpResult("/api/v1/license-expirations");
        });

        group.MapPut("/{id:guid}", async (Guid id, LicenseExpirationInput input, LicenseExpirationCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.UpdateAsync(id, input, cancellationToken)).ToHttpResult();
        });

        group.MapDelete("/{id:guid}", async (Guid id, LicenseExpirationCommands commands,
            CancellationToken cancellationToken) =>
        {
            return (await commands.DeleteAsync(id, cancellationToken)).ToHttpResult();
        });
    }
}
