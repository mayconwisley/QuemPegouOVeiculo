using Microsoft.AspNetCore.Authorization;

namespace FleetManagement.Api.Common;

internal sealed class WriteAuthorizationFilter(IAuthorizationService authorization) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.HttpContext.Request;
        if (HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method) ||
            HttpMethods.IsOptions(request.Method) || request.Path == "/api/v1/auth/login")
            return await next(context);

        var result = await authorization.AuthorizeAsync(context.HttpContext.User, "write");
        return result.Succeeded ? await next(context) : Results.Forbid();
    }
}
