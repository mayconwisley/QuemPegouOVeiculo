using Microsoft.EntityFrameworkCore;
using FleetManagement.Api.Common;
using FleetManagement.Application.Modules.Registrations.Drivers;
using FleetManagement.Application.Modules.Registrations.Vehicles;
using FleetManagement.Api.Modules.Queries;
using FleetManagement.Application.Modules.Queries;
using FleetManagement.Application.Modules.Operations.Refuelings;
using FleetManagement.Application.Modules.Operations.MaintenanceRecords;
using FleetManagement.Application.Modules.Operations.Movements;
using FleetManagement.Application.Modules.Operations.Fines;
using FleetManagement.Application.Modules.Operations.VehicleStatuses;
using FleetManagement.Application.Modules.Operations.LicenseExpirations;
using FleetManagement.Infrastructure;
using FleetManagement.Infrastructure.Persistence;
using Refuelings = FleetManagement.Api.Modules.Operations.Refuelings.Endpoints;
using Maintenance = FleetManagement.Api.Modules.Operations.MaintenanceRecords.Endpoints;
using Drivers = FleetManagement.Api.Modules.Registrations.Drivers.Endpoints;
using Movements = FleetManagement.Api.Modules.Operations.Movements.Endpoints;
using Fines = FleetManagement.Api.Modules.Operations.Fines.Endpoints;
using VehicleStatuses = FleetManagement.Api.Modules.Operations.VehicleStatuses.Endpoints;
using Vehicles = FleetManagement.Api.Modules.Registrations.Vehicles.Endpoints;
using LicenseExpirations = FleetManagement.Api.Modules.Operations.LicenseExpirations.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var connectionString = PostgresConnectionString.Create(builder.Configuration);

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddScoped<DriverCommands>();
builder.Services.AddScoped<DriverQueries>();
builder.Services.AddScoped<VehicleCommands>();
builder.Services.AddScoped<VehicleQueries>();
builder.Services.AddScoped<MovementCommands>();
builder.Services.AddScoped<MovementQueries>();
builder.Services.AddScoped<RefuelingCommands>();
builder.Services.AddScoped<RefuelingQueries>();
builder.Services.AddScoped<FineCommands>();
builder.Services.AddScoped<FineQueries>();
builder.Services.AddScoped<MaintenanceCommands>();
builder.Services.AddScoped<MaintenanceQueries>();
builder.Services.AddScoped<VehicleStatusCommands>();
builder.Services.AddScoped<VehicleStatusQueries>();
builder.Services.AddScoped<LicenseExpirationCommands>();
builder.Services.AddScoped<LicenseExpirationQueries>();
builder.Services.AddScoped<FleetQueries>();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.MapGet("/health/live", () => Results.Ok(new { status = "ok" }));
app.MapGet("/health/ready", async (FleetDbContext db, CancellationToken cancellationToken) =>
    await db.Database.CanConnectAsync(cancellationToken)
        ? Results.Ok(new { status = "ok" })
        : Results.StatusCode(StatusCodes.Status503ServiceUnavailable));

var api = app.MapGroup("/api/v1");
Drivers.MapDrivers(api);
Vehicles.MapVehicles(api);
Movements.MapMovements(api);
Refuelings.MapRefuelings(api);
Fines.MapFines(api);
Maintenance.MapMaintenance(api);
VehicleStatuses.MapVehicleStatuses(api);
LicenseExpirations.MapLicenseExpirations(api);
Endpoints.MapQueries(api);

app.Run();
