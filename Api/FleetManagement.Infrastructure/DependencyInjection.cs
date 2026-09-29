using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Queries;
using FleetManagement.Application.Modules.Operations;
using FleetManagement.Infrastructure.Persistence;

namespace FleetManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<FleetDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(ICommandRepository<>), typeof(EfCommandRepository<>));
        services.AddScoped(typeof(IQueryRepository<>), typeof(EfQueryRepository<>));
        services.AddScoped<IRegistrationStatusReader, RegistrationStatusReader>();
        services.AddScoped<IFleetReadRepository, FleetReadRepository>();
        return services;
    }
}
