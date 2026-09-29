using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FleetManagement.Infrastructure.Persistence;

namespace FleetManagement.Api.Common;

public sealed class FleetDbContextFactory : IDesignTimeDbContextFactory<FleetDbContext>
{
    public FleetDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
        var connectionString = PostgresConnectionString.Create(configuration);
        var options = new DbContextOptionsBuilder<FleetDbContext>()
            .UseNpgsql(connectionString)
            .Options;
        return new FleetDbContext(options);
    }
}
