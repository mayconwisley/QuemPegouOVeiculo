using System.Globalization;
using System.Text;

namespace QuemPegouOVeiculo.Shared.Presentation
{
    public static class FormatarValor
    {
        public static string Valor(string valor)
        {
            var resultado = new StringBuilder();
            var possuiVirgula = false;

            foreach (var caractere in valor ?? string.Empty)
            {
                if ((caractere >= '0' && caractere <= '9') || caractere == '.')
                    resultado.Append(caractere);
                else if (caractere == ',' && !possuiVirgula)
                {
                    resultado.Append(caractere);
                    possuiVirgula = true;
                }
            }

            return resultado.ToString();
        }

        public static string ParaValor(string valor) =>
            decimal.Parse(valor, NumberStyles.Number, CultureInfo.CurrentCulture)
                .ToString("#,##0.00", CultureInfo.CurrentCulture);

        public static string Zero(string valor) =>
            string.IsNullOrWhiteSpace(valor) ? "0,00" : valor;
    }
}
