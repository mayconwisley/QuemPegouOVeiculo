using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmContCombustivel : Form
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public FrmContCombustivel()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        ControleAbastecimentoObj controleAbastecimento = null;
        int idCtrlAbastecimento = 0, idVeiculo = 0, idMotorista = 0;

        private void ListRegister(string search)
        {
            try
            {
                DgvAbastecimento.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.Register(search);
                LblAbastecimento.Text = "Abastecimento - " + DgvAbastecimento.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task ListRegisterAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvAbastecimento.DataSource = table;
                    LblAbastecimento.Text = "Abastecimento - " + DgvAbastecimento.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private async void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            await ListRegisterAsync("%" + TxtPesquisa.Text.Trim() + "%", 300);
        }

        private void DgvAbastecimento_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

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
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }

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

            UCDescricao.TxtDescricao.Text = DgvAbastecimento.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
            UCValor.TxtValor.Text = decimal.Parse(DgvAbastecimento.Rows[e.RowIndex].Cells["Valor"].Value.ToString()).ToString("#,##0.00");
            TxtLitros.Text = decimal.Parse(DgvAbastecimento.Rows[e.RowIndex].Cells["Litros"].Value.ToString()).ToString("#,##0.00");
            MktData.Text = DgvAbastecimento.Rows[e.RowIndex].Cells["Data"].Value.ToString();
            TxtKmInicial.Text = DgvAbastecimento.Rows[e.RowIndex].Cells["KmInicial"].Value.ToString();
            HabilitarBotoes.DefinirModoEdicao(true, BtnAlterar, BtnExcluir, BtnGravar);
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
            TxtLitros.Text = FormatarValor.Valor(TxtLitros.Text.Trim());
            TxtLitros.Select(TxtLitros.Text.Length, 0);
        }

        private void TxtLitros_Leave(object sender, EventArgs e)
        {
            TxtLitros.Text = FormatarValor.Zero(TxtLitros.Text.Trim());
            TxtLitros.Text = FormatarValor.ParaValor(TxtLitros.Text.Trim());
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
                controleAbastecimento.Litros = decimal.Parse(TxtLitros.Text.Trim());
                controleAbastecimento.Descricao = UCDescricao.Descricao;

                switch (opc)
                {
                    case 'I':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Insert.Register(controleAbastecimento);
                        break;
                    case 'U':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Update.Register(controleAbastecimento);
                        break;
                    case 'D':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Delete.Register(controleAbastecimento);
                        break;

                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                UCValor.TxtValor.Text = "0,00";
                TxtLitros.Text = "0,00";
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void FrmContCombustivel_Load(object sender, EventArgs e)
        {
            await ListRegisterAsync("%%");
        }
    }
}
