using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuemPegouOVeiculo.Application.Common;
using QuemPegouOVeiculo.Application.Modules.Consultas;
using QuemPegouOVeiculo.Application.Modules.Operacoes;
using QuemPegouOVeiculo.Infrastructure.Persistence;

namespace QuemPegouOVeiculo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextPool<FleetDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(ICommandRepository<>), typeof(EfCommandRepository<>));
        services.AddScoped(typeof(IQueryRepository<>), typeof(EfQueryRepository<>));
        services.AddScoped<ICadastrosStatusReader, CadastrosStatusReader>();
        services.AddScoped<IConsultasFrota, ConsultasFrota>();
        return services;
    }
}
