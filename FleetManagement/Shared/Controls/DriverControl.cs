using System;
using System.Data;
using System.Windows.Forms;
using FleetManagement.Shared.Presentation;

namespace FleetManagement
{
    public partial class DriverControl : UserControl
    {
        private readonly AsyncDataLoader<DataTable> loader;

        public DriverControl()
        {
            InitializeComponent();
            loader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => loader.Dispose();
        }

        #region Retornar os amountes selecionado no ComboBox

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        private string name;
        public string SelectedName
        {
            get
            {
                return name;
            }
        }
        #endregion
        private async void UCDriver_Load(object sender, EventArgs e)
        {
            await loader.LoadAsync(
                cancellationToken => FleetManagement.Desktop.Client.Features.Registrations.Drivers.Query
                    .IdAndNameActiveAsync(cancellationToken),
                table => CbxDriver.DataSource = table);
        }

        private void CbxDriver_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CbxDriver.SelectedValue != null && int.TryParse(CbxDriver.SelectedValue.ToString(), out id))
                name = CbxDriver.Text.ToString();
        }
    }
}
