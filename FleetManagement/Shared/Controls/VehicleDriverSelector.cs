using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class VehicleDriverSelector : UserControl
    {
        private readonly AsyncDataLoader<DataTable> vehiclesLoader;
        private readonly AsyncDataLoader<DataTable> driversLoader;
        private readonly AsyncDataLoader<string> mileageLoader;
        private Form form;

        public VehicleDriverSelector()
        {
            InitializeComponent();
            vehiclesLoader = new AsyncDataLoader<DataTable>(this);
            driversLoader = new AsyncDataLoader<DataTable>(this);
            mileageLoader = new AsyncDataLoader<string>(this);
            Disposed += DisposeLoaders;
        }


        public VehicleDriverSelector(Form form)
            : this()
        {
            this.form = form;
        }

        private void DisposeLoaders(object sender, EventArgs args)
        {
            vehiclesLoader.Dispose();
            driversLoader.Dispose();
            mileageLoader.Dispose();
        }

        #region Retornar os amountes selecionado no ComboBox Driver
        private int driverId;
        public int DriverId
        {
            get
            {
                return driverId;
            }
        }
        private string nameDriver;
        public string NameDriver
        {
            get
            {
                return nameDriver;
            }
        }
        #endregion

        #region Retornar os amountes selecionado no ComboBox Vehicles
        private int vehicleId;
        public int VehicleId
        {
            get
            {
                return vehicleId;
            }
        }
        private string modelVehicle;
        public string ModelVehicle
        {
            get
            {
                return modelVehicle;
            }
        }

        private string finalVehicleMileage;
        public string FinalVehicleMileage
        {
            get
            {
                return finalVehicleMileage;
            }
        }
        #endregion

        public delegate void VehicleEndingMileage(string finalMileage);
        public event VehicleEndingMileage FinalMileage;

        protected virtual void OnFinalMileage()
        {
            if (FinalMileage != null)
            {
                FinalMileage(finalVehicleMileage);
            }
        }

        private async void VehicleDriverSelector_Load(object sender, EventArgs e)
        {
            await Task.WhenAll(
                vehiclesLoader.LoadAsync(
                    cancellationToken => FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Query
                        .IdAndModelActiveAsync(cancellationToken),
                    table => CbxVehicle.DataSource = table),
                driversLoader.LoadAsync(
                    cancellationToken => FleetManagement.Desktop.Client.Features.Registrations.Drivers.Query
                        .IdAndNameActiveAsync(cancellationToken),
                    table => CbxDriver.DataSource = table));
        }

        public async void CbxVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxVehicle.SelectedValue == null
                || !int.TryParse(CbxVehicle.SelectedValue.ToString(), out vehicleId))
                return;

            modelVehicle = CbxVehicle.Text.ToString();
            var selectedVehicleId = vehicleId;
            var ownerForm = form ?? FindForm();

            await mileageLoader.LoadAsync(
                cancellationToken => ownerForm?.Name == "RefuelingForm"
                    ? FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query
                        .LatestVehicleMileageAsync(selectedVehicleId, cancellationToken)
                    : FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Query
                        .LatestVehicleMileageAsync(selectedVehicleId, cancellationToken),
                mileage =>
                {
                    finalVehicleMileage = mileage;
                    OnFinalMileage();
                });
        }

        private void CbxDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxDriver.SelectedValue != null
                && int.TryParse(CbxDriver.SelectedValue.ToString(), out driverId))
            {
                nameDriver = CbxDriver.Text.ToString();
            }
        }
    }
}
