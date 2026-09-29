using System;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmRelStatusVeiculo : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
    {
        DateTime dtInicio, dtFinal;
        public FrmRelStatusVeiculo()
        {
            InitializeComponent();
        }

        private void ListarRelatorio()
        {
            FrmVisualizarRelatorio visualizarRelatorio = null;

            try
            {
                if (RbTodos.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("ALL", 3);

                }
                if (RbVeiculo.Checked)
                {
                    int idVeiculo = UCVeiculo.Id;
                    visualizarRelatorio = new FrmVisualizarRelatorio("VEI", 3, idVeiculo);
                }
                if (RbPeriodo.Checked)
                {

                    if (MktDataInicio.Text == "  /  /" && CbListaDataNull.Checked == false)
                    {
                        MessageBox.Show("Inserir uma data de inicio.", "Aviso");
                        return;
                    }

                    if (MktDataFinal.Text == "  /  /" && CbListaDataNull.Checked == false)
                    {
                        MktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    DateTime.TryParse(MktDataInicio.Text, out dtInicio);
                    DateTime.TryParse(MktDataFinal.Text, out dtFinal);

                    if (RbDataInicio.Checked && CbListaDataNull.Checked == false)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTIni", 3, dtInicio, dtFinal);
                    }
                    else if (RbDataFinal.Checked && CbListaDataNull.Checked == false)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTFin", 3, dtInicio, dtFinal);
                    }
                    else if (CbListaDataNull.Checked)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("NULL", 3);
                    }
                }

                MdiForms.AbrirSubstituindo(this, visualizarRelatorio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RbDataFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (RbDataFinal.Checked)
            {
                CbListaDataNull.Visible = true;

            }
        }

        private void RbDataInicio_CheckedChanged(object sender, EventArgs e)
        {
            if (RbDataInicio.Checked)
            {
                CbListaDataNull.Visible = false;
            }
        }

        private void RbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTodos.Checked)
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

        private void BtnListar_Click(object sender, EventArgs e)
        {
            ListarRelatorio();
        }
    }
}
