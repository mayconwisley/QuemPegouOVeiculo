using FleetManagement.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class DriverForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public DriverForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        DriverModel driver = null;
        int driverId = 0;

        private void LoadRecords(string search)
        {
            try
            {
                DgvDrivers.DataSource = FleetManagement.Desktop.Client.Features.Registrations.Drivers.Query.Register(search);
                LblDriver.Text = "Motoristas - " + DgvDrivers.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Registrations.Drivers.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvDrivers.DataSource = table;
                    LblDriver.Text = "Motoristas - " + DgvDrivers.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private void HandleOperation(char operation)
        {
            driver = new DriverModel();

            try
            {
                if (operation == 'D')
                {
                    FleetManagement.Desktop.Client.Features.Registrations.Drivers.Delete.Register(new DriverModel { Id = driverId });
                    LoadRecords("%" + TxtSearch.Text.Trim() + "%");
                    EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);
                    return;
                }
                driver.Id = driverId;
                driver.Name = TxtName.Text.Trim();
                driver.LicenseNumber = TxtLicenseNumber.Text.Trim();

                /*Validar data de vencimento da CNH*/
                if (DateTime.Parse(MktLicenseExpiration.Text).Date >= DateTime.Now.Date)
                {
                    driver.LicenseExpiration = DateTime.Parse(MktLicenseExpiration.Text.Trim());
                }
                else
                {
                    if (MessageBox.Show("CNH Vencida.\n\nDeseja cadastrar mesmo assim?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        driver.LicenseExpiration = DateTime.Parse(MktLicenseExpiration.Text.Trim());
                    }
                    else
                    {
                        return;
                    }
                }

                driver.LicenseCategory = TxtLicenseCategory.Text.Trim();

                /*Validar CPF*/
                string cpf = MktCPF.Text.Replace(".", "").Replace("-", "").Trim();
                bool validateCPF = FleetManagement.Desktop.Client.Validation.CpfValidator.CPF(cpf);
                if (!validateCPF)
                {
                    MessageBox.Show("CPF inválido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                driver.CPF = cpf;

                driver.RG = TxtRG.Text.Trim();

                if (CbActive.Checked)
                {
                    driver.Active = 'A';
                }
                else
                {
                    driver.Active = 'D';
                }

                /*Insert, Update, Delete*/
                switch (operation)
                {
                    case 'I':
                        FleetManagement.Desktop.Client.Features.Registrations.Drivers.Insert.Register(driver);
                        break;
                    case 'U':
                        FleetManagement.Desktop.Client.Features.Registrations.Drivers.Update.Register(driver);
                        break;
                    case 'D':
                        FleetManagement.Desktop.Client.Features.Registrations.Drivers.Delete.Register(driver);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                /*Listar registros após cada Insert, Update, Delete*/
                LoadRecords("%" + TxtSearch.Text.Trim() + "%");

                /*Limpar os campos*/
                ClearFields.ClearTextBox(this.Controls);
                ClearFields.ClearMaskedTextBox(this.Controls);

                /*Librar BtnGravar, e desativar os BtnAlterar, BtnExcluir*/
                EditButtonState.SetEditMode(false, BtnEdit, BtnDelete, BtnSave);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DriverForm_Load(object sender, EventArgs e)
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

        private void DgvDrivers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            driverId = int.Parse(DgvDrivers.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            TxtName.Text = DgvDrivers.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            TxtLicenseNumber.Text = DgvDrivers.Rows[e.RowIndex].Cells["LicenseNumber"].Value.ToString();
            MktLicenseExpiration.Text = DgvDrivers.Rows[e.RowIndex].Cells["LicenseExpiration"].Value.ToString();
            TxtLicenseCategory.Text = DgvDrivers.Rows[e.RowIndex].Cells["LicenseCategory"].Value.ToString();
            MktCPF.Text = DgvDrivers.Rows[e.RowIndex].Cells["CPF"].Value.ToString();
            TxtRG.Text = DgvDrivers.Rows[e.RowIndex].Cells["RG"].Value.ToString();

            string active = DgvDrivers.Rows[e.RowIndex].Cells["Active"].Value.ToString();
            if (active.Trim() == "Ativo")
            {
                CbActive.Checked = true;
            }
            else
            {
                CbActive.Checked = false;
            }
            /*Liberar os BtnAlterar e BtnExcluir. Desativar o BtnGravar*/
            EditButtonState.SetEditMode(true, BtnEdit, BtnDelete, BtnSave);
        }

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadRecordsAsync("%" + TxtSearch.Text.Trim() + "%", 300);
        }
    }
}
