using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FleetManagement.Domain.Modules.Access;
using Microsoft.IdentityModel.Tokens;

namespace FleetManagement.Api.Common;

internal sealed class TokenSettings
{
    public const string Issuer = "FleetManagement.Api";
    public const string Audience = "FleetManagement.Client";

    public TokenSettings(IHostEnvironment environment)
    {
        var configured = Environment.GetEnvironmentVariable("FLEET_JWT_KEY")
            ?? (OperatingSystem.IsWindows()
                ? Environment.GetEnvironmentVariable("FLEET_JWT_KEY", EnvironmentVariableTarget.Machine)
                : null);
        if (string.IsNullOrEmpty(configured) && !environment.IsDevelopment())
            throw new InvalidOperationException("FLEET_JWT_KEY é obrigatório fora de Development.");
        Key = string.IsNullOrEmpty(configured)
            ? RandomNumberGenerator.GetBytes(64)
            : Encoding.UTF8.GetBytes(configured);
        if (Key.Length < 32)
            throw new InvalidOperationException("FLEET_JWT_KEY deve ter pelo menos 32 bytes UTF-8.");
    }

    public byte[] Key { get; }

    public string SecurityStamp(UserAccount user) => Base64UrlEncoder.Encode(
        HMACSHA256.HashData(Key, Encoding.UTF8.GetBytes(user.PasswordHash + ":" + user.SecurityVersion)));
}

internal sealed class TokenService(TokenSettings settings)
{
    public (string Token, DateTime ExpiresAtUtc) Create(UserAccount user)
    {
        var expires = DateTime.UtcNow.AddHours(8);
        var claims = new[]
        {
            new Claim("sub", user.Id.ToString(System.Globalization.CultureInfo.InvariantCulture)),
            new Claim("name", user.Username),
            new Claim("role", user.Role),
            new Claim("stamp", settings.SecurityStamp(user))
        };
        var token = new JwtSecurityToken(TokenSettings.Issuer, TokenSettings.Audience, claims,
            expires: expires, signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(settings.Key), SecurityAlgorithms.HmacSha256));
        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}
