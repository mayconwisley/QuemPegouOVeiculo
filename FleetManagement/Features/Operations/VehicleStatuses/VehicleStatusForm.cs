using FleetManagement.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleStatusForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public VehicleStatusForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }

        VehicleStatusModel statusVehicle;
        int idStatus = 0, vehicleId = 0;
        DateTime startAt, endAt;

        private void LoadRecords(string search)
        {
            try
            {
                DgvVehicle.DataSource = FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Query.Register(search);
                LblStatus.Text = "Status - " + DgvVehicle.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvVehicle.DataSource = table;
                    LblStatus.Text = "Status - " + DgvVehicle.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private void HandleOperation(char operation)
        {
            try
            {
                DateTime.TryParse(MktStartAt.Text, out startAt);
                DateTime.TryParse(MktEndAt.Text, out endAt);

                statusVehicle = new VehicleStatusModel();
                statusVehicle.Id = idStatus;
                statusVehicle.Vehicle = new VehicleModel();
                statusVehicle.Vehicle.Id = VehicleControl.Id;


                if (startAt.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    statusVehicle.StartAt = null;
                }
                else
                {
                    statusVehicle.StartAt = startAt;
                }



                if (endAt.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    statusVehicle.EndAt = null;
                }
                else
                {
                    statusVehicle.EndAt = endAt;
                }

                statusVehicle.Description = DescriptionControl.Description;

                switch (operation)
                {
                    case 'I':
                        FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Insert.Register(statusVehicle);
                        break;
                    case 'U':
                        FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Update.Register(statusVehicle);
                        break;
                    case 'D':
                        FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Delete.Register(statusVehicle);
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LnkCurrentStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktStartAt.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void DgvVehicle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            idStatus = int.Parse(DgvVehicle.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            vehicleId = int.Parse(DgvVehicle.Rows[e.RowIndex].Cells["VehicleId"].Value.ToString());


            DateTime.TryParse(DgvVehicle.Rows[e.RowIndex].Cells["StartAt"].Value.ToString(), out startAt);
            DateTime.TryParse(DgvVehicle.Rows[e.RowIndex].Cells["EndAt"].Value.ToString(), out endAt);

            if (endAt.Date == DateTime.Parse("01/01/0001 00:00:00"))
            {
                MktEndAt.Text = "";
            }
            else
            {
                MktEndAt.Text = endAt.ToString("dd/MM/yyyy HH:mm");
            }

            MktStartAt.Text = startAt.ToString("dd/MM/yyyy HH:mm");
            DescriptionControl.TxtDescription.Text = DgvVehicle.Rows[e.RowIndex].Cells["Description"].Value.ToString();
            VehicleControl.CbxVehicle.SelectedValue = vehicleId;

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

        private async void VehicleStatusForm_Load(object sender, EventArgs e)
        {
            MktStartAt.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            await LoadRecordsAsync("%%");
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
        }

        private void LnkCurrentEnd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktEndAt.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
