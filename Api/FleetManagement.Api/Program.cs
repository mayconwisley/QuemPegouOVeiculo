using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.RateLimiting;
using FleetManagement.Api.Common;
using FleetManagement.Application.Common;
using FleetManagement.Application.Modules.Access;
using FleetManagement.Domain.Modules.Access;
using FleetManagement.Application.Modules.Registrations.Drivers;
using FleetManagement.Application.Modules.Registrations.Vehicles;
using FleetManagement.Api.Modules.Queries;
using FleetManagement.Application.Modules.Queries;
using FleetManagement.Application.Modules.Operations.Refuelings;
using FleetManagement.Application.Modules.Operations.MaintenanceRecords;
using FleetManagement.Application.Modules.Operations.Movements;
using FleetManagement.Application.Modules.Operations.Reservations;
using FleetManagement.Application.Modules.Operations.Fines;
using FleetManagement.Application.Modules.Operations.VehicleStatuses;
using FleetManagement.Application.Modules.Operations.LicenseExpirations;
using FleetManagement.Infrastructure;
using FleetManagement.Infrastructure.Persistence;
using Refuelings = FleetManagement.Api.Modules.Operations.Refuelings.Endpoints;
using Maintenance = FleetManagement.Api.Modules.Operations.MaintenanceRecords.Endpoints;
using MaintenancePlans = FleetManagement.Api.Modules.Operations.MaintenanceRecords.PlanEndpoints;
using Drivers = FleetManagement.Api.Modules.Registrations.Drivers.Endpoints;
using Movements = FleetManagement.Api.Modules.Operations.Movements.Endpoints;
using Reservations = FleetManagement.Api.Modules.Operations.Reservations.Endpoints;
using Fines = FleetManagement.Api.Modules.Operations.Fines.Endpoints;
using VehicleStatuses = FleetManagement.Api.Modules.Operations.VehicleStatuses.Endpoints;
using Vehicles = FleetManagement.Api.Modules.Registrations.Vehicles.Endpoints;
using LicenseExpirations = FleetManagement.Api.Modules.Operations.LicenseExpirations.Endpoints;
using AccessEndpoints = FleetManagement.Api.Modules.Access.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var connectionString = PostgresConnectionString.Create(builder.Configuration);
var tokenSettings = new TokenSettings(builder.Environment);

builder.Services.AddSingleton(tokenSettings);
builder.Services.AddSingleton<TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IActorContext, HttpActorContext>();
builder.Services.AddScoped<AccessService>();
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = TokenSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = TokenSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(tokenSettings.Key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1),
        NameClaimType = "name",
        RoleClaimType = "role"
    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            if (!Guid.TryParse(context.Principal?.FindFirstValue("sub"), out var id)
                || id == Guid.Empty)
            {
                context.Fail("Sessão inválida.");
                return;
            }
            var db = context.HttpContext.RequestServices.GetRequiredService<FleetDbContext>();
            var user = await db.UserAccounts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (user is not { IsActive: true } || user.Role != context.Principal?.FindFirstValue("role") ||
                tokenSettings.SecurityStamp(user) != context.Principal?.FindFirstValue("stamp"))
                context.Fail("Sessão revogada.");
        }
    };
});
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", policy => policy.RequireRole(UserRoles.Administrator))
    .AddPolicy("write", policy => policy.RequireRole(UserRoles.Administrator, UserRoles.Operator));
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("login", context => RateLimitPartition.GetFixedWindowLimiter(
        context.Connection.RemoteIpAddress?.ToString() ?? "unknown", _ =>
            new FixedWindowRateLimiterOptions { PermitLimit = 5, Window = TimeSpan.FromMinutes(1), QueueLimit = 0 }));
});
builder.Services.AddScoped<DriverCommands>();
builder.Services.AddScoped<DriverQueries>();
builder.Services.AddScoped<VehicleCommands>();
builder.Services.AddScoped<VehicleQueries>();
builder.Services.AddScoped<MovementCommands>();
builder.Services.AddScoped<MovementQueries>();
builder.Services.AddScoped<ReservationCommands>();
builder.Services.AddScoped<ReservationQueries>();
builder.Services.AddScoped<ChecklistService>();
builder.Services.AddScoped<RefuelingCommands>();
builder.Services.AddScoped<RefuelingQueries>();
builder.Services.AddScoped<FineCommands>();
builder.Services.AddScoped<FineQueries>();
builder.Services.AddScoped<MaintenanceCommands>();
builder.Services.AddScoped<MaintenanceQueries>();
builder.Services.AddScoped<MaintenancePlanCommands>();
builder.Services.AddScoped<MaintenancePlanQueries>();
builder.Services.AddScoped<VehicleStatusCommands>();
builder.Services.AddScoped<VehicleStatusQueries>();
builder.Services.AddScoped<LicenseExpirationCommands>();
builder.Services.AddScoped<LicenseExpirationQueries>();
builder.Services.AddScoped<FleetQueries>();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();
if (args.Contains("--bootstrap-admin"))
{
    var username = Environment.GetEnvironmentVariable("FLEET_BOOTSTRAP_USERNAME");
    var password = Environment.GetEnvironmentVariable("FLEET_BOOTSTRAP_PASSWORD");
    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        throw new InvalidOperationException("Defina FLEET_BOOTSTRAP_USERNAME e FLEET_BOOTSTRAP_PASSWORD no processo.");
    await using var scope = app.Services.CreateAsyncScope();
    var users = scope.ServiceProvider.GetRequiredService<IUserAccountStore>();
    if ((await users.ListAsync(CancellationToken.None)).Count != 0)
        throw new InvalidOperationException("O administrador inicial só pode ser criado em um banco sem usuários.");
    var result = await scope.ServiceProvider.GetRequiredService<AccessService>()
        .CreateAsync(new UserInput(username, password, UserRoles.Administrator), CancellationToken.None);
    if (!result.IsSuccess)
        throw new InvalidOperationException(result.Error.Message);
    Console.WriteLine($"Administrador inicial '{result.Value.Username}' criado.");
    return;
}
app.UseExceptionHandler();
app.UseRouting();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapGet("/health/live", () => Results.Ok(new { status = "ok" }));
app.MapGet("/health/ready", async (FleetDbContext db, CancellationToken cancellationToken) =>
    await db.Database.CanConnectAsync(cancellationToken)
        ? Results.Ok(new { status = "ok" })
        : Results.StatusCode(StatusCodes.Status503ServiceUnavailable));

var api = app.MapGroup("/api/v1").RequireAuthorization().AddEndpointFilter<WriteAuthorizationFilter>();
AccessEndpoints.MapAccess(api);
Drivers.MapDrivers(api);
Vehicles.MapVehicles(api);
Movements.MapMovements(api);
Reservations.MapReservations(api);
Refuelings.MapRefuelings(api);
Fines.MapFines(api);
Maintenance.MapMaintenance(api);
MaintenancePlans.MapMaintenancePlans(api);
VehicleStatuses.MapVehicleStatuses(api);
LicenseExpirations.MapLicenseExpirations(api);
Endpoints.MapQueries(api);
FleetManagement.Api.Modules.Exports.Endpoints.MapExports(api);

app.Run();
