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
    public partial class FrmContCombustivel : Form
    {
        public FrmContCombustivel()
        {
            InitializeComponent();
        }
        ControleAbastecimentoObj controleAbastecimento = null;
        int idCtrlAbastecimento = 0, idVeiculo = 0, idMotorista = 0;

        private void ListRegister(string search)
        {
            try
            {
                DgvAbastecimento.DataSource = Negocio.Controle.Combustivel.Query.Register(search);
                LblAbastecimento.Text = "Abastecimento - " + DgvAbastecimento.Rows.Count.ToString("000");
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

        private void DgvAbastecimento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idCtrlAbastecimento = int.Parse(DgvAbastecimento.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            idVeiculo = int.Parse(DgvAbastecimento.Rows[e.RowIndex].Cells["Id_Veiculo"].Value.ToString());
            idMotorista = int.Parse(DgvAbastecimento.Rows[e.RowIndex].Cells["Id_Motorista"].Value.ToString());

            UCVeiculoMotorista.CbxMotorista.SelectedValue = idMotorista;
            UCVeiculoMotorista.CbxVeiculo.SelectedValue = idVeiculo;

            if (UCVeiculoMotorista.CbxVeiculo.SelectedValue != null)
            {
                UCVeiculoMotorista.CbxVeiculo.SelectedValue = idVeiculo;
            }
            else
            {
                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }

            if (UCVeiculoMotorista.CbxMotorista.SelectedValue != null)
            {
                UCVeiculoMotorista.CbxMotorista.SelectedValue = idMotorista;
            }
            else
            {
                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }

            UCDescricao.TxtDescricao.Text = DgvAbastecimento.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
            UCValor.TxtValor.Text = decimal.Parse(DgvAbastecimento.Rows[e.RowIndex].Cells["Valor"].Value.ToString()).ToString("#,##0.00");

            MktData.Text = DgvAbastecimento.Rows[e.RowIndex].Cells["Data"].Value.ToString();
            TxtKmInicial.Text = DgvAbastecimento.Rows[e.RowIndex].Cells["KmInicial"].Value.ToString();
            Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(true, BtnAlterar, BtnExcluir, BtnGravar);
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

        private void UCVeiculoMotorista_KmFinal(string kmfinal)
        {
            //TxtKmInicial.Text = kmfinal;
        }

        private void TxtLitros_TextChanged(object sender, EventArgs e)
        {
            TxtLitros.Text = Negocio.Utilitario.FormatarValor.Valor(TxtLitros.Text.Trim());
            TxtLitros.Select(TxtLitros.Text.Length, 0);
        }

        private void TxtLitros_Leave(object sender, EventArgs e)
        {
            TxtLitros.Text = Negocio.Utilitario.FormatarValor.Zero(TxtLitros.Text.Trim());
            TxtLitros.Text = Negocio.Utilitario.FormatarValor.ParaValor(TxtLitros.Text.Trim());
        }

        private void TxtLitros_Enter(object sender, EventArgs e)
        {
            if (TxtLitros.Text == "0,00")
            {
                TxtLitros.Clear();
            }
        }

        private void Manipulate(char opc)
        {
            controleAbastecimento = new ControleAbastecimentoObj();
            try
            {
                controleAbastecimento.Id = idCtrlAbastecimento;
                controleAbastecimento.Veiculo = new VeiculoObj();
                controleAbastecimento.Veiculo.Id = UCVeiculoMotorista.IdVeiculo;
                controleAbastecimento.Motorista = new MotoristaObj();
                controleAbastecimento.Motorista.Id = UCVeiculoMotorista.IdMotorista;
                controleAbastecimento.KmInicio = TxtKmInicial.Text.Trim();
                controleAbastecimento.Data = DateTime.Parse(MktData.Text.Trim());
                controleAbastecimento.Valor = UCValor.Valor;
                controleAbastecimento.Descricao = UCDescricao.Descricao;

                switch (opc)
                {
                    case 'I':
                        Negocio.Controle.Combustivel.Insert.Register(controleAbastecimento);
                        break;
                    case 'U':
                        Negocio.Controle.Combustivel.Update.Register(controleAbastecimento);
                        break;
                    case 'D':
                        Negocio.Controle.Combustivel.Delete.Register(controleAbastecimento);
                        break;

                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
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

        private void FrmContCombustivel_Load(object sender, EventArgs e)
        {
            ListRegister("%%");
        }
    }
}
