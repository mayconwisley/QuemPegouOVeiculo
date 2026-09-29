using Microsoft.AspNetCore.Diagnostics;
using FleetManagement.Application.Common;

namespace FleetManagement.Api.Common;

public sealed class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        if (!ExpectedError.IsExpected(exception))
            return false;

        await Result.Failure(ExpectedError.Map(exception)).ToHttpResult()
            .ExecuteAsync(context);
        return true;
    }
}
