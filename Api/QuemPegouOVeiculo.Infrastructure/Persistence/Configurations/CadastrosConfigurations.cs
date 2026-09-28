using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuemPegouOVeiculo.Domain.Modules.Cadastros;

namespace QuemPegouOVeiculo.Infrastructure.Persistence.Configurations;

internal sealed class MotoristaConfiguration : IEntityTypeConfiguration<Motorista>
{
    public void Configure(EntityTypeBuilder<Motorista> builder)
    {
        builder.ToTable("motoristas", "cadastros");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Cnh).HasColumnName("cnh").HasMaxLength(20).IsRequired();
        builder.Property(x => x.VencimentoCnh).HasColumnName("vencimento_cnh").HasColumnType("date");
        builder.Property(x => x.CategoriaCnh).HasColumnName("categoria_cnh").HasMaxLength(10).IsRequired();
        builder.Property(x => x.Cpf).HasColumnName("cpf").HasMaxLength(11).IsRequired();
        builder.Property(x => x.Rg).HasColumnName("rg").HasMaxLength(20).IsRequired();
        builder.Property(x => x.Ativo).HasColumnName("ativo");
        builder.HasIndex(x => x.Cpf).IsUnique();
    }
}

internal sealed class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.ToTable("veiculos", "cadastros");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Placa).HasColumnName("placa").HasMaxLength(7).IsRequired();
        builder.Property(x => x.Modelo).HasColumnName("modelo").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Chassi).HasColumnName("chassi").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Renavam).HasColumnName("renavam").HasMaxLength(20).IsRequired();
        builder.Property(x => x.Ativo).HasColumnName("ativo");
        builder.HasIndex(x => x.Placa).IsUnique();
    }
}
