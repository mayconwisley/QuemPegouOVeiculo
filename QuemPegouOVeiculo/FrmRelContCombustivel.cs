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
    public partial class FrmRelContCombustivel : Form
    {
        public FrmRelContCombustivel()
        {
            InitializeComponent();
        }

        bool abrir = true;
        DateTime dtInicio, dtFinal;
        int idVeiculo = 0, idMotorista = 0;

        private void RbGeral_CheckedChanged(object sender, EventArgs e)
        {
            GbMotorista.Enabled = false;
            GbPeriodo.Enabled = false;
            GbVeiculo.Enabled = false;
        }

        private void RbVeiculo_CheckedChanged(object sender, EventArgs e)
        {
            GbMotorista.Enabled = false;
            GbPeriodo.Enabled = false;
            GbVeiculo.Enabled = true;
        }

        private void RbMotorista_CheckedChanged(object sender, EventArgs e)
        {
            GbMotorista.Enabled = true;
            GbPeriodo.Enabled = false;
            GbVeiculo.Enabled = false;
        }

        private void RbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            GbMotorista.Enabled = false;
            GbPeriodo.Enabled = true; ;
            GbVeiculo.Enabled = false;
        }

        private void RbVeiMot_CheckedChanged(object sender, EventArgs e)
        {
            GbMotorista.Enabled = true;
            GbPeriodo.Enabled = false;
            GbVeiculo.Enabled = true;
        }

        private void CbVeiculo_CheckedChanged(object sender, EventArgs e)
        {
            if (CbVeiculo.Checked)
            {
                CbMotorista.Enabled = false;
                GbVeiculo.Enabled = true;
            }
            else
            {
                CbMotorista.Enabled = true;
                GbVeiculo.Enabled = false;
            }
        }

        private void CbMotorista_CheckedChanged(object sender, EventArgs e)
        {
            if (CbMotorista.Checked)
            {
                CbVeiculo.Enabled = false;
                GbMotorista.Enabled = true;
            }
            else
            {
                CbVeiculo.Enabled = true;
                GbMotorista.Enabled = false;
            }
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            ListarRelatorio();
        }

        private void ListarRelatorio()
        {
            FrmVisualizarRelatorio visualizarRelatorio = null;
            idVeiculo = UCVeiculo.Id;
            idMotorista = UCMotorista.Id;

            /* ALL = Todos, VEI = Veiculos, MOT = Motoristas
             * DTSai = Data Saida, DTChe = Data chegada
             * DTSaiVei = Data Saída e Veiculo, DTCheVei = Data Chegada e Veiculo
             * DTSaiMot = Data Saída e Motorista, DTCheMot = Data Chegada e Motorista
             * NULL = Nulo, VEIMOT = Veiculo e Motorista
             */
            try
            {
                if (RbGeral.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("ALL", 6);
                }
                if (RbVeiculo.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("VEI", 6, idVeiculo);
                }
                if (RbMotorista.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("MOT", 6, idMotorista);
                }

                if (RbPeriodo.Checked)
                {

                    if (MktDataIncial.Text == "  /  /")
                    {
                        MessageBox.Show("Inserir uma data de inicio.", "Aviso");
                        return;
                    }

                    if (MktDataFinal.Text == "  /  /")
                    {
                        MktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    DateTime.TryParse(MktDataIncial.Text, out dtInicio);
                    DateTime.TryParse(MktDataFinal.Text, out dtFinal);

                    if (RbPeriodo.Checked && CbVeiculo.Checked == false && CbMotorista.Checked == false)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTPer", 6, dtInicio, dtFinal);
                    }
                    else if (RbPeriodo.Checked && CbVeiculo.Checked == true)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTPerVei", 6, dtInicio, dtFinal, idVeiculo);
                    }

                    else if (RbPeriodo.Checked && CbMotorista.Checked == true)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTPerMot", 6, dtInicio, dtFinal, idMotorista);
                    }
                }
                if (RbVeiMot.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("VEIMOT", 6, idVeiculo, idMotorista);
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
