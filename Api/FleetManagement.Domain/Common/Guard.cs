using System.Text.RegularExpressions;

namespace FleetManagement.Domain.Common;

public static class Guard
{
    private static readonly Regex PlatePattern = new("^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", RegexOptions.Compiled);

    public static string Required(string? value, string field, int maxLength)
    {
        var normalized = value?.Trim() ?? "";
        if (normalized.Length is 0 || normalized.Length > maxLength)
            throw new DomainException($"{field} deve ter entre 1 e {maxLength} caracteres.");
        return normalized;
    }

    public static string Optional(string? value, string field, int maxLength)
    {
        var normalized = value?.Trim() ?? "";
        if (normalized.Length > maxLength)
            throw new DomainException($"{field} deve ter no máximo {maxLength} caracteres.");
        return normalized;
    }

    public static Guid ValidId(Guid value, string field)
    {
        if (value == Guid.Empty)
            throw new DomainException($"{field} deve ter um identificador válido.");
        return value;
    }

    public static int NonNegative(int value, string field)
    {
        if (value < 0)
            throw new DomainException($"{field} não pode ser negativo.");
        return value;
    }

    public static decimal NonNegative(decimal value, string field)
    {
        if (value < 0)
            throw new DomainException($"{field} não pode ser negativo.");
        return value;
    }

    public static DateTime Utc(DateTime value, string field)
    {
        if (value == default || value.Kind != DateTimeKind.Utc)
            throw new DomainException($"{field} deve ser informado em UTC, com sufixo Z.");
        return value;
    }

    public static DateOnly Date(DateOnly value, string field)
    {
        if (value == default)
            throw new DomainException($"{field} é obrigatório.");
        return value;
    }

    public static string Plate(string? value)
    {
        var plate = Required(value, "Placa", 8).Replace("-", "").Replace(" ", "").ToUpperInvariant();
        if (!PlatePattern.IsMatch(plate))
            throw new DomainException("Placa deve seguir o formato brasileiro antigo ou Mercosul.");
        return plate;
    }

    public static string Cpf(string? value)
    {
        var cpf = new string((value ?? "").Where(char.IsDigit).ToArray());
        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            throw new DomainException("CPF inválido.");

        for (var length = 9; length <= 10; length++)
        {
            var sum = 0;
            for (var index = 0; index < length; index++)
                sum += (cpf[index] - '0') * (length + 1 - index);
            var digit = (sum * 10) % 11;
            if (digit == 10)
                digit = 0;
            if (digit != cpf[length] - '0')
                throw new DomainException("CPF inválido.");
        }
        return cpf;
    }
}
