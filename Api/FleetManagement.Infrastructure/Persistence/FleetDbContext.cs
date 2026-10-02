using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using FleetManagement.Application.Common;
using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Access;
using FleetManagement.Domain.Modules.Registrations;
using FleetManagement.Domain.Modules.Operations;

namespace FleetManagement.Infrastructure.Persistence;

public sealed class FleetDbContext(DbContextOptions<FleetDbContext> options, IActorContext? actor = null) : DbContext(options)
{
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
    public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleMovement> Movements => Set<VehicleMovement>();
    public DbSet<MovementChecklist> MovementChecklists => Set<MovementChecklist>();
    public DbSet<MaintenancePlan> MaintenancePlans => Set<MaintenancePlan>();
    public DbSet<VehicleReservation> Reservations => Set<VehicleReservation>();
    public DbSet<Refueling> Refuelings => Set<Refueling>();
    public DbSet<Fine> Fines => Set<Fine>();
    public DbSet<Maintenance> Maintenance => Set<Maintenance>();
    public DbSet<VehicleStatus> VehicleStatuses => Set<VehicleStatus>();
    public DbSet<LicenseExpiration> LicenseExpirations => Set<LicenseExpiration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetDbContext).Assembly);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();
        var changes = ChangeTracker.Entries<IEntity>()
            .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(x => new AuditDraft(x, x.State.ToString(), SerializeChanges(x)))
            .ToArray();
        if (changes.Length == 0)
            return await base.SaveChangesAsync(cancellationToken);

        var ownsTransaction = Database.CurrentTransaction is null;
        await using var transaction = ownsTransaction
            ? await Database.BeginTransactionAsync(cancellationToken)
            : null;
        var affected = await base.SaveChangesAsync(cancellationToken);
        foreach (var change in changes)
        {
            AuditEntries.Add(new AuditEntry(actor?.UserId, actor?.Username ?? "system",
                change.Entry.Entity.GetType().Name, change.Entry.Entity.Id,
                change.Action, change.ChangesJson));
        }
        affected += await base.SaveChangesAsync(cancellationToken);
        if (transaction is not null)
            await transaction.CommitAsync(cancellationToken);
        return affected;
    }

    private static string SerializeChanges(EntityEntry<IEntity> entry)
    {
        var values = new Dictionary<string, object?>();
        foreach (var property in entry.Properties)
        {
            if (property.Metadata.IsPrimaryKey() || property.Metadata.Name is "PasswordHash" or "SecurityVersion" or "Cpf" or "Rg" ||
                entry.State == EntityState.Modified && !property.IsModified)
                continue;
            values[property.Metadata.Name] = entry.State switch
            {
                EntityState.Modified => new { Before = property.OriginalValue, After = property.CurrentValue },
                EntityState.Deleted => property.OriginalValue,
                _ => property.CurrentValue
            };
        }
        return JsonSerializer.Serialize(values);
    }

    private sealed record AuditDraft(EntityEntry<IEntity> Entry, string Action, string ChangesJson);
}
