using System.ComponentModel;
using System.Windows.Forms;

namespace QuemPegouOVeiculo.Shared.Presentation
{
    public class ThemedForm : Form
    {
        private bool _themeApplied;

        protected override void OnLoad(System.EventArgs e)
        {
            if (!_themeApplied && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _themeApplied = true;
                DesktopTheme.Apply(this);
            }

            base.OnLoad(e);
        }
    }
}
