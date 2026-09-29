using System.Windows.Forms;

namespace FleetManagement.Shared.Presentation
{
    public static class ClearFields
    {
        public static void ClearTextBox(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox textBox)
                    textBox.Clear();
            }
        }

        public static void ClearMaskedTextBox(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is MaskedTextBox maskedTextBox)
                    maskedTextBox.Clear();
            }
        }
    }
}
