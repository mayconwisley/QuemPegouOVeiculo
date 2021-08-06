using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmRelContManutencao : Form
    {
        public FrmRelContManutencao()
        {
            InitializeComponent();
        }
        bool abrir = true;
        DateTime dtInicio, dtFinal;

        private void BtnListar_Click(object sender, EventArgs e)
        {
            ListarRelatorio();
        }

        private void RbGeral_CheckedChanged(object sender, EventArgs e)
        {
            if (RbGeral.Checked)
            {
                GbPeriodo.Enabled = false;
                GbVeiculo.Enabled = false;
            }
        }

        private void RbVeiculo_CheckedChanged(object sender, EventArgs e)
        {
            if (RbVeiculo.Checked)
            {
                GbPeriodo.Enabled = false;
                GbVeiculo.Enabled = true;
            }
        }

        private void RbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (RbPeriodo.Checked)
            {
                GbPeriodo.Enabled = true;
                GbVeiculo.Enabled = false;
            }
        }

        private void ListarRelatorio()
        {
            FrmVisualizarRelatorio visualizarRelatorio = null;

            try
            {
                if (RbGeral.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("ALL", 5);

                }
                if (RbVeiculo.Checked)
                {
                    int idVeiculo = UCVeiculo.Id;
                    visualizarRelatorio = new FrmVisualizarRelatorio("VEI", 5, idVeiculo);
                }
                if (RbPeriodo.Checked)
                {

                    if (MktDataInicio.Text == "  /  /")
                    {
                        MessageBox.Show("Inserir uma data de inicio.", "Aviso");
                        return;
                    }

                    if (MktDataFinal.Text == "  /  /")
                    {
                        MktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    DateTime.TryParse(MktDataInicio.Text, out dtInicio);
                    DateTime.TryParse(MktDataFinal.Text, out dtFinal);

                    if (RbPeriodo.Checked)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTPer", 5, dtInicio, dtFinal);
                    }
                }

                visualizarRelatorio.MdiParent = FrmPrincipal.ActiveForm;
                foreach (Form item in Application.OpenForms)
                {
                    if (item is FrmVisualizarRelatorio)
                    {
                        item.Focus();
                        abrir = false;
                    }
                }
                if (abrir)
                {
                    visualizarRelatorio.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
