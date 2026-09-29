using FleetManagement.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class MaintenanceForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public MaintenanceForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        MaintenanceModel maintenance;
        int maintenanceId = 0, vehicleId = 0;
        private void LoadRecords(string search)
        {
            try
            {
                DgvMaintenance.DataSource = FleetManagement.Desktop.Client.Features.Operations.Maintenance.Query.Register(search);
                LblMaintenance.Text = "Manutenção - " + DgvMaintenance.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Operations.Maintenance.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvMaintenance.DataSource = table;
                    LblMaintenance.Text = "Manutenção - " + DgvMaintenance.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private void HandleOperation(char operation)
        {
            maintenance = new MaintenanceModel();

            try
            {
                maintenance.Id = maintenanceId;
                maintenance.Vehicle = new VehicleModel();
                maintenance.Vehicle.Id = VehicleControl.Id;
                maintenance.Date = DateTime.Parse(MktDate.Text.Trim());
                maintenance.Amount = AmountControl.Amount;
                maintenance.Description = DescriptionControl.Description;

                switch (operation)
                {
                    case 'I':
                        FleetManagement.Desktop.Client.Features.Operations.Maintenance.Insert.Register(maintenance);
                        break;
                    case 'U':
                        FleetManagement.Desktop.Client.Features.Operations.Maintenance.Update.Register(maintenance);
                        break;
                    case 'D':
                        FleetManagement.Desktop.Client.Features.Operations.Maintenance.Delete.Register(maintenance);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                LoadRecords("%" + TxtSearch.Text.Trim() + "%");
                ClearFields.ClearTextBox(this.Controls);
                ClearFields.ClearMaskedTextBox(this.Controls);
                DescriptionControl.TxtDescription.Clear();
                AmountControl.TxtAmount.Text = "0,00";
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void MaintenanceForm_Load(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%%");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            HandleOperation('I');
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            HandleOperation('U');
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            HandleOperation('D');
        }

        private void DgvMaintenance_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            maintenanceId = int.Parse(DgvMaintenance.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            vehicleId = int.Parse(DgvMaintenance.Rows[e.RowIndex].Cells["VehicleId"].Value.ToString());
            VehicleControl.CbxVehicle.SelectedValue = vehicleId;

            if (VehicleControl.CbxVehicle.SelectedValue != null)
            {
                VehicleControl.CbxVehicle.SelectedValue = vehicleId;
            }
            else
            {
                ClearFields.ClearTextBox(this.Controls);
                ClearFields.ClearMaskedTextBox(this.Controls);
                DescriptionControl.TxtDescription.Clear();
                AmountControl.TxtAmount.Text = "0,00";
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);
                return;
            }


            DescriptionControl.TxtDescription.Text = DgvMaintenance.Rows[e.RowIndex].Cells["Description"].Value.ToString();
            AmountControl.TxtAmount.Text = DgvMaintenance.Rows[e.RowIndex].Cells["Amount"].Value.ToString();


            MktDate.Text = DgvMaintenance.Rows[e.RowIndex].Cells["Date"].Value.ToString();

            EditButtonState.SetEditMode(true, BtnEdit, BtnDelete, BtnSave);

        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
        }
    }
}
