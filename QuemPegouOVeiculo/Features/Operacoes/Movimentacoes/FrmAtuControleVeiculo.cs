using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmAtuControleVeiculo : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;
        int idCont;
        public FrmAtuControleVeiculo()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        public void ListRegister()
        {
            try
            {
                DgvControleVeiculo.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterArrivalNull();
                lblInfo.Text = "Controle Veiculo - " + DgvControleVeiculo.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task ListRegisterAsync() =>
            listLoader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query
                    .RegisterArrivalNullAsync(cancellationToken),
                table =>
                {
                    DgvControleVeiculo.DataSource = table;
                    lblInfo.Text = "Controle Veiculo - " + DgvControleVeiculo.Rows.Count.ToString("000");
                });

        private async void FrmAtuControleVeiculo_Load(object sender, EventArgs e)
        {
            await ListRegisterAsync();
        }

        private void DgvControleVeiculo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            idCont = int.Parse(DgvControleVeiculo.Rows[e.RowIndex].Cells["Id"].Value.ToString());



            FrmAtuControleVeiculo_0 controleVeiculo_0 = new FrmAtuControleVeiculo_0(idCont, this);
            controleVeiculo_0.MdiParent = MdiParent;
            controleVeiculo_0.Show();
        }
    }
}
