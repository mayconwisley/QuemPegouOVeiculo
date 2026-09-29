using System.Globalization;
using System.Text;

namespace FleetManagement.Shared.Presentation
{
    public static class AmountFormatter
    {
        public static string Amount(string amount)
        {
            var resultado = new StringBuilder();
            var possuiVirgula = false;

            foreach (var caractere in amount ?? string.Empty)
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

        public static string ParaAmount(string amount) =>
            decimal.Parse(amount, NumberStyles.Number, CultureInfo.CurrentCulture)
                .ToString("#,##0.00", CultureInfo.CurrentCulture);

        public static string Zero(string amount) =>
            string.IsNullOrWhiteSpace(amount) ? "0,00" : amount;
    }
}
