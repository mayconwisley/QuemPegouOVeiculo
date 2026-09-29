using System;
using System.Windows.Forms;

namespace FleetManagement.Shared.Presentation
{
    internal static class MdiForms
    {
        internal static void OpenReplacing(Form sourceForm, Form newForm)
        {
            if (newForm == null)
                throw new ArgumentNullException(nameof(newForm));

            var mainForm = sourceForm.MdiParent
                ?? throw new InvalidOperationException("A janela principal não está disponível.");

            foreach (Form openForm in mainForm.MdiChildren)
            {
                if (openForm.GetType() == newForm.GetType())
                {
                    openForm.Close();
                    break;
                }
            }

            newForm.MdiParent = mainForm;
            newForm.Show();
        }
    }
}
