using FleetManagement.Domain.Common;

namespace FleetManagement.Application.Common;

public enum ErrorType
{
    Validation,
    NotFound,
    Conflict
}

public sealed record Error(ErrorType Type, string Code, string Message)
{
    public static Error Validation(string message) => new(ErrorType.Validation, "validation", message);
    public static Error NotFound(string entity, Guid id) =>
        new(ErrorType.NotFound, "not_found", $"{entity} {id} não encontrado.");
    public static Error Conflict(string message) => new(ErrorType.Conflict, "conflict", message);
}

public sealed class Result
{
    private readonly Error? _error;

    private Result(Error? error) => _error = error;

    public bool IsSuccess => _error is null;
    public Error Error => _error ?? throw new InvalidOperationException("O resultado não contém erro.");

    public static Result Success() => new(null);
    public static Result Failure(Error error) => new(error ?? throw new ArgumentNullException(nameof(error)));

    public static async Task<Result<T>> TryAsync<T>(Func<Task<T>> action)
    {
        try
        {
            return Result<T>.Success(await action());
        }
        catch (Exception exception) when (ExpectedError.IsExpected(exception))
        {
            return Result<T>.Failure(ExpectedError.Map(exception));
        }
    }

    public static async Task<Result> CaptureAsync(Func<Task<Result>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception exception) when (ExpectedError.IsExpected(exception))
        {
            return Failure(ExpectedError.Map(exception));
        }
    }

    public static async Task<Result<T>> CaptureValueAsync<T>(Func<Task<Result<T>>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception exception) when (ExpectedError.IsExpected(exception))
        {
            return Result<T>.Failure(ExpectedError.Map(exception));
        }
    }
}

public sealed class Result<T>
{
    private readonly T? _value;
    private readonly Error? _error;

    private Result(T? value, Error? error) => (_value, _error) = (value, error);

    public bool IsSuccess => _error is null;
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("O resultado não contém valor.");
    public Error Error => _error ?? throw new InvalidOperationException("O resultado não contém erro.");

    public static Result<T> Success(T value) =>
        new(value ?? throw new ArgumentNullException(nameof(value)), null);
    public static Result<T> Failure(Error error) =>
        new(default, error ?? throw new ArgumentNullException(nameof(error)));
}

public static class ExpectedError
{
    public static bool IsExpected(Exception exception) =>
        exception is DomainException or BusinessConflictException;

    public static Error Map(Exception exception)
    {
        return exception switch
        {
            DomainException ex => Error.Validation(ex.Message),
            BusinessConflictException ex => Error.Conflict(ex.Message),
            _ => throw new ArgumentOutOfRangeException(nameof(exception))
        };
    }
}
