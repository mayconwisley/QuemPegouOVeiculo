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
                DgvManutencao.DataSource = Negocio.Controle.Mecanica.Query.Register(search);
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
                        Negocio.Controle.Mecanica.Insert.Register(controleManutencao);
                        break;
                    case 'U':
                        Negocio.Controle.Mecanica.Update.Register(controleManutencao);
                        break;
                    case 'D':
                        Negocio.Controle.Mecanica.Delete.Register(controleManutencao);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                ListRegister("%" + TxtPesquisar.Text.Trim() + "%");
                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                UCValor.TxtValor.Text = "0,00";
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);
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
            idCtrlManutencao = int.Parse(DgvManutencao.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            idVeiculo = int.Parse(DgvManutencao.Rows[e.RowIndex].Cells["Id_Veiculo"].Value.ToString());
            UCVeiculo.CbxVeiculo.SelectedValue = idVeiculo;

            if (UCVeiculo.CbxVeiculo.SelectedValue != null)
            {
                UCVeiculo.CbxVeiculo.SelectedValue = idVeiculo;
            }
            else
            {
                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                UCValor.TxtValor.Text = "0,00";
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }


            UCDescricao.TxtDescricao.Text = DgvManutencao.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
            UCValor.TxtValor.Text = DgvManutencao.Rows[e.RowIndex].Cells["Valor"].Value.ToString();


            MktData.Text = DgvManutencao.Rows[e.RowIndex].Cells["Data"].Value.ToString();

            Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(true, BtnAlterar, BtnExcluir, BtnGravar);

        }

        private void TxtPesquisar_TextChanged(object sender, EventArgs e)
        {
            ListRegister("%" + TxtPesquisar.Text.Trim() + "%");
        }
    }
}
