using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadContVeiculo : Form
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public FrmCadContVeiculo()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        ControleVeiculoObj controleVeiculo;
        int idContVeiculo = 0, idVeiculo = 0, idMotorista = 0;

        DateTime dtDataHoraSaida, dtDataHoraChegada;

        private void ListRegister(string search)
        {
            try
            {
                DgvControleVeiculo.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.Register(search);
                LblContVeiculo.Text = "Controle Veiculo - " + DgvControleVeiculo.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task ListRegisterAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvControleVeiculo.DataSource = table;
                    LblContVeiculo.Text = "Controle Veiculo - " + DgvControleVeiculo.Rows.Count.ToString("000");
                },
                delayMilliseconds);
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
            if (e.RowIndex < 0)
                return;

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
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }
            /*Motorista*/
            if (UCVeiculoMotorista.CbxMotorista.SelectedValue != null)
            {
                UCVeiculoMotorista.CbxMotorista.SelectedValue = idMotorista;
            }
            else
            {
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }

            DateTime.TryParse(DgvControleVeiculo.Rows[e.RowIndex].Cells["DataHoraSaida"].Value.ToString(), out dtDataHoraSaida);
            DateTime.TryParse(DgvControleVeiculo.Rows[e.RowIndex].Cells["DataHoraChegada"].Value.ToString(), out dtDataHoraChegada);


            if (dtDataHoraChegada.Date == DateTime.Parse("01/01/0001 00:00:00"))
            {
                MktDtChegada.Clear();
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

            HabilitarBotoes.DefinirModoEdicao(true, BtnAlterar, BtnExcluir, BtnGravar);
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
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Insert.Register(controleVeiculo);
                        break;
                    case 'U':
                        controleVeiculo.Status = 'C';
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Update.Register(controleVeiculo);
                        break;
                    case 'D':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Delete.Register(controleVeiculo);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();

                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);

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

        private async void FrmCadContVeiculo_Load(object sender, EventArgs e)
        {
            MktDtSaida.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            await ListRegisterAsync("%%");
        }

        private async void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            await ListRegisterAsync("%" + TxtPesquisa.Text.Trim() + "%", 300);
        }
    }
}
