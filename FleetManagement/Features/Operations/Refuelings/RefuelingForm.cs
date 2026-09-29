using FleetManagement.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class RefuelingForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public RefuelingForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        RefuelingModel refueling = null;
        int refuelingId = 0, vehicleId = 0, driverId = 0;

        private void LoadRecords(string search)
        {
            try
            {
                DgvRefueling.DataSource = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.Register(search);
                LblRefueling.Text = "Abastecimento - " + DgvRefueling.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvRefueling.DataSource = table;
                    LblRefueling.Text = "Abastecimento - " + DgvRefueling.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
        }

        private void DgvRefueling_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            refuelingId = int.Parse(DgvRefueling.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            vehicleId = int.Parse(DgvRefueling.Rows[e.RowIndex].Cells["VehicleId"].Value.ToString());
            driverId = int.Parse(DgvRefueling.Rows[e.RowIndex].Cells["DriverId"].Value.ToString());

            VehicleDriverSelector.CbxDriver.SelectedValue = driverId;
            VehicleDriverSelector.CbxVehicle.SelectedValue = vehicleId;

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

            DescriptionControl.TxtDescription.Text = DgvRefueling.Rows[e.RowIndex].Cells["Description"].Value.ToString();
            AmountControl.TxtAmount.Text = decimal.Parse(DgvRefueling.Rows[e.RowIndex].Cells["Amount"].Value.ToString()).ToString("#,##0.00");
            TxtLiters.Text = decimal.Parse(DgvRefueling.Rows[e.RowIndex].Cells["Liters"].Value.ToString()).ToString("#,##0.00");
            MktDate.Text = DgvRefueling.Rows[e.RowIndex].Cells["Date"].Value.ToString();
            TxtInitialMileage.Text = DgvRefueling.Rows[e.RowIndex].Cells["InitialMileage"].Value.ToString();
            EditButtonState.SetEditMode(true, BtnEdit, BtnDelete, BtnSave);
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

        private void VehicleDriverSelector_FinalMileage(string finalMileage)
        {
            //TxtKmInicial.Text = kmfinal;
        }

        private void TxtLiters_TextChanged(object sender, EventArgs e)
        {
            TxtLiters.Text = AmountFormatter.Amount(TxtLiters.Text.Trim());
            TxtLiters.Select(TxtLiters.Text.Length, 0);
        }

        private void TxtLiters_Leave(object sender, EventArgs e)
        {
            TxtLiters.Text = AmountFormatter.Zero(TxtLiters.Text.Trim());
            TxtLiters.Text = AmountFormatter.ParaAmount(TxtLiters.Text.Trim());
        }

        private void TxtLiters_Enter(object sender, EventArgs e)
        {
            if (TxtLiters.Text == "0,00")
            {
                TxtLiters.Clear();
            }
        }

        private void HandleOperation(char operation)
        {
            refueling = new RefuelingModel();
            try
            {
                refueling.Id = refuelingId;
                refueling.Vehicle = new VehicleModel();
                refueling.Vehicle.Id = VehicleDriverSelector.VehicleId;
                refueling.Driver = new DriverModel();
                refueling.Driver.Id = VehicleDriverSelector.DriverId;
                refueling.InitialMileage = TxtInitialMileage.Text.Trim();
                refueling.Date = DateTime.Parse(MktDate.Text.Trim());
                refueling.Amount = AmountControl.Amount;
                refueling.Liters = decimal.Parse(TxtLiters.Text.Trim());
                refueling.Description = DescriptionControl.Description;

                switch (operation)
                {
                    case 'I':
                        FleetManagement.Desktop.Client.Features.Operations.Refuelings.Insert.Register(refueling);
                        break;
                    case 'U':
                        FleetManagement.Desktop.Client.Features.Operations.Refuelings.Update.Register(refueling);
                        break;
                    case 'D':
                        FleetManagement.Desktop.Client.Features.Operations.Refuelings.Delete.Register(refueling);
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
                TxtLiters.Text = "0,00";
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void RefuelingForm_Load(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%%");
        }
    }
}
