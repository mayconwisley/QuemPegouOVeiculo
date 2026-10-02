using FleetManagement.Application.Modules.Access;
using FleetManagement.Domain.Modules.Access;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using FleetManagement.Application.Common;
using System.Data;

namespace FleetManagement.Infrastructure.Persistence;

internal sealed class UserAccountStore(FleetDbContext db) : IUserAccountStore
{
    public Task<UserAccount?> FindByUsernameAsync(string username, CancellationToken ct) =>
        db.UserAccounts.SingleOrDefaultAsync(x => x.Username == username, ct);

    public Task<UserAccount?> FindByIdAsync(int id, CancellationToken ct) =>
        db.UserAccounts.FindAsync([id], ct).AsTask();

    public async Task<IReadOnlyList<UserView>> ListAsync(CancellationToken ct) =>
        await db.UserAccounts.AsNoTracking().OrderBy(x => x.Username)
            .Select(x => new UserView(x.Id, x.Username, x.Role, x.IsActive)).ToListAsync(ct);

    public Task<int> ActiveAdministratorsAsync(CancellationToken ct) =>
        db.UserAccounts.CountAsync(x => x.IsActive && x.Role == UserRoles.Administrator, ct);

    public async Task AddAsync(UserAccount user, CancellationToken ct) =>
        await db.UserAccounts.AddAsync(user, ct);

    public async Task SaveAsync(CancellationToken ct)
    {
        try
        {
            await using var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);
            await db.SaveChangesAsync(ct);
            if (!await db.UserAccounts.AnyAsync(x => x.IsActive && x.Role == UserRoles.Administrator, ct))
                throw new BusinessConflictException("É necessário manter ao menos um administrador ativo.");
            await transaction.CommitAsync(ct);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException
            { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            throw new BusinessConflictException("Usuário já cadastrado.", ex);
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.SerializationFailure)
        {
            throw new BusinessConflictException("O cadastro foi alterado por outro usuário. Recarregue e tente novamente.", ex);
        }
    }
}

internal sealed class PasswordService : IPasswordService
{
    private readonly PasswordHasher<UserAccount> _hasher = new();

    public string Hash(UserAccount user, string password) => _hasher.HashPassword(user, password);

    public bool Verify(UserAccount user, string password) =>
        _hasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;
}
