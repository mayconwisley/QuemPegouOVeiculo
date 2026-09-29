using FleetManagement.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleMovementForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public VehicleMovementForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        VehicleMovementModel movement;
        int movementId = 0, vehicleId = 0, driverId = 0;

        DateTime departureAt, arrivalAt;

        private void LoadRecords(string search)
        {
            try
            {
                DgvVehicleMovements.DataSource = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.Register(search);
                LblVehicleMovementCount.Text = "Controle Veiculo - " + DgvVehicleMovements.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvVehicleMovements.DataSource = table;
                    LblVehicleMovementCount.Text = "Controle Veiculo - " + DgvVehicleMovements.Rows.Count.ToString("000");
                },
                delayMilliseconds);
        private void BtnSave_Click(object sender, EventArgs e)
        {
            Manipupate('I');
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Manipupate('U');
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Manipupate('D');
        }

        private void DgvVehicleMovements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            movementId = int.Parse(DgvVehicleMovements.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            vehicleId = int.Parse(DgvVehicleMovements.Rows[e.RowIndex].Cells["VehicleId"].Value.ToString());
            driverId = int.Parse(DgvVehicleMovements.Rows[e.RowIndex].Cells["DriverId"].Value.ToString());

            /*Verificar se os Veiculos ou/e motoristas estão ativos*/
            VehicleDriverSelector.CbxVehicle.SelectedValue = vehicleId;
            VehicleDriverSelector.CbxDriver.SelectedValue = driverId;

            /*Veiculo*/
            if (VehicleDriverSelector.CbxVehicle.SelectedValue != null)
            {
                VehicleDriverSelector.CbxVehicle.SelectedValue = vehicleId;
            }
            else
            {
                ClearFields.ClearTextBox(this.Controls);
                ClearFields.ClearMaskedTextBox(this.Controls);
                DescriptionControl.TxtDescription.Clear();
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);
                return;
            }
            /*Motorista*/
            if (VehicleDriverSelector.CbxDriver.SelectedValue != null)
            {
                VehicleDriverSelector.CbxDriver.SelectedValue = driverId;
            }
            else
            {
                ClearFields.ClearTextBox(this.Controls);
                ClearFields.ClearMaskedTextBox(this.Controls);
                DescriptionControl.TxtDescription.Clear();
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);
                return;
            }

            DateTime.TryParse(DgvVehicleMovements.Rows[e.RowIndex].Cells["DepartureAt"].Value.ToString(), out departureAt);
            DateTime.TryParse(DgvVehicleMovements.Rows[e.RowIndex].Cells["ArrivalAt"].Value.ToString(), out arrivalAt);


            if (arrivalAt.Date == DateTime.Parse("01/01/0001 00:00:00"))
            {
                MktDtArrival.Clear();
            }
            else
            {
                MktDtArrival.Text = arrivalAt.ToString("dd/MM/yyyy HH:mm");
            }

            /*Travar edição para chegada na atualização*/
            MktDtDeparture.Enabled = false;
            LkLblDtDepartureCurrent.Enabled = false;

            MktDtDeparture.Text = departureAt.ToString("dd/MM/yyyy HH:mm");

            DescriptionControl.TxtDescription.Text = DgvVehicleMovements.Rows[e.RowIndex].Cells["Description"].Value.ToString();


            TxtInitialMileage.Text = DgvVehicleMovements.Rows[e.RowIndex].Cells["InitialMileage"].Value.ToString();
            TxtFinalMileage.Text = DgvVehicleMovements.Rows[e.RowIndex].Cells["FinalMileage"].Value.ToString();

            EditButtonState.SetEditMode(true, BtnEdit, BtnDelete, BtnSave);
        }

        private void Manipupate(char operation)
        {
            movement = new VehicleMovementModel();
            try
            {
                DateTime.TryParse(MktDtDeparture.Text.Trim(), out departureAt);
                DateTime.TryParse(MktDtArrival.Text.Trim(), out arrivalAt);


                movement.Id = movementId;
                movement.Vehicle = new VehicleModel();
                movement.Vehicle.Id = VehicleDriverSelector.VehicleId;
                movement.Driver = new DriverModel();
                movement.Driver.Id = VehicleDriverSelector.DriverId;


                if (departureAt.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    movement.DepartureAt = null;
                }
                else
                {
                    movement.DepartureAt = departureAt;
                }


                if (arrivalAt.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    movement.ArrivalAt = null;
                }
                else
                {
                    movement.ArrivalAt = arrivalAt;
                }

                movement.Description = DescriptionControl.Description;
                movement.InitialMileage = TxtInitialMileage.Text.Trim();
                movement.FinalMileage = TxtFinalMileage.Text.Trim();

                switch (operation)
                {
                    case 'I':
                        movement.Status = 'S';
                        FleetManagement.Desktop.Client.Features.Operations.Movements.Insert.Register(movement);
                        break;
                    case 'U':
                        movement.Status = 'C';
                        FleetManagement.Desktop.Client.Features.Operations.Movements.Update.Register(movement);
                        break;
                    case 'D':
                        FleetManagement.Desktop.Client.Features.Operations.Movements.Delete.Register(movement);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                LoadRecords("%" + TxtSearch.Text.Trim() + "%");
                ClearFields.ClearTextBox(this.Controls);
                ClearFields.ClearMaskedTextBox(this.Controls);
                DescriptionControl.TxtDescription.Clear();

                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);

                /*Liberar edição*/
                MktDtDeparture.Enabled = true;
                LkLblDtDepartureCurrent.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LkLblDtDepartureCurrent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktDtDeparture.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void VehicleDriverSelector_FinalMileage(string finalMileage)
        {
            TxtInitialMileage.Text = finalMileage;
        }


        private void LkLblDtArrivalCurrent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktDtArrival.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private async void VehicleMovementForm_Load(object sender, EventArgs e)
        {
            MktDtDeparture.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            await LoadRecordsAsync("%%");
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
        }
    }
}
