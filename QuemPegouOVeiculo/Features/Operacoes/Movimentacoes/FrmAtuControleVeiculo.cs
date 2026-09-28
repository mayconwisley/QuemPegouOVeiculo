using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmAtuControleVeiculo : Form
    {
        int idCont;
        public FrmAtuControleVeiculo()
        {
            InitializeComponent();
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

        private void FrmAtuControleVeiculo_Load(object sender, EventArgs e)
        {
            ListRegister();
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
