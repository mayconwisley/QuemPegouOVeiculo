using Microsoft.AspNetCore.Http;
using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Common;

namespace FleetManagement.Tests;

public sealed class ResultTests
{
    [Fact]
    public async Task TryAsync_ConvertsExpectedFailures()
    {
        var invalid = await Result.TryAsync<int>(() => throw new DomainException("Placa inválida."));
        var conflict = await Result.CaptureAsync(() => throw new BusinessConflictException("Placa duplicada."));

        Assert.Equal(ErrorType.Validation, invalid.Error.Type);
        Assert.Equal("Placa inválida.", invalid.Error.Message);
        Assert.Equal(ErrorType.Conflict, conflict.Error.Type);
    }

    [Fact]
    public async Task TryAsync_PreservesUnexpectedFailures()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            Result.CaptureAsync(() => throw new InvalidOperationException("Falha inesperada.")));
    }

    [Theory]
    [InlineData(ErrorType.Validation, 400)]
    [InlineData(ErrorType.NotFound, 404)]
    [InlineData(ErrorType.Conflict, 409)]
    public void ToHttpResult_MapsErrorType(ErrorType type, int expectedStatus)
    {
        var result = Result.Failure(new Error(type, "test", "Falha conhecida."));

        var response = result.ToHttpResult();

        Assert.Equal(expectedStatus, Assert.IsAssignableFrom<IStatusCodeHttpResult>(response).StatusCode);
    }

    [Fact]
    public void ToHttpResult_PreservesSuccessContracts()
    {
        var created = Result<Guid>.Success(TestIds.Vehicle).ToCreatedHttpResult("/api/v1/vehicles");
        var updated = Result.Success().ToHttpResult();

        Assert.Equal(201, Assert.IsAssignableFrom<IStatusCodeHttpResult>(created).StatusCode);
        Assert.Equal(204, Assert.IsAssignableFrom<IStatusCodeHttpResult>(updated).StatusCode);
    }
}
