using Objeto;
using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadStatusVeic : Form
    {
        public FrmCadStatusVeic()
        {
            InitializeComponent();
        }

        StatusVeiculoObj statusVeiculo;
        int idStatus = 0, idVeiculo = 0;
        DateTime dtDataHoraInicio, dtDataHoraFinal;

        private void ListResgister(string search)
        {
            try
            {
                DgvVeiculo.DataSource = Negocio.Controle.StatusVeiculo.Query.Register(search);
                LblStatus.Text = "Status - " + DgvVeiculo.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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
                        Negocio.Controle.StatusVeiculo.Insert.Register(statusVeiculo);
                        break;
                    case 'U':
                        Negocio.Controle.StatusVeiculo.Update.Register(statusVeiculo);
                        break;
                    case 'D':
                        Negocio.Controle.StatusVeiculo.Delete.Register(statusVeiculo);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                ListResgister("%" + TxtPesquisa.Text.Trim() + "%");

                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);
                UCDescricao.TxtDescricao.Clear();
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);

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

        private void FrmCadStatusVeic_Load(object sender, EventArgs e)
        {
            ListResgister("%%");
        }

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            ListResgister("%" + TxtPesquisa.Text.Trim() + "%");
        }

        private void LkLblDtFinalAtual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MktDtFinal.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
