using System.Windows.Forms;

namespace FleetManagement.Shared.Presentation
{
    public static class EditButtonState
    {
        public static void SetEditMode(bool emEdicao, Button edit, Button delete, Button save)
        {
            edit.Enabled = emEdicao;
            delete.Enabled = emEdicao;
            save.Enabled = !emEdicao;
        }
    }
}
