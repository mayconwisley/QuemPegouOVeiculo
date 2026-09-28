using System.Linq;

namespace QuemPegouOVeiculo.Desktop.Client.Validation
{
    public static class ValidarCPF
    {
        public static bool CPF(string valor)
        {
            var cpf = new string((valor ?? string.Empty)
                .Where(caractere => caractere >= '0' && caractere <= '9').ToArray());

            if (cpf.Length != 11 || cpf.All(caractere => caractere == cpf[0]))
                return false;

            for (var tamanho = 9; tamanho <= 10; tamanho++)
            {
                var soma = 0;
                for (var indice = 0; indice < tamanho; indice++)
                    soma += (cpf[indice] - '0') * (tamanho + 1 - indice);

                var digito = (soma * 10) % 11;
                if (digito == 10)
                    digito = 0;

                if (digito != cpf[tamanho] - '0')
                    return false;
            }

            return true;
        }
    }
}

