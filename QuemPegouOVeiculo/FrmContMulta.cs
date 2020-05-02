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
    public partial class FrmContMulta : Form
    {
        public FrmContMulta()
        {
            InitializeComponent();
        }
        ControleMultaObj controleMulta;
        int idCtrlMulta = 0, idVeiculo = 0, idMotorista = 0;

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
        }

        private void ListRegister(string search)
        {
            try
            {
                DgvMultas.DataSource = Negocio.Controle.Multa.Query.Register(search);
                LblMultas.Text = "Multas - " + DgvMultas.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void DgvMultas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idCtrlMulta = int.Parse(DgvMultas.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            idVeiculo = int.Parse(DgvMultas.Rows[e.RowIndex].Cells["Id_Veiculo"].Value.ToString());
            idMotorista = int.Parse(DgvMultas.Rows[e.RowIndex].Cells["Id_Motorista"].Value.ToString());

            UCVeiculoMotorista.CbxVeiculo.SelectedValue = idVeiculo;
            UCVeiculoMotorista.CbxMotorista.SelectedValue = idMotorista;

            if (UCVeiculoMotorista.CbxVeiculo.SelectedValue != null)
            {
                UCVeiculoMotorista.CbxVeiculo.SelectedValue = idVeiculo;
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

            if (UCVeiculoMotorista.CbxMotorista.SelectedValue != null)
            {
                UCVeiculoMotorista.CbxMotorista.SelectedValue = idMotorista;
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



            UCDescricao.TxtDescricao.Text = DgvMultas.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
            UCValor.TxtValor.Text = decimal.Parse(DgvMultas.Rows[e.RowIndex].Cells["Valor"].Value.ToString()).ToString("#,##0.00");

            MktData.Text = DgvMultas.Rows[e.RowIndex].Cells["Data"].Value.ToString();
            TxtPontos.Text = DgvMultas.Rows[e.RowIndex].Cells["Pontos"].Value.ToString();

            Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(true, BtnAlterar, BtnExcluir, BtnGravar);
        }

        private void Manipulate(char opc)
        {
            controleMulta = new ControleMultaObj();

            try
            {
                controleMulta.Id = idCtrlMulta;
                controleMulta.Veiculo = new VeiculoObj();
                controleMulta.Veiculo.Id = UCVeiculoMotorista.IdVeiculo;
                controleMulta.Motorista = new MotoristaObj();
                controleMulta.Motorista.Id = UCVeiculoMotorista.IdMotorista;

                controleMulta.Descricao = UCDescricao.Descricao;
                controleMulta.Valor = UCValor.Valor;

                controleMulta.Data = DateTime.Parse(MktData.Text.Trim());
                controleMulta.Pontos = int.Parse(TxtPontos.Text.Trim());

                switch (opc)
                {
                    case 'I':
                        Negocio.Controle.Multa.Insert.Register(controleMulta);
                        break;
                    case 'U':
                        Negocio.Controle.Multa.Update.Register(controleMulta);
                        break;
                    case 'D':
                        Negocio.Controle.Multa.Delete.Register(controleMulta);
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

        private void FrmContMulta_Load(object sender, EventArgs e)
        {
            ListRegister("%%");
        }
    }
}
