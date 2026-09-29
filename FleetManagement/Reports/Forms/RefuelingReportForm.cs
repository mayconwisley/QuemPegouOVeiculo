using System;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class RefuelingReportForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        public RefuelingReportForm()
        {
            InitializeComponent();
        }

        DateTime startDate, endDate;
        int vehicleId = 0, driverId = 0;

        private void RbGeral_CheckedChanged(object sender, EventArgs e)
        {
            GbDriver.Enabled = false;
            GbPeriod.Enabled = false;
            GbVehicle.Enabled = false;
        }

        private void RbVehicle_CheckedChanged(object sender, EventArgs e)
        {
            GbDriver.Enabled = false;
            GbPeriod.Enabled = false;
            GbVehicle.Enabled = true;
        }

        private void RbDriver_CheckedChanged(object sender, EventArgs e)
        {
            GbDriver.Enabled = true;
            GbPeriod.Enabled = false;
            GbVehicle.Enabled = false;
        }

        private void RbPeriod_CheckedChanged(object sender, EventArgs e)
        {
            GbDriver.Enabled = false;
            GbPeriod.Enabled = true; ;
            GbVehicle.Enabled = false;
        }

        private void RbVeiMot_CheckedChanged(object sender, EventArgs e)
        {
            GbDriver.Enabled = true;
            GbPeriod.Enabled = false;
            GbVehicle.Enabled = true;
        }

        private void CbVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVehicle.Checked)
            {
                CbDriver.Enabled = false;
                GbVehicle.Enabled = true;
            }
            else
            {
                CbDriver.Enabled = true;
                GbVehicle.Enabled = false;
            }
        }

        private void CbDriver_CheckedChanged(object sender, EventArgs e)
        {
            if (CbDriver.Checked)
            {
                CbVehicle.Enabled = false;
                GbDriver.Enabled = true;
            }
            else
            {
                CbVehicle.Enabled = true;
                GbDriver.Enabled = false;
            }
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            ListReport();
        }

        private void ListReport()
        {
            ReportViewerForm reportViewer = null;
            vehicleId = VehicleControl.Id;
            driverId = DriverControl.Id;

            /* ALL = Todos, VEI = Veiculos, MOT = Motoristas
             * DTSai = Data Saida, DTChe = Data chegada
             * DTSaiVei = Data Saída e Veiculo, DTCheVei = Data Chegada e Veiculo
             * DTSaiMot = Data Saída e Motorista, DTCheMot = Data Chegada e Motorista
             * NULL = Nulo, VEIMOT = Veiculo e Motorista
             */
            try
            {
                if (RbGeral.Checked)
                {
                    reportViewer = new ReportViewerForm("ALL", 6);
                }
                if (RbVehicle.Checked)
                {
                    reportViewer = new ReportViewerForm("VEI", 6, vehicleId);
                }
                if (RbDriver.Checked)
                {
                    reportViewer = new ReportViewerForm("MOT", 6, driverId);
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

                    if (RbPeriod.Checked && CbVehicle.Checked == false && CbDriver.Checked == false)
                    {
                        reportViewer = new ReportViewerForm("DTPer", 6, startDate, endDate);
                    }
                    else if (RbPeriod.Checked && CbVehicle.Checked == true)
                    {
                        reportViewer = new ReportViewerForm("DTPerVei", 6, startDate, endDate, vehicleId);
                    }

                    else if (RbPeriod.Checked && CbDriver.Checked == true)
                    {
                        reportViewer = new ReportViewerForm("DTPerMot", 6, startDate, endDate, driverId);
                    }
                }
                if (RbVeiMot.Checked)
                {
                    reportViewer = new ReportViewerForm("VEIMOT", 6, vehicleId, driverId);
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
