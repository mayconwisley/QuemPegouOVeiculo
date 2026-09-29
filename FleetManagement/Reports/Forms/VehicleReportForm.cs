using System;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleReportForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        public VehicleReportForm()
        {
            InitializeComponent();
        }
        string search;
        private void BtnList_Click(object sender, EventArgs e)
        {


            ReportViewerForm reportViewer = new ReportViewerForm(search, 1);
            MdiForms.OpenReplacing(this, reportViewer);
        }

        private void CbxListVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CbxListVehicle.SelectedIndex)
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

        private void VehicleReportForm_Load(object sender, EventArgs e)
        {
            CbxListVehicle.SelectedIndex = 0;
        }
    }
}
