using System.Windows.Forms;

namespace Negocio.Utilitario
{
    public static class HabilitarBotoes
    {
        public static void AlterarExcluir(bool ativar, params Button[] buttons)
        {

            foreach (Button button in buttons)
            {
                if ((ativar == true && button.Name == "BtnAlterar") || (ativar == true && button.Name == "BtnExcluir"))
                {
                    button.Enabled = true;
                }
                else if (ativar == false && button.Name == "BtnGravar")
                {
                    button.Enabled = true;
                }
                else
                {
                    button.Enabled = false;
                }
            }

        }
    }
}
