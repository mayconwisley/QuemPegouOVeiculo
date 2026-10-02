using Npgsql;

namespace FleetManagement.Api.Common;

public sealed class PostgresSettings
{
    public string? Host { get; init; }
    public int Port { get; init; } = 5432;
    public string? Database { get; init; }
    public string? SslMode { get; init; }
}

public static class PostgresConnectionString
{
    private const string UserVariable = "FleetUser";
    private const string PasswordVariable = "FleetPass";

    public static string Create(IConfiguration configuration)
    {
        var settings = configuration.GetSection("Postgres").Get<PostgresSettings>()
            ?? throw new InvalidOperationException("Configure a seção Postgres para o ambiente atual.");
        return Create(settings, ReadCredential(UserVariable), ReadCredential(PasswordVariable));
    }

    public static string Create(PostgresSettings settings, string? user, string? password)
    {
        if (string.IsNullOrWhiteSpace(settings.Host) || string.IsNullOrWhiteSpace(settings.Database)
            || settings.Port is < 1 or > 65535)
            throw new InvalidOperationException("Configure Postgres:Host, Postgres:Port e Postgres:Database.");

        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(password))
            throw new InvalidOperationException("Configure FleetUser e FleetPass nas variáveis de ambiente.");

        if (!Enum.TryParse<SslMode>(settings.SslMode, true, out var sslMode))
            throw new InvalidOperationException("Postgres:SslMode inválido.");

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = settings.Host,
            Port = settings.Port,
            Database = settings.Database,
            Username = user,
            Password = password,
            SslMode = sslMode,
            Pooling = true,
            ApplicationName = "FleetManagement.Api"
        };
        return builder.ConnectionString;
    }

    private static string? ReadCredential(string name) =>
        Environment.GetEnvironmentVariable(name)
        ?? (OperatingSystem.IsWindows()
            ? Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine)
            : null);
}
