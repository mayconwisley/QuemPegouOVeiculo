using FleetManagement.Application.Modules.Access;
using FleetManagement.Domain.Common;
using FleetManagement.Domain.Modules.Access;

namespace FleetManagement.Tests;

public sealed class AccessServiceTests
{
    [Fact]
    public void UserAccount_RejectsUnknownRole()
    {
        Assert.Throws<DomainException>(() => new UserAccount("ana", "hashed", "Owner"));
    }

    [Fact]
    public async Task Create_RejectsShortPassword()
    {
        var service = new AccessService(new FakeStore(), new FakePasswords());

        var result = await service.CreateAsync(new UserInput("ana", "short", UserRoles.Operator),
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("validation", result.Error.Code);
    }

    [Fact]
    public async Task Update_ProtectsLastAdministrator()
    {
        var store = new FakeStore { User = new UserAccount("admin", "hashed", UserRoles.Administrator) };
        var service = new AccessService(store, new FakePasswords());

        var result = await service.UpdateAsync(store.User.Id, new UserUpdate(UserRoles.Viewer, true), TestIds.OtherUser,
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal("conflict", result.Error.Code);
        Assert.Equal(UserRoles.Administrator, store.User.Role);
    }

    [Fact]
    public async Task Update_ProtectsOwnAdministrativeAccess()
    {
        var store = new FakeStore { User = new UserAccount("admin", "hashed", UserRoles.Administrator) };
        var service = new AccessService(store, new FakePasswords());

        var result = await service.UpdateAsync(store.User.Id, new UserUpdate(UserRoles.Administrator, false), store.User.Id,
            CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.True(store.User.IsActive);
    }

    [Fact]
    public async Task Authenticate_RejectsInactiveAccount()
    {
        var user = new UserAccount("ana", "hashed:correct-password", UserRoles.Operator);
        user.SetActive(false);
        var service = new AccessService(new FakeStore { User = user }, new FakePasswords());

        Assert.Null(await service.AuthenticateAsync("ana", "correct-password", CancellationToken.None));
    }

    [Fact]
    public void UserAccount_AccessChangesKeepOldSecurityVersionsRevoked()
    {
        var user = new UserAccount("ana", "hashed", UserRoles.Viewer);
        var original = user.SecurityVersion;

        user.SetActive(false);
        user.SetActive(true);
        Assert.NotEqual(original, user.SecurityVersion);

        var reactivated = user.SecurityVersion;
        user.SetRole(UserRoles.Operator);
        user.SetRole(UserRoles.Viewer);
        Assert.NotEqual(reactivated, user.SecurityVersion);

        var sameAccess = user.SecurityVersion;
        user.SetRole(UserRoles.Viewer);
        user.SetActive(true);
        Assert.Equal(sameAccess, user.SecurityVersion);
    }

    private sealed class FakeStore : IUserAccountStore
    {
        public UserAccount? User { get; set; }
        public Task<UserAccount?> FindByUsernameAsync(string username, CancellationToken ct) =>
            Task.FromResult(User?.Username == username ? User : null);
        public Task<UserAccount?> FindByIdAsync(Guid id, CancellationToken ct) => Task.FromResult(User);
        public Task<IReadOnlyList<UserView>> ListAsync(CancellationToken ct) =>
            Task.FromResult<IReadOnlyList<UserView>>([]);
        public Task<int> ActiveAdministratorsAsync(CancellationToken ct) => Task.FromResult(1);
        public Task AddAsync(UserAccount user, CancellationToken ct) { User = user; return Task.CompletedTask; }
        public Task SaveAsync(CancellationToken ct) => Task.CompletedTask;
    }

    private sealed class FakePasswords : IPasswordService
    {
        public string Hash(UserAccount user, string password) => "hashed:" + password;
        public bool Verify(UserAccount user, string password) => user.PasswordHash == "hashed:" + password;
    }
}
