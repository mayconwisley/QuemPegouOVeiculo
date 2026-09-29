using System;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleMovementReportForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        public VehicleMovementReportForm()
        {
            InitializeComponent();
        }
        DateTime startDate, endDate;
        int vehicleId = 0, driverId = 0;

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
                    reportViewer = new ReportViewerForm("ALL", 4);
                }
                if (RbVehicle.Checked)
                {
                    reportViewer = new ReportViewerForm("VEI", 4, vehicleId);
                }
                if (RbDriver.Checked)
                {
                    reportViewer = new ReportViewerForm("MOT", 4, driverId);
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

                    if (RbDepartureDate.Checked && CbOpenOnly.Checked == false && CbVehicle.Checked == false && CbDriver.Checked == false)
                    {
                        reportViewer = new ReportViewerForm("DTSai", 4, startDate, endDate);
                    }
                    else if (RbArrivalDate.Checked && CbOpenOnly.Checked == false && CbVehicle.Checked == false && CbDriver.Checked == false)
                    {
                        reportViewer = new ReportViewerForm("DTChe", 4, startDate, endDate);
                    }
                    else if (RbDepartureDate.Checked && CbVehicle.Checked == true)
                    {
                        reportViewer = new ReportViewerForm("DTSaiVei", 4, startDate, endDate, vehicleId);
                    }
                    else if (RbArrivalDate.Checked && CbVehicle.Checked == true)
                    {
                        reportViewer = new ReportViewerForm("DTCheVei", 4, startDate, endDate, vehicleId);
                    }
                    else if (RbDepartureDate.Checked && CbDriver.Checked == true)
                    {
                        reportViewer = new ReportViewerForm("DTSaiMot", 4, startDate, endDate, driverId);
                    }
                    else if (RbArrivalDate.Checked && CbDriver.Checked == true)
                    {
                        reportViewer = new ReportViewerForm("DTCheMot", 4, startDate, endDate, driverId);
                    }
                    else if (CbOpenOnly.Checked)
                    {
                        reportViewer = new ReportViewerForm("NULL", 4);
                    }
                }
                if (RbVeiMot.Checked)
                {
                    reportViewer = new ReportViewerForm("VEIMOT", 4, vehicleId, driverId);
                }

                MdiForms.OpenReplacing(this, reportViewer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnList_Click(object sender, EventArgs e)
        {
            ListReport();
        }
    }
}
