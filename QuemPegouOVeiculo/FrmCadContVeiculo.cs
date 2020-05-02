using Objeto;
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
    public partial class FrmCadContVeiculo : Form
    {
        public FrmCadContVeiculo()
        {
            InitializeComponent();
        }
        ControleVeiculoObj controleVeiculo;
        int idContVeiculo = 0, idVeiculo = 0, idMotorista = 0;

        DateTime dtDataHoraSaida, dtDataHoraChegada;

        private void ListRegister(string search)
        {
            try
            {
                DgvControleVeiculo.DataSource = Negocio.Controle.Veiculo.Query.Register(search);
                LblContVeiculo.Text = "Controle Veiculo - " + DgvControleVeiculo.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnGravar_Click(object sender, EventArgs e)
        {
            Manipupate('I');
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            Manipupate('U');
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            Manipupate('D');
        }

        private void DgvControleVeiculo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idContVeiculo = int.Parse(DgvControleVeiculo.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            idVeiculo = int.Parse(DgvControleVeiculo.Rows[e.RowIndex].Cells["Id_Veiculo"].Value.ToString());
            idMotorista = int.Parse(DgvControleVeiculo.Rows[e.RowIndex].Cells["Id_Motorista"].Value.ToString());

            /*Verificar se os Veiculos ou/e motoristas estão ativos*/
            UCVeiculoMotorista.CbxVeiculo.SelectedValue = idVeiculo;
            UCVeiculoMotorista.CbxMotorista.SelectedValue = idMotorista;

            /*Veiculo*/
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
            /*Motorista*/
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

            DateTime.TryParse(DgvControleVeiculo.Rows[e.RowIndex].Cells["DataHoraSaida"].Value.ToString(), out dtDataHoraSaida);
            DateTime.TryParse(DgvControleVeiculo.Rows[e.RowIndex].Cells["DataHoraChegada"].Value.ToString(), out dtDataHoraChegada);


            if (dtDataHoraChegada.Date == DateTime.Parse("01/01/0001 00:00:00"))
            {
                MktDtChegada.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                MktDtChegada.Text = dtDataHoraChegada.ToString("dd/MM/yyyy HH:mm");
            }

            /*Travar edição para chegada na atualização*/
            MktDtSaida.Enabled = false;
            LkLblDtSaidaAtual.Enabled = false;

            MktDtSaida.Text = dtDataHoraSaida.ToString("dd/MM/yyyy HH:mm");

            UCDescricao.TxtDescricao.Text = DgvControleVeiculo.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();


            TxtKmInicial.Text = DgvControleVeiculo.Rows[e.RowIndex].Cells["KmInicial"].Value.ToString();
            TxtKmFinal.Text = DgvControleVeiculo.Rows[e.RowIndex].Cells["KmFinal"].Value.ToString();

            Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(true, BtnAlterar, BtnExcluir, BtnGravar);
        }

        private void Manipupate(char opc)
        {
            controleVeiculo = new ControleVeiculoObj();
            try
            {
                DateTime.TryParse(MktDtSaida.Text.Trim(), out dtDataHoraSaida);
                DateTime.TryParse(MktDtChegada.Text.Trim(), out dtDataHoraChegada);


                controleVeiculo.Id = idContVeiculo;
                controleVeiculo.Veiculo = new VeiculoObj();
                controleVeiculo.Veiculo.Id = UCVeiculoMotorista.IdVeiculo;
                controleVeiculo.Motorista = new MotoristaObj();
                controleVeiculo.Motorista.Id = UCVeiculoMotorista.IdMotorista;


                if (dtDataHoraSaida.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    controleVeiculo.DataHoraSaida = null;
                }
                else
                {
                    controleVeiculo.DataHoraSaida = dtDataHoraSaida;
                }


                if (dtDataHoraChegada.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    controleVeiculo.DataHoraChegada = null;
                }
                else
                {
                    controleVeiculo.DataHoraChegada = dtDataHoraChegada;
                }

                controleVeiculo.Descricao = UCDescricao.Descricao;
                controleVeiculo.KmInicial = TxtKmInicial.Text.Trim();
                controleVeiculo.KmFinal = TxtKmFinal.Text.Trim();

                switch (opc)
                {
                    case 'I':
                        controleVeiculo.Status = 'S';
                        Negocio.Controle.Veiculo.Insert.Register(controleVeiculo);
                        break;
                    case 'U':
                        controleVeiculo.Status = 'C';
                        Negocio.Controle.Veiculo.Update.Register(controleVeiculo);
                        break;
                    case 'D':
                        Negocio.Controle.Veiculo.Delete.Register(controleVeiculo);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();

                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);

                /*Liberar edição*/
                MktDtSaida.Enabled = true;
                LkLblDtSaidaAtual.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LkLblDtSaidaAtual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktDtSaida.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void UCVeiculoMotorista_KmFinal(string kmfinal)
        {
            TxtKmInicial.Text = kmfinal;
        }


        private void LkLblDtChegadaAtual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktDtChegada.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void FrmCadContVeiculo_Load(object sender, EventArgs e)
        {
            ListRegister("%%");
        }

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
        }
    }
}
