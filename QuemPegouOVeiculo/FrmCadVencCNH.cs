using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Objeto;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadVencCNH : Form
    {
        public FrmCadVencCNH()
        {
            InitializeComponent();
        }
        VencimentoCNHObj vencimentoCNH = null;
        int idVencimentoCNH = 0, idMotorista = 0;
        private void ListRegister(string search)
        {
            try
            {
                DgvVencCNH.DataSource = Negocio.Controle.CNH.Query.Register(search);
                LblVencimento.Text = "Vencimento - " + DgvVencCNH.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Manipulate(char opc)
        {
            vencimentoCNH = new VencimentoCNHObj();

            try
            {
                vencimentoCNH.Id = idVencimentoCNH;
                vencimentoCNH.Motorista = new MotoristaObj();
                vencimentoCNH.Motorista.Id = UCMotorista.Id;
                vencimentoCNH.Data = DateTime.Parse(MktDtVencimento.Text);

                if (CbVencida.Checked)
                {
                    vencimentoCNH.Status = 'V';
                }
                else
                {
                    vencimentoCNH.Status = 'N';
                }

                switch (opc)
                {
                    case 'I':
                        Negocio.Controle.CNH.Insert.Register(vencimentoCNH);
                        break;
                    case 'U':
                        Negocio.Controle.CNH.Update.Register(vencimentoCNH);
                        break;
                    case 'D':
                        Negocio.Controle.CNH.Delete.Register(vencimentoCNH);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
        }

        private void BtnGravar_Click(object sender, EventArgs e)
        {
            Manipulate('I');
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            Manipulate('U');
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            Manipulate('D');
        }

        private void DgvVencCNH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idVencimentoCNH = int.Parse(DgvVencCNH.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            idMotorista = int.Parse(DgvVencCNH.Rows[e.RowIndex].Cells["Id_Motorista"].Value.ToString());
            MktDtVencimento.Text = DgvVencCNH.Rows[e.RowIndex].Cells["Data"].Value.ToString();
            string status = DgvVencCNH.Rows[e.RowIndex].Cells["Status"].Value.ToString();
            UCMotorista.CbxMotorista.SelectedValue = idMotorista;
            if (status == "Vencido")
            {
                CbVencida.Checked = true;

            }
            else
            {
                CbVencida.Checked = false;
            }
            Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(true, BtnAlterar, BtnExcluir, BtnGravar);
        }

        private void FrmCadVencCNH_Load(object sender, EventArgs e)
        {
            ListRegister("%%");
        }
    }
}
