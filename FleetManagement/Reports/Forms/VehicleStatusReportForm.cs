using System;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleStatusReportForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        DateTime startDate, endDate;
        public VehicleStatusReportForm()
        {
            InitializeComponent();
        }

        private void ListReport()
        {
            ReportViewerForm reportViewer = null;

            try
            {
                if (RbTodos.Checked)
                {
                    reportViewer = new ReportViewerForm("ALL", 3);

                }
                if (RbVehicle.Checked)
                {
                    int vehicleId = VehicleControl.Id;
                    reportViewer = new ReportViewerForm("VEI", 3, vehicleId);
                }
                if (RbPeriod.Checked)
                {

                    if (MktStartDate.Text == "  /  /" && CbOpenOnly.Checked == false)
                    {
                        MessageBox.Show("Inserir uma data de inicio.", "Aviso");
                        return;
                    }

                    if (MktEndDate.Text == "  /  /" && CbOpenOnly.Checked == false)
                    {
                        MktEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    DateTime.TryParse(MktStartDate.Text, out startDate);
                    DateTime.TryParse(MktEndDate.Text, out endDate);

                    if (RbStartDate.Checked && CbOpenOnly.Checked == false)
                    {
                        reportViewer = new ReportViewerForm("DTIni", 3, startDate, endDate);
                    }
                    else if (RbEndDate.Checked && CbOpenOnly.Checked == false)
                    {
                        reportViewer = new ReportViewerForm("DTFin", 3, startDate, endDate);
                    }
                    else if (CbOpenOnly.Checked)
                    {
                        reportViewer = new ReportViewerForm("NULL", 3);
                    }
                }

                MdiForms.OpenReplacing(this, reportViewer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RbEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (RbEndDate.Checked)
            {
                CbOpenOnly.Visible = true;

            }
        }

        private void RbStartDate_CheckedChanged(object sender, EventArgs e)
        {
            if (RbStartDate.Checked)
            {
                CbOpenOnly.Visible = false;
            }
        }

        private void RbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTodos.Checked)
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

        private void BtnList_Click(object sender, EventArgs e)
        {
            ListReport();
        }
    }
}
