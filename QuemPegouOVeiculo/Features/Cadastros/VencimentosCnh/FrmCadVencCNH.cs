using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

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
                DgvVencCNH.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh.Query.Register(search);
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
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh.Insert.Register(vencimentoCNH);
                        break;
                    case 'U':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh.Update.Register(vencimentoCNH);
                        break;
                    case 'D':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh.Delete.Register(vencimentoCNH);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
                LimparCampos.LimparMaskedTextBox(this.Controls);
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
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
            if (e.RowIndex < 0)
                return;

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
            HabilitarBotoes.DefinirModoEdicao(true, BtnAlterar, BtnExcluir, BtnGravar);
        }

        private void FrmCadVencCNH_Load(object sender, EventArgs e)
        {
            ListRegister("%%");
        }
    }
}
