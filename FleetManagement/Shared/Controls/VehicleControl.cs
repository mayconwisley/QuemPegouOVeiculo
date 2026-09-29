using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleControl : UserControl
    {
        private readonly AsyncDataLoader<DataTable> loader;

        public VehicleControl()
        {
            InitializeComponent();
            loader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => loader.Dispose();
        }

        #region Retornar os amountes selecionado no ComboBox Vehicles
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        private string model;
        public string Model
        {
            get
            {
                return model;
            }
        }
        #endregion

        private async void UCVehicle_Load(object sender, EventArgs e)
        {
            await loader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Query
                    .IdAndModelActiveAsync(cancellationToken),
                table => CbxVehicle.DataSource = table);
        }

        private void CbxVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxVehicle.SelectedValue != null && int.TryParse(CbxVehicle.SelectedValue.ToString(), out id))
            {
                model = CbxVehicle.Text.ToString();
            }
            else
            {
                return;
            }

        }
    }
}
