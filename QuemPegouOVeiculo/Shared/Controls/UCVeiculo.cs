using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class UCVeiculo : UserControl
    {
        private readonly AsyncDataLoader<DataTable> loader;

        public UCVeiculo()
        {
            InitializeComponent();
            loader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => loader.Dispose();
        }

        #region Retornar os valores selecionado no ComboBox Veiculos
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        private string modelo;
        public string Modelo
        {
            get
            {
                return modelo;
            }
        }
        #endregion

        private async void UCVeiculo_Load(object sender, EventArgs e)
        {
            await loader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Veiculos.Query
                    .IdAndModelActiveAsync(cancellationToken),
                table => CbxVeiculo.DataSource = table);
        }

        private void CbxVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxVeiculo.SelectedValue != null && int.TryParse(CbxVeiculo.SelectedValue.ToString(), out id))
            {
                modelo = CbxVeiculo.Text.ToString();
            }
            else
            {
                return;
            }

        }
    }
}
