using System.Windows.Forms;

namespace QuemPegouOVeiculo.Shared.Presentation
{
    public static class LimparCampos
    {
        public static void LimparTextBox(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox textBox)
                    textBox.Clear();
            }
        }

        public static void LimparMaskedTextBox(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is MaskedTextBox maskedTextBox)
                    maskedTextBox.Clear();
            }
        }
    }
}
