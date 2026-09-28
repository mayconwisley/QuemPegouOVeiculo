using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmContManutencao : Form
    {
        public FrmContManutencao()
        {
            InitializeComponent();
        }
        ControleManutencaoObj controleManutencao;
        int idCtrlManutencao = 0, idVeiculo = 0;
        private void ListRegister(string search)
        {
            try
            {
                DgvManutencao.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes.Query.Register(search);
                LblManutencao.Text = "Manutenção - " + DgvManutencao.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Manipulate(char opc)
        {
            controleManutencao = new ControleManutencaoObj();

            try
            {
                controleManutencao.Id = idCtrlManutencao;
                controleManutencao.Veiculo = new VeiculoObj();
                controleManutencao.Veiculo.Id = UCVeiculo.Id;
                controleManutencao.Data = DateTime.Parse(MktData.Text.Trim());
                controleManutencao.Valor = UCValor.Valor;
                controleManutencao.Descricao = UCDescricao.Descricao;

                switch (opc)
                {
                    case 'I':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes.Insert.Register(controleManutencao);
                        break;
                    case 'U':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes.Update.Register(controleManutencao);
                        break;
                    case 'D':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes.Delete.Register(controleManutencao);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                ListRegister("%" + TxtPesquisar.Text.Trim() + "%");
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                UCValor.TxtValor.Text = "0,00";
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FrmContManutencao_Load(object sender, EventArgs e)
        {
            ListRegister("%%");
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

        private void DgvManutencao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            idCtrlManutencao = int.Parse(DgvManutencao.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            idVeiculo = int.Parse(DgvManutencao.Rows[e.RowIndex].Cells["Id_Veiculo"].Value.ToString());
            UCVeiculo.CbxVeiculo.SelectedValue = idVeiculo;

            if (UCVeiculo.CbxVeiculo.SelectedValue != null)
            {
                UCVeiculo.CbxVeiculo.SelectedValue = idVeiculo;
            }
            else
            {
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                UCValor.TxtValor.Text = "0,00";
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }


            UCDescricao.TxtDescricao.Text = DgvManutencao.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
            UCValor.TxtValor.Text = DgvManutencao.Rows[e.RowIndex].Cells["Valor"].Value.ToString();


            MktData.Text = DgvManutencao.Rows[e.RowIndex].Cells["Data"].Value.ToString();

            HabilitarBotoes.DefinirModoEdicao(true, BtnAlterar, BtnExcluir, BtnGravar);

        }

        private void TxtPesquisar_TextChanged(object sender, EventArgs e)
        {
            ListRegister("%" + TxtPesquisar.Text.Trim() + "%");
        }
    }
}
