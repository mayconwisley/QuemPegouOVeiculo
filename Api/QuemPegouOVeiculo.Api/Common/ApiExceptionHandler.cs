using Microsoft.AspNetCore.Diagnostics;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Domain.Common;

namespace QuemPegouOVeiculo.Api.Common;

public sealed class ApiExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        var status = exception switch
        {
            DomainException => StatusCodes.Status400BadRequest,
            EntityNotFoundException => StatusCodes.Status404NotFound,
            BusinessConflictException => StatusCodes.Status409Conflict,
            _ => 0
        };

        if (status == 0)
            return false;

        await Results.Problem(statusCode: status, detail: exception.Message)
            .ExecuteAsync(context);
        return true;
    }
}
