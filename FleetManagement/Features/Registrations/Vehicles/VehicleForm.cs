using FleetManagement.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public VehicleForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        VehicleModel vehicle;
        int vehicleId = 0;

        private void LoadRecords(string search)
        {
            try
            {
                DgvVehicle.DataSource = FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Query.Register(search);
                LblVehicles.Text = "Veiculos - " + DgvVehicle.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvVehicle.DataSource = table;
                    LblVehicles.Text = "Veiculos - " + DgvVehicle.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private void HandleOperation(char operation)
        {
            vehicle = new VehicleModel();

            try
            {
                vehicle.Id = vehicleId;
                vehicle.Plate = TxtPlate.Text.Trim();
                vehicle.Model = TxtModel.Text.Trim();
                vehicle.Chassis = TxtChassis.Text.Trim();
                vehicle.Renavam = TxtRenavam.Text.Trim();

                if (CbActive.Checked)
                {
                    vehicle.Status = 'A';
                }
                else
                {
                    vehicle.Status = 'D';
                }

                switch (operation)
                {
                    case 'I':
                        FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Insert.Register(vehicle);
                        break;
                    case 'U':
                        FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Update.Register(vehicle);
                        break;
                    case 'D':
                        FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Delete.Register(vehicle);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                LoadRecords("%" + TxtSearch.Text.Trim() + "%");
                ClearFields.ClearTextBox(this.Controls);
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void VehicleForm_Load(object sender, EventArgs e)
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

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
        }

        private void DgvVehicle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            vehicleId = int.Parse(DgvVehicle.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            TxtPlate.Text = DgvVehicle.Rows[e.RowIndex].Cells["Plate"].Value.ToString();
            TxtModel.Text = DgvVehicle.Rows[e.RowIndex].Cells["Model"].Value.ToString();
            TxtChassis.Text = DgvVehicle.Rows[e.RowIndex].Cells["Chassis"].Value.ToString();
            TxtRenavam.Text = DgvVehicle.Rows[e.RowIndex].Cells["Renavam"].Value.ToString();
            string status = DgvVehicle.Rows[e.RowIndex].Cells["Status"].Value.ToString();

            if (status.Trim() == "Ativo")
            {
                CbActive.Checked = true;
            }
            else
            {
                CbActive.Checked = false;
            }
            EditButtonState.SetEditMode(true, BtnEdit, BtnDelete, BtnSave);
        }
    }
}
