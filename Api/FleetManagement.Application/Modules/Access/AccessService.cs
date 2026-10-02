using FleetManagement.Application.Common;
using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Access;

namespace FleetManagement.Application.Modules.Access;

public sealed record UserView(int Id, string Username, string Role, bool IsActive);
public sealed record UserInput(string Username, string Password, string Role);
public sealed record UserUpdate(string Role, bool IsActive);

public interface IUserAccountStore
{
    Task<UserAccount?> FindByUsernameAsync(string username, CancellationToken ct);
    Task<UserAccount?> FindByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<UserView>> ListAsync(CancellationToken ct);
    Task<int> ActiveAdministratorsAsync(CancellationToken ct);
    Task AddAsync(UserAccount user, CancellationToken ct);
    Task SaveAsync(CancellationToken ct);
}

public interface IPasswordService
{
    string Hash(UserAccount user, string password);
    bool Verify(UserAccount user, string password);
}

public sealed class AccessService(IUserAccountStore store, IPasswordService passwords)
{
    public Task<IReadOnlyList<UserView>> ListAsync(CancellationToken ct) => store.ListAsync(ct);

    public async Task<UserAccount?> AuthenticateAsync(string? username, string? password, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            return null;
        var user = await store.FindByUsernameAsync(username.Trim().ToLowerInvariant(), ct);
        return user is { IsActive: true } && passwords.Verify(user, password) ? user : null;
    }

    public Task<Result<UserView>> CreateAsync(UserInput input, CancellationToken ct) =>
        Result.CaptureValueAsync(async () =>
        {
            var username = Guard.Required(input.Username, "Usuário", 80).ToLowerInvariant();
            ValidatePassword(input.Password);
            if (!UserRoles.IsValid(input.Role))
                throw new DomainException("Perfil de acesso inválido.");
            if (await store.FindByUsernameAsync(username, ct) is not null)
                return Result<UserView>.Failure(Error.Conflict("Usuário já cadastrado."));
            var user = new UserAccount(username, "pending", input.Role);
            user.SetPasswordHash(passwords.Hash(user, input.Password));
            await store.AddAsync(user, ct);
            await store.SaveAsync(ct);
            return Result<UserView>.Success(new UserView(user.Id, user.Username, user.Role, user.IsActive));
        });

    public Task<Result> UpdateAsync(int id, UserUpdate input, int actorId, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            var user = await store.FindByIdAsync(id, ct);
            if (user is null)
                return Result.Failure(Error.NotFound("Usuário", id));
            if (!UserRoles.IsValid(input.Role))
                throw new DomainException("Perfil de acesso inválido.");
            if (id == actorId && (!input.IsActive || input.Role != UserRoles.Administrator))
                return Result.Failure(Error.Conflict("Não é possível remover o próprio acesso administrativo."));
            if (user.IsActive && user.Role == UserRoles.Administrator &&
                (!input.IsActive || input.Role != UserRoles.Administrator) &&
                await store.ActiveAdministratorsAsync(ct) <= 1)
                return Result.Failure(Error.Conflict("É necessário manter ao menos um administrador ativo."));
            user.SetRole(input.Role);
            user.SetActive(input.IsActive);
            await store.SaveAsync(ct);
            return Result.Success();
        });

    public Task<Result> ResetPasswordAsync(int id, string password, CancellationToken ct) =>
        Result.CaptureAsync(async () =>
        {
            ValidatePassword(password);
            var user = await store.FindByIdAsync(id, ct);
            if (user is null)
                return Result.Failure(Error.NotFound("Usuário", id));
            user.SetPasswordHash(passwords.Hash(user, password));
            await store.SaveAsync(ct);
            return Result.Success();
        });

    private static void ValidatePassword(string? password)
    {
        if (password is null || password.Length is < 12 or > 128)
            throw new DomainException("A senha deve ter entre 12 e 128 caracteres.");
    }
}
