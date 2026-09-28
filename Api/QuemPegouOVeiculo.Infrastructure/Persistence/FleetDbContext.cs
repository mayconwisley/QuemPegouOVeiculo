using Microsoft.EntityFrameworkCore;
using QuemPegouOVeiculo.Domain.Modules.Cadastros;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Infrastructure.Persistence;

public sealed class FleetDbContext(DbContextOptions<FleetDbContext> options) : DbContext(options)
{
    public DbSet<Motorista> Motoristas => Set<Motorista>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<MovimentacaoVeiculo> Movimentacoes => Set<MovimentacaoVeiculo>();
    public DbSet<Abastecimento> Abastecimentos => Set<Abastecimento>();
    public DbSet<Multa> Multas => Set<Multa>();
    public DbSet<Manutencao> Manutencoes => Set<Manutencao>();
    public DbSet<StatusVeiculo> StatusVeiculos => Set<StatusVeiculo>();
    public DbSet<VencimentoCnh> VencimentosCnh => Set<VencimentoCnh>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetDbContext).Assembly);
}
