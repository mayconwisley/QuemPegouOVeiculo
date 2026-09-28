using QuemPegouOVeiculo.Application.Common;

namespace QuemPegouOVeiculo.Api.Common;

public static class ResultHttpExtensions
{
    public static IResult ToHttpResult(this Result result) =>
        result.IsSuccess ? Results.NoContent() : ToProblem(result.Error);

    public static IResult ToHttpResult<T>(this Result<T> result) =>
        result.IsSuccess ? Results.Ok(result.Value) : ToProblem(result.Error);

    public static IResult ToCreatedHttpResult(this Result<int> result, string resourcePath) =>
        result.IsSuccess
            ? Results.Created($"{resourcePath}/{result.Value}", new { id = result.Value })
            : ToProblem(result.Error);

    private static IResult ToProblem(Error error)
    {
        var status = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => throw new ArgumentOutOfRangeException(nameof(error))
        };

        return Results.Problem(statusCode: status, detail: error.Message,
            extensions: new Dictionary<string, object?> { ["code"] = error.Code });
    }
}
