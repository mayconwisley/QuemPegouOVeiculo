using System;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class DriverReportForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        string search;

        public DriverReportForm()
        {
            InitializeComponent();
        }

        private void CbxListDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CbxListDriver.SelectedIndex)
            {
                case 0:
                    search = "%";
                    break;
                case 1:
                    search = "A";
                    break;
                case 2:
                    search = "D";
                    break;
                default:
                    break;
            }


        }
        private void BtnList_Click(object sender, EventArgs e)
        {

            ReportViewerForm reportViewer = new ReportViewerForm(search, 2);
            MdiForms.OpenReplacing(this, reportViewer);
        }

        private void DriverReportForm_Load(object sender, EventArgs e)
        {
            CbxListDriver.SelectedIndex = 0;
        }
    }
}
