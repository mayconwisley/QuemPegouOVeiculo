using Objeto;
using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadVeiculo : Form
    {
        public FrmCadVeiculo()
        {
            InitializeComponent();
        }
        VeiculoObj veiculo;
        int idVeiculo = 0;

        private void ListResgister(string search)
        {
            try
            {
                DgvVeiculo.DataSource = Negocio.Veiculo.Query.Register(search);
                LblVeiculos.Text = "Veiculos - " + DgvVeiculo.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Manipulate(char opc)
        {
            veiculo = new VeiculoObj();

            try
            {
                veiculo.Id = idVeiculo;
                veiculo.Placa = TxtPlaca.Text.Trim();
                veiculo.Modelo = TxtModelo.Text.Trim();
                veiculo.Chassi = TxtChassi.Text.Trim();
                veiculo.Renavam = TxtRenavam.Text.Trim();

                if (CbAtivo.Checked)
                {
                    veiculo.Status = 'A';
                }
                else
                {
                    veiculo.Status = 'D';
                }

                switch (opc)
                {
                    case 'I':
                        Negocio.Veiculo.Insert.Register(veiculo);
                        break;
                    case 'U':
                        Negocio.Veiculo.Update.Register(veiculo);
                        break;
                    case 'D':
                        Negocio.Veiculo.Delete.Register(veiculo);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
                ListResgister("%" + TxtPesquisa.Text.Trim() + "%");
                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmCadVeiculo_Load(object sender, EventArgs e)
        {
            ListResgister("%%");
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

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            ListResgister("%" + TxtPesquisa.Text.Trim() + "%");
        }

        private void DgvVeiculo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idVeiculo = int.Parse(DgvVeiculo.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            TxtPlaca.Text = DgvVeiculo.Rows[e.RowIndex].Cells["Placa"].Value.ToString();
            TxtModelo.Text = DgvVeiculo.Rows[e.RowIndex].Cells["Modelo"].Value.ToString();
            TxtChassi.Text = DgvVeiculo.Rows[e.RowIndex].Cells["Chassi"].Value.ToString();
            TxtRenavam.Text = DgvVeiculo.Rows[e.RowIndex].Cells["Renavam"].Value.ToString();
            string status = DgvVeiculo.Rows[e.RowIndex].Cells["Status"].Value.ToString();

            if (status.Trim() == "Ativo")
            {
                CbAtivo.Checked = true;
            }
            else
            {
                CbAtivo.Checked = false;
            }
            Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(true, BtnAlterar, BtnExcluir, BtnGravar);
        }
    }
}
