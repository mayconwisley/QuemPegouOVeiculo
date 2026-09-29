using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmContMulta : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public FrmContMulta()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        ControleMultaObj controleMulta;
        int idCtrlMulta = 0, idVeiculo = 0, idMotorista = 0;

        private async void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            await ListRegisterAsync("%" + TxtPesquisa.Text.Trim() + "%", 300);
        }

        private void ListRegister(string search)
        {
            try
            {
                DgvMultas.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.Register(search);
                LblMultas.Text = "Multas - " + DgvMultas.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task ListRegisterAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvMultas.DataSource = table;
                    LblMultas.Text = "Multas - " + DgvMultas.Rows.Count.ToString("000");
                },
                delayMilliseconds);

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
            if (e.RowIndex < 0)
                return;

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
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                UCValor.TxtValor.Text = "0,00";
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
                UCValor.TxtValor.Text = "0,00";
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
                return;
            }



            UCDescricao.TxtDescricao.Text = DgvMultas.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
            UCValor.TxtValor.Text = decimal.Parse(DgvMultas.Rows[e.RowIndex].Cells["Valor"].Value.ToString()).ToString("#,##0.00");

            MktData.Text = DgvMultas.Rows[e.RowIndex].Cells["Data"].Value.ToString();
            TxtPontos.Text = DgvMultas.Rows[e.RowIndex].Cells["Pontos"].Value.ToString();

            HabilitarBotoes.DefinirModoEdicao(true, BtnAlterar, BtnExcluir, BtnGravar);
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
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Insert.Register(controleMulta);
                        break;
                    case 'U':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Update.Register(controleMulta);
                        break;
                    case 'D':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Delete.Register(controleMulta);
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
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void FrmContMulta_Load(object sender, EventArgs e)
        {
            await ListRegisterAsync("%%");
        }
    }
}
