using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmRelStatusVeiculo : Form
    {
        bool abrir = true;
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
