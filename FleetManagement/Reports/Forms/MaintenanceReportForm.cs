using System;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class MaintenanceReportForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        public MaintenanceReportForm()
        {
            InitializeComponent();
        }
        DateTime startDate, endDate;

        private void BtnList_Click(object sender, EventArgs e)
        {
            ListReport();
        }

        private void RbGeral_CheckedChanged(object sender, EventArgs e)
        {
            if (RbGeral.Checked)
            {
                GbPeriod.Enabled = false;
                GbVehicle.Enabled = false;
            }
        }

        private void RbVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (RbVehicle.Checked)
            {
                GbPeriod.Enabled = false;
                GbVehicle.Enabled = true;
            }
        }

        private void RbPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPeriod.Checked)
            {
                GbPeriod.Enabled = true;
                GbVehicle.Enabled = false;
            }
        }

        private void ListReport()
        {
            ReportViewerForm reportViewer = null;

            try
            {
                if (RbGeral.Checked)
                {
                    reportViewer = new ReportViewerForm("ALL", 5);

                }
                if (RbVehicle.Checked)
                {
                    int vehicleId = VehicleControl.Id;
                    reportViewer = new ReportViewerForm("VEI", 5, vehicleId);
                }
                if (RbPeriod.Checked)
                {

                    if (MktStartDate.Text == "  /  /")
                    {
                        MessageBox.Show("Inserir uma data de inicio.", "Aviso");
                        return;
                    }

                    if (MktEndDate.Text == "  /  /")
                    {
                        MktEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    DateTime.TryParse(MktStartDate.Text, out startDate);
                    DateTime.TryParse(MktEndDate.Text, out endDate);

                    if (RbPeriod.Checked)
                    {
                        reportViewer = new ReportViewerForm("DTPer", 5, startDate, endDate);
                    }
                }

                MdiForms.OpenReplacing(this, reportViewer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
