using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Queries;
using FleetManagement.Application.Modules.Operations;
using FleetManagement.Application.Modules.Access;
using FleetManagement.Application.Modules.Dashboard;
using FleetManagement.Application.Modules.Operations.Movements;
using FleetManagement.Application.Modules.Operations.MaintenanceRecords;
using FleetManagement.Application.Modules.Operations.Reservations;
using FleetManagement.Infrastructure.Persistence;

namespace FleetManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FleetDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IUserAccountStore, UserAccountStore>();
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped<IDashboardReader, DashboardReader>();
        services.AddScoped<IMovementChecklistStore, MovementChecklistStore>();
        services.AddScoped<IVehicleMileageReader, VehicleMileageReader>();
        services.AddScoped<IReservationAvailabilityReader, ReservationAvailabilityReader>();
        services.AddScoped<IVehicleScheduleGuard, VehicleScheduleGuard>();
        services.AddScoped<IReservationReadRepository, ReservationReadRepository>();
        services.AddScoped<IAuditReader, AuditReader>();
        services.AddScoped(typeof(ICommandRepository<>), typeof(EfCommandRepository<>));
        services.AddScoped(typeof(IQueryRepository<>), typeof(EfQueryRepository<>));
        services.AddScoped<IRegistrationStatusReader, RegistrationStatusReader>();
        services.AddScoped<IFleetReadRepository, FleetReadRepository>();
        return services;
    }
}
