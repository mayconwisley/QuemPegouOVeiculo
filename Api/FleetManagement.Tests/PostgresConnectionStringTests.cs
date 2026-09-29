using Npgsql;
using FleetManagement.Api.Common;

namespace FleetManagement.Tests;

public sealed class PostgresConnectionStringTests
{
    [Theory]
    [InlineData("qveiculo_dev", "Disable")]
    [InlineData("qveiculo_prod", "Prefer")]
    public void BuildsConnectionStringWithSeparateCredentials(string database, string sslMode)
    {
        var settings = new PostgresSettings
        {
            Host = "localhost",
            Port = 5432,
            Database = database,
            SslMode = sslMode
        };

        var connection = new NpgsqlConnectionStringBuilder(
            PostgresConnectionString.Create(settings, "postgres", "senha;com espaços"));

        Assert.Equal("localhost", connection.Host);
        Assert.Equal(5432, connection.Port);
        Assert.Equal(database, connection.Database);
        Assert.Equal("postgres", connection.Username);
        Assert.Equal("senha;com espaços", connection.Password);
        Assert.True(connection.Pooling);
    }

    [Fact]
    public void RejectsMissingCredentials()
    {
        var settings = new PostgresSettings
        {
            Host = "localhost",
            Database = "qveiculo_dev",
            SslMode = "Disable"
        };

        Assert.Throws<InvalidOperationException>(() =>
            PostgresConnectionString.Create(settings, "postgres", null));
    }
}
