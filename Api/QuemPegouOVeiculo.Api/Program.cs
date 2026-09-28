using Microsoft.EntityFrameworkCore;
using QuemPegouOVeiculo.Api.Common;
using QuemPegouOVeiculo.Application.Modules.Cadastros.Motoristas;
using QuemPegouOVeiculo.Application.Modules.Cadastros.Veiculos;
using QuemPegouOVeiculo.Api.Modules.Consultas;
using QuemPegouOVeiculo.Application.Modules.Consultas;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Abastecimentos;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Manutencoes;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Movimentacoes;
using QuemPegouOVeiculo.Application.Modules.Operacoes.Multas;
using QuemPegouOVeiculo.Application.Modules.Operacoes.StatusVeiculos;
using QuemPegouOVeiculo.Application.Modules.Operacoes.VencimentosCnh;
using QuemPegouOVeiculo.Infrastructure;
using QuemPegouOVeiculo.Infrastructure.Persistence;
using Abastecimentos = QuemPegouOVeiculo.Api.Modules.Operacoes.Abastecimentos.Endpoints;
using Manutencoes = QuemPegouOVeiculo.Api.Modules.Operacoes.Manutencoes.Endpoints;
using Motoristas = QuemPegouOVeiculo.Api.Modules.Cadastros.Motoristas.Endpoints;
using Movimentacoes = QuemPegouOVeiculo.Api.Modules.Operacoes.Movimentacoes.Endpoints;
using Multas = QuemPegouOVeiculo.Api.Modules.Operacoes.Multas.Endpoints;
using StatusVeiculos = QuemPegouOVeiculo.Api.Modules.Operacoes.StatusVeiculos.Endpoints;
using Veiculos = QuemPegouOVeiculo.Api.Modules.Cadastros.Veiculos.Endpoints;
using VencimentosCnh = QuemPegouOVeiculo.Api.Modules.Operacoes.VencimentosCnh.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var connectionString = PostgresConnectionString.Create(builder.Configuration);

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddScoped<MotoristaCommands>();
builder.Services.AddScoped<MotoristaQueries>();
builder.Services.AddScoped<VeiculoCommands>();
builder.Services.AddScoped<VeiculoQueries>();
builder.Services.AddScoped<MovimentacaoCommands>();
builder.Services.AddScoped<MovimentacaoQueries>();
builder.Services.AddScoped<AbastecimentoCommands>();
builder.Services.AddScoped<AbastecimentoQueries>();
builder.Services.AddScoped<MultaCommands>();
builder.Services.AddScoped<MultaQueries>();
builder.Services.AddScoped<ManutencaoCommands>();
builder.Services.AddScoped<ManutencaoQueries>();
builder.Services.AddScoped<StatusVeiculoCommands>();
builder.Services.AddScoped<StatusVeiculoQueries>();
builder.Services.AddScoped<VencimentoCnhCommands>();
builder.Services.AddScoped<VencimentoCnhQueries>();
builder.Services.AddScoped<ConsultasFrotaQueries>();
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
Motoristas.MapMotoristas(api);
Veiculos.MapVeiculos(api);
Movimentacoes.MapMovimentacoes(api);
Abastecimentos.MapAbastecimentos(api);
Multas.MapMultas(api);
Manutencoes.MapManutencoes(api);
StatusVeiculos.MapStatusVeiculos(api);
VencimentosCnh.MapVencimentosCnh(api);
Endpoints.MapConsultas(api);

app.Run();
