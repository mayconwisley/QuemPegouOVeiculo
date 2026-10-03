using FleetManagement.Domain.Common;

namespace FleetManagement.Domain.Modules.Access;

public static class UserRoles
{
    public const string Administrator = "Administrator";
    public const string Operator = "Operator";
    public const string Viewer = "Viewer";

    public static bool IsValid(string? role) => role is Administrator or Operator or Viewer;
}

public sealed class UserAccount : IEntity
{
    private UserAccount() { }

    public UserAccount(string username, string passwordHash, string role)
    {
        Username = Guard.Required(username, "Usuário", 80).ToLowerInvariant();
        PasswordHash = Guard.Required(passwordHash, "Hash da senha", 500);
        SecurityVersion = Guid.NewGuid().ToString("N");
        SetRole(role);
        IsActive = true;
    }

    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public string Username { get; private set; } = "";
    public string PasswordHash { get; private set; } = "";
    public string Role { get; private set; } = "";
    public bool IsActive { get; private set; }
    public string SecurityVersion { get; private set; } = "";

    public void SetRole(string role)
    {
        if (!UserRoles.IsValid(role))
            throw new DomainException("Perfil de acesso inválido.");
        if (Role == role)
            return;
        Role = role;
        RotateSecurityVersion();
    }

    public void SetActive(bool active)
    {
        if (IsActive == active)
            return;
        IsActive = active;
        RotateSecurityVersion();
    }

    public void SetPasswordHash(string hash)
    {
        PasswordHash = Guard.Required(hash, "Hash da senha", 500);
        RotateSecurityVersion();
    }

    private void RotateSecurityVersion() => SecurityVersion = Guid.NewGuid().ToString("N");
}
