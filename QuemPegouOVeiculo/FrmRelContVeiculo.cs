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
    public partial class FrmRelContVeiculo : Form
    {
        public FrmRelContVeiculo()
        {
            InitializeComponent();
        }
        bool abrir = true;
        DateTime dtInicio, dtFinal;
        int idVeiculo = 0, idMotorista = 0;

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
                    visualizarRelatorio = new FrmVisualizarRelatorio("ALL", 4);
                }
                if (RbVeiculo.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("VEI", 4, idVeiculo);
                }
                if (RbMotorista.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("MOT", 4, idMotorista);
                }

                if (RbPeriodo.Checked)
                {

                    if (MktDataIncial.Text == "  /  /" && CbListaDataNull.Checked == false)
                    {
                        MessageBox.Show("Inserir uma data de inicio.", "Aviso");
                        return;
                    }

                    if (MktDataFinal.Text == "  /  /" && CbListaDataNull.Checked == false)
                    {
                        MktDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    DateTime.TryParse(MktDataIncial.Text, out dtInicio);
                    DateTime.TryParse(MktDataFinal.Text, out dtFinal);

                    if (RbDtSaida.Checked && CbListaDataNull.Checked == false && CbVeiculo.Checked == false && CbMotorista.Checked == false)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTSai", 4, dtInicio, dtFinal);
                    }
                    else if (RbDtChegada.Checked && CbListaDataNull.Checked == false && CbVeiculo.Checked == false && CbMotorista.Checked == false)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTChe", 4, dtInicio, dtFinal);
                    }
                    else if (RbDtSaida.Checked && CbVeiculo.Checked == true)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTSaiVei", 4, dtInicio, dtFinal, idVeiculo);
                    }
                    else if (RbDtChegada.Checked && CbVeiculo.Checked == true)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTCheVei", 4, dtInicio, dtFinal, idVeiculo);
                    }
                    else if (RbDtSaida.Checked && CbMotorista.Checked == true)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTSaiMot", 4, dtInicio, dtFinal, idMotorista);
                    }
                    else if (RbDtChegada.Checked && CbMotorista.Checked == true)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("DTCheMot", 4, dtInicio, dtFinal, idMotorista);
                    }
                    else if (CbListaDataNull.Checked)
                    {
                        visualizarRelatorio = new FrmVisualizarRelatorio("NULL", 4);
                    }
                }
                if (RbVeiMot.Checked)
                {
                    visualizarRelatorio = new FrmVisualizarRelatorio("VEIMOT", 4, idVeiculo, idMotorista);
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
        private void BtnListar_Click(object sender, EventArgs e)
        {
            ListarRelatorio();
        }
    }
}
