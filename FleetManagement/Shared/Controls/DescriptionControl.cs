using System.Windows.Forms;

namespace FleetManagement
{
    public partial class DescriptionControl : UserControl
    {
        public DescriptionControl()
        {
            InitializeComponent();
        }

        public string Description
        {
            get
            {
                return TxtDescription.Text.Trim();
            }
        }
    }
}
