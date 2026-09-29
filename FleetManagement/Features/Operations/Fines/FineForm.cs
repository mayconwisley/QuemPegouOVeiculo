using FleetManagement.Desktop.Models;
using FleetManagement.Shared.Presentation;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManagement
{
	public partial class FineForm : ThemedForm
	{
		private readonly AsyncDataLoader<DataTable> listLoader;

		public FineForm()
		{
			InitializeComponent();
			listLoader = new AsyncDataLoader<DataTable>(this);
			Disposed += (sender, args) => listLoader.Dispose();
		}
		FineModel fine;
		int fineId = 0, vehicleId = 0, driverId = 0;

		private async void TxtSearch_TextChanged(object sender, EventArgs e)
		{
			await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
		}

		private void LoadRecords(string search)
		{
			try
			{
				DgvFines.DataSource = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.Register(search);
				LblFines.Text = "Multas - " + DgvFines.Rows.Count.ToString("000");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
			listLoader.LoadAsync(
				cancellationToken => FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterAsync(search, cancellationToken),
				table =>
				{
					DgvFines.DataSource = table;
					LblFines.Text = "Multas - " + DgvFines.Rows.Count.ToString("000");
				},
				delayMilliseconds);

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

		private void DgvFines_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			fineId = int.Parse(DgvFines.Rows[e.RowIndex].Cells["Id"].Value.ToString());
			vehicleId = int.Parse(DgvFines.Rows[e.RowIndex].Cells["VehicleId"].Value.ToString());
			driverId = int.Parse(DgvFines.Rows[e.RowIndex].Cells["DriverId"].Value.ToString());

			VehicleDriverSelector.CbxVehicle.SelectedValue = vehicleId;
			VehicleDriverSelector.CbxDriver.SelectedValue = driverId;

			if (VehicleDriverSelector.CbxVehicle.SelectedValue != null)
			{
				VehicleDriverSelector.CbxVehicle.SelectedValue = vehicleId;
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

			if (VehicleDriverSelector.CbxDriver.SelectedValue != null)
			{
				VehicleDriverSelector.CbxDriver.SelectedValue = driverId;
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



			DescriptionControl.TxtDescription.Text = DgvFines.Rows[e.RowIndex].Cells["Description"].Value.ToString();
			AmountControl.TxtAmount.Text = decimal.Parse(DgvFines.Rows[e.RowIndex].Cells["Amount"].Value.ToString()).ToString("#,##0.00");

			MktDate.Text = DgvFines.Rows[e.RowIndex].Cells["Date"].Value.ToString();
			TxtPoints.Text = DgvFines.Rows[e.RowIndex].Cells["Points"].Value.ToString();

			EditButtonState.SetEditMode(true, BtnEdit, BtnDelete, BtnSave);
		}

		private void HandleOperation(char operation)
		{
			fine = new FineModel();

			try
			{
				fine.Id = fineId;
				fine.Vehicle = new VehicleModel();
				fine.Vehicle.Id = VehicleDriverSelector.VehicleId;
				fine.Driver = new DriverModel();
				fine.Driver.Id = VehicleDriverSelector.DriverId;

				fine.Description = DescriptionControl.Description;
				fine.Amount = AmountControl.Amount;

				fine.Date = DateTime.Parse(MktDate.Text.Trim());
				fine.Points = int.Parse(TxtPoints.Text.Trim());

				switch (operation)
				{
					case 'I':
						FleetManagement.Desktop.Client.Features.Operations.Fines.Insert.Register(fine);
						break;
					case 'U':
						FleetManagement.Desktop.Client.Features.Operations.Fines.Update.Register(fine);
						break;
					case 'D':
						FleetManagement.Desktop.Client.Features.Operations.Fines.Delete.Register(fine);
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

		private async void FineForm_Load(object sender, EventArgs e)
		{
			await LoadRecordsAsync("%%");
		}
	}
}
