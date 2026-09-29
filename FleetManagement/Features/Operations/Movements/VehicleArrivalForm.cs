using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleArrivalForm : FleetManagement.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;
        int movementId;
        public VehicleArrivalForm()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        public void LoadRecords()
        {
            try
            {
                DgvVehicleMovements.DataSource = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterArrivalNull();
                lblInfo.Text = "Controle Veiculo - " + DgvVehicleMovements.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task LoadRecordsAsync() =>
            listLoader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Operations.Movements.Query
                    .RegisterArrivalNullAsync(cancellationToken),
                table =>
                {
                    DgvVehicleMovements.DataSource = table;
                    lblInfo.Text = "Controle Veiculo - " + DgvVehicleMovements.Rows.Count.ToString("000");
                });

        private async void VehicleArrivalForm_Load(object sender, EventArgs e)
        {
            await LoadRecordsAsync();
        }

        private void DgvVehicleMovements_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            movementId = int.Parse(DgvVehicleMovements.Rows[e.RowIndex].Cells["Id"].Value.ToString());



            VehicleArrivalCompactForm arrivalForm = new VehicleArrivalCompactForm(movementId, this);
            arrivalForm.MdiParent = MdiParent;
            arrivalForm.Show();
        }
    }
}
