using FleetManagement.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class LicenseExpirationForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public LicenseExpirationForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        LicenseExpirationModel expirationLicenseNumber = null;
        int idExpirationLicenseNumber = 0, driverId = 0;
        private void LoadRecords(string search)
        {
            try
            {
                DgvLicenseExpirations.DataSource = FleetManagement.Desktop.Client.Features.Operations.LicenseExpirations.Query.Register(search);
                LblExpiration.Text = "Vencimento - " + DgvLicenseExpirations.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Operations.LicenseExpirations.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvLicenseExpirations.DataSource = table;
                    LblExpiration.Text = "Vencimento - " + DgvLicenseExpirations.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private void HandleOperation(char operation)
        {
            expirationLicenseNumber = new LicenseExpirationModel();

            try
            {
                expirationLicenseNumber.Id = idExpirationLicenseNumber;
                expirationLicenseNumber.Driver = new DriverModel();
                expirationLicenseNumber.Driver.Id = DriverControl.Id;
                expirationLicenseNumber.Date = DateTime.Parse(MktDtExpiration.Text);

                if (CbExpired.Checked)
                {
                    expirationLicenseNumber.Status = 'V';
                }
                else
                {
                    expirationLicenseNumber.Status = 'N';
                }

                switch (operation)
                {
                    case 'I':
                        FleetManagement.Desktop.Client.Features.Operations.LicenseExpirations.Insert.Register(expirationLicenseNumber);
                        break;
                    case 'U':
                        FleetManagement.Desktop.Client.Features.Operations.LicenseExpirations.Update.Register(expirationLicenseNumber);
                        break;
                    case 'D':
                        FleetManagement.Desktop.Client.Features.Operations.LicenseExpirations.Delete.Register(expirationLicenseNumber);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                LoadRecords("%" + TxtSearch.Text.Trim() + "%");
                ClearFields.ClearMaskedTextBox(this.Controls);
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
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

        private void DgvLicenseExpirations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            idExpirationLicenseNumber = int.Parse(DgvLicenseExpirations.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            driverId = int.Parse(DgvLicenseExpirations.Rows[e.RowIndex].Cells["DriverId"].Value.ToString());
            MktDtExpiration.Text = DgvLicenseExpirations.Rows[e.RowIndex].Cells["Date"].Value.ToString();
            string status = DgvLicenseExpirations.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            DriverControl.CbxDriver.SelectedValue = driverId;
            if (status == "Vencido")
            {
                CbExpired.Checked = true;

            }
            else
            {
                CbExpired.Checked = false;
            }
            EditButtonState.SetEditMode(true, BtnEdit, BtnDelete, BtnSave);
        }

        private async void LicenseExpirationForm_Load(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%%");
        }
    }
}
