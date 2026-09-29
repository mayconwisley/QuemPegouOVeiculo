using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadVencCNH : Form
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public FrmCadVencCNH()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
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

        private Task ListRegisterAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.VencimentosCnh.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvVencCNH.DataSource = table;
                    LblVencimento.Text = "Vencimento - " + DgvVencCNH.Rows.Count.ToString("000");
                },
                delayMilliseconds);

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

        private async void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            await ListRegisterAsync("%" + TxtPesquisa.Text.Trim() + "%", 300);
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

        private async void FrmCadVencCNH_Load(object sender, EventArgs e)
        {
            await ListRegisterAsync("%%");
        }
    }
}
