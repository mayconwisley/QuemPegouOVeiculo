using FleetManagement.Domain.Modules.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Infrastructure.Persistence.Configurations;

internal sealed class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("users", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();
        builder.Property(x => x.Username).HasColumnName("username").HasMaxLength(80).IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnName("password_hash").HasMaxLength(500).IsRequired();
        builder.Property(x => x.SecurityVersion).HasColumnName("security_version").HasMaxLength(32).IsRequired();
        builder.Property(x => x.Role).HasColumnName("role").HasMaxLength(20).IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("is_active").IsRequired();
        builder.HasIndex(x => x.Username).IsUnique();
    }
}

internal sealed class AuditEntryConfiguration : IEntityTypeConfiguration<AuditEntry>
{
    public void Configure(EntityTypeBuilder<AuditEntry> builder)
    {
        builder.ToTable("audit_entries", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedNever();
        builder.Property(x => x.OccurredAtUtc).HasColumnName("occurred_at_utc").IsRequired();
        builder.Property(x => x.ActorUserId).HasColumnName("actor_user_id");
        builder.Property(x => x.ActorUsername).HasColumnName("actor_username").HasMaxLength(80).IsRequired();
        builder.Property(x => x.EntityName).HasColumnName("entity_name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.EntityId).HasColumnName("entity_id").IsRequired();
        builder.Property(x => x.Action).HasColumnName("action").HasMaxLength(20).IsRequired();
        builder.Property(x => x.ChangesJson).HasColumnName("changes_json").HasColumnType("jsonb").IsRequired();
        builder.HasIndex(x => x.OccurredAtUtc);
        builder.HasIndex(x => new { x.EntityName, x.EntityId });
    }
}
