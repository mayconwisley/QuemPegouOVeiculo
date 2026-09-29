using System;
using System.Data;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class UCMotorista : UserControl
    {
        private readonly AsyncDataLoader<DataTable> loader;

        public UCMotorista()
        {
            InitializeComponent();
            loader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => loader.Dispose();
        }

        #region Retornar os valores selecionado no ComboBox

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        private string nome;
        public string Nome
        {
            get
            {
                return nome;
            }
        }
        #endregion
        private async void UCMotorista_Load(object sender, EventArgs e)
        {
            await loader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Query
                    .IdAndNameActiveAsync(cancellationToken),
                table => CbxMotorista.DataSource = table);
        }

        private void CbxMotorista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CbxMotorista.SelectedValue != null && int.TryParse(CbxMotorista.SelectedValue.ToString(), out id))
                nome = CbxMotorista.Text.ToString();
        }
    }
}
