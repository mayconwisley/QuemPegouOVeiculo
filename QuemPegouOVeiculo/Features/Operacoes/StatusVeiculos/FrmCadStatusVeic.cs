using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadStatusVeic : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public FrmCadStatusVeic()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }

        StatusVeiculoObj statusVeiculo;
        int idStatus = 0, idVeiculo = 0;
        DateTime dtDataHoraInicio, dtDataHoraFinal;

        private void ListResgister(string search)
        {
            try
            {
                DgvVeiculo.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Query.Register(search);
                LblStatus.Text = "Status - " + DgvVeiculo.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task ListResgisterAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvVeiculo.DataSource = table;
                    LblStatus.Text = "Status - " + DgvVeiculo.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private void Manipulate(char opc)
        {
            try
            {
                DateTime.TryParse(MktDtInicio.Text, out dtDataHoraInicio);
                DateTime.TryParse(MktDtFinal.Text, out dtDataHoraFinal);

                statusVeiculo = new StatusVeiculoObj();
                statusVeiculo.Id = idStatus;
                statusVeiculo.Veiculo = new VeiculoObj();
                statusVeiculo.Veiculo.Id = UCVeiculo.Id;


                if (dtDataHoraInicio.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    statusVeiculo.DataHoraInicial = null;
                }
                else
                {
                    statusVeiculo.DataHoraInicial = dtDataHoraInicio;
                }



                if (dtDataHoraFinal.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    statusVeiculo.DataHoraFinal = null;
                }
                else
                {
                    statusVeiculo.DataHoraFinal = dtDataHoraFinal;
                }

                statusVeiculo.Descricao = UCDescricao.Descricao;

                switch (opc)
                {
                    case 'I':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Insert.Register(statusVeiculo);
                        break;
                    case 'U':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Update.Register(statusVeiculo);
                        break;
                    case 'D':
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Delete.Register(statusVeiculo);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                ListResgister("%" + TxtPesquisa.Text.Trim() + "%");

                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LkLblDtInicioAtual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktDtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void DgvVeiculo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            idStatus = int.Parse(DgvVeiculo.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            idVeiculo = int.Parse(DgvVeiculo.Rows[e.RowIndex].Cells["Id_Veiculo"].Value.ToString());


            DateTime.TryParse(DgvVeiculo.Rows[e.RowIndex].Cells["DataHoraInicio"].Value.ToString(), out dtDataHoraInicio);
            DateTime.TryParse(DgvVeiculo.Rows[e.RowIndex].Cells["DataHoraFinal"].Value.ToString(), out dtDataHoraFinal);

            if (dtDataHoraFinal.Date == DateTime.Parse("01/01/0001 00:00:00"))
            {
                MktDtFinal.Text = "";
            }
            else
            {
                MktDtFinal.Text = dtDataHoraFinal.ToString("dd/MM/yyyy HH:mm");
            }

            MktDtInicio.Text = dtDataHoraInicio.ToString("dd/MM/yyyy HH:mm");
            UCDescricao.TxtDescricao.Text = DgvVeiculo.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
            UCVeiculo.CbxVeiculo.SelectedValue = idVeiculo;

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

        private async void FrmCadStatusVeic_Load(object sender, EventArgs e)
        {
            MktDtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            await ListResgisterAsync("%%");
        }

        private async void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            await ListResgisterAsync("%" + TxtPesquisa.Text.Trim() + "%", 300);
        }

        private void LkLblDtFinalAtual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktDtFinal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
