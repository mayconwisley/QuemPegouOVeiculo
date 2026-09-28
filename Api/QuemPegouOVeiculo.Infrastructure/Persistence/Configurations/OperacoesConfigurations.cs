using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemPegouOVeiculo.Domain.Modules.Cadastros;
using QuemPegouOVeiculo.Domain.Modules.Operacoes;

namespace QuemPegouOVeiculo.Infrastructure.Persistence.Configurations;

internal sealed class MovimentacaoConfiguration : IEntityTypeConfiguration<MovimentacaoVeiculo>
{
    public void Configure(EntityTypeBuilder<MovimentacaoVeiculo> builder)
    {
        builder.ToTable("movimentacoes", "operacoes", table =>
        {
            table.HasCheckConstraint("ck_movimentacoes_km", "km_inicial >= 0 AND (km_final IS NULL OR km_final >= km_inicial)");
            table.HasCheckConstraint("ck_movimentacoes_chegada", "(chegada_utc IS NULL) = (km_final IS NULL) AND (chegada_utc IS NULL OR chegada_utc >= saida_utc)");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VeiculoId).HasColumnName("veiculo_id");
        builder.Property(x => x.MotoristaId).HasColumnName("motorista_id");
        builder.Property(x => x.SaidaUtc).HasColumnName("saida_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.ChegadaUtc).HasColumnName("chegada_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.KmInicial).HasColumnName("km_inicial");
        builder.Property(x => x.KmFinal).HasColumnName("km_final");
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(2000).IsRequired();
        builder.Ignore(x => x.EmAberto);
        builder.HasOne<Veiculo>().WithMany().HasForeignKey(x => x.VeiculoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Motorista>().WithMany().HasForeignKey(x => x.MotoristaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.VeiculoId).IsUnique().HasFilter("chegada_utc IS NULL");
        builder.HasIndex(x => new { x.MotoristaId, x.SaidaUtc });
    }
}

internal sealed class AbastecimentoConfiguration : IEntityTypeConfiguration<Abastecimento>
{
    public void Configure(EntityTypeBuilder<Abastecimento> builder)
    {
        builder.ToTable("abastecimentos", "operacoes", table =>
            table.HasCheckConstraint("ck_abastecimentos_valores", "quilometragem >= 0 AND valor >= 0 AND litros > 0"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VeiculoId).HasColumnName("veiculo_id");
        builder.Property(x => x.MotoristaId).HasColumnName("motorista_id");
        builder.Property(x => x.Quilometragem).HasColumnName("quilometragem");
        builder.Property(x => x.Data).HasColumnName("data").HasColumnType("date");
        builder.Property(x => x.Valor).HasColumnName("valor").HasPrecision(18, 2);
        builder.Property(x => x.Litros).HasColumnName("litros").HasPrecision(18, 3);
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(2000).IsRequired();
        builder.HasOne<Veiculo>().WithMany().HasForeignKey(x => x.VeiculoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Motorista>().WithMany().HasForeignKey(x => x.MotoristaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VeiculoId, x.Data });
    }
}

internal sealed class MultaConfiguration : IEntityTypeConfiguration<Multa>
{
    public void Configure(EntityTypeBuilder<Multa> builder)
    {
        builder.ToTable("multas", "operacoes", table =>
            table.HasCheckConstraint("ck_multas_valores", "valor >= 0 AND pontos >= 0"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VeiculoId).HasColumnName("veiculo_id");
        builder.Property(x => x.MotoristaId).HasColumnName("motorista_id");
        builder.Property(x => x.Data).HasColumnName("data").HasColumnType("date");
        builder.Property(x => x.Valor).HasColumnName("valor").HasPrecision(18, 2);
        builder.Property(x => x.Pontos).HasColumnName("pontos");
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(2000).IsRequired();
        builder.HasOne<Veiculo>().WithMany().HasForeignKey(x => x.VeiculoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Motorista>().WithMany().HasForeignKey(x => x.MotoristaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VeiculoId, x.Data });
    }
}

internal sealed class ManutencaoConfiguration : IEntityTypeConfiguration<Manutencao>
{
    public void Configure(EntityTypeBuilder<Manutencao> builder)
    {
        builder.ToTable("manutencoes", "operacoes", table =>
            table.HasCheckConstraint("ck_manutencoes_valor", "valor >= 0"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VeiculoId).HasColumnName("veiculo_id");
        builder.Property(x => x.Data).HasColumnName("data").HasColumnType("date");
        builder.Property(x => x.Valor).HasColumnName("valor").HasPrecision(18, 2);
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(2000).IsRequired();
        builder.HasOne<Veiculo>().WithMany().HasForeignKey(x => x.VeiculoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VeiculoId, x.Data });
    }
}

internal sealed class StatusVeiculoConfiguration : IEntityTypeConfiguration<StatusVeiculo>
{
    public void Configure(EntityTypeBuilder<StatusVeiculo> builder)
    {
        builder.ToTable("status_veiculos", "operacoes", table =>
            table.HasCheckConstraint("ck_status_veiculos_datas", "fim_utc IS NULL OR fim_utc >= inicio_utc"));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.VeiculoId).HasColumnName("veiculo_id");
        builder.Property(x => x.InicioUtc).HasColumnName("inicio_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.FimUtc).HasColumnName("fim_utc").HasColumnType("timestamp with time zone");
        builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(2000).IsRequired();
        builder.HasOne<Veiculo>().WithMany().HasForeignKey(x => x.VeiculoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.VeiculoId, x.InicioUtc });
    }
}

internal sealed class VencimentoCnhConfiguration : IEntityTypeConfiguration<VencimentoCnh>
{
    public void Configure(EntityTypeBuilder<VencimentoCnh> builder)
    {
        builder.ToTable("vencimentos_cnh", "operacoes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.MotoristaId).HasColumnName("motorista_id");
        builder.Property(x => x.Data).HasColumnName("data").HasColumnType("date");
        builder.Property(x => x.Vencido).HasColumnName("vencido");
        builder.HasOne<Motorista>().WithMany().HasForeignKey(x => x.MotoristaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.MotoristaId, x.Data });
    }
}
