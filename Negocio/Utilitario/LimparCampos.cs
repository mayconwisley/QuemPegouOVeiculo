using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Negocio.Utilitario
{
    public static class LimparCampos
    {
        public static void LimparTextBox(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)(control)).Clear();
                }
            }
        }

        public static void LimparMaskedTextBox(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is MaskedTextBox)
                {
                    ((MaskedTextBox)(control)).Clear();
                }
            }
        }

        public static void LimparSelecaoTextBox(params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }
        }

        public static void LimparSelecaoMaskedTextBox(params MaskedTextBox[] maskedTextBoxes)
        {
            foreach (MaskedTextBox maskedTextBox in maskedTextBoxes)
            {
                maskedTextBox.Clear();
            }
        }


        public static void LimparSelecaoUserControl(params UserControl[] userControls)
        {
            foreach (UserControl userControl in userControls)
            {
                if(userControl.Controls.Owner.Name == "UCDescricao")
                {
                    userControl.Controls.Owner.AccessibilityObject.Value = "TxtDescricao";
                }
            }
        }
    }
}
