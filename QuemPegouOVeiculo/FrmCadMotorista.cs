using Objeto;
using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadMotorista : Form
    {
        public FrmCadMotorista()
        {
            InitializeComponent();
        }
        MotoristaObj motorista = null;
        int idMotorista = 0;

        private void ListRegister(string search)
        {
            try
            {
                DgvMotoristas.DataSource = Negocio.Motorista.Query.Register(search);
                LblMotorista.Text = "Motoristas - " + DgvMotoristas.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Manipulate(char opc)
        {
            motorista = new MotoristaObj();

            try
            {
                motorista.Id = idMotorista;
                motorista.Nome = TxtNome.Text.Trim();
                motorista.CNH = TxtCNH.Text.Trim();

                /*Validar data de vencimento da CNH*/
                if (DateTime.Parse(MktVencCNH.Text).Date >= DateTime.Now.Date)
                {
                    motorista.VencimentoCNH = DateTime.Parse(MktVencCNH.Text.Trim());
                }
                else
                {
                    if (MessageBox.Show("CNH Vencida.\n\nDeseja cadastrar mesmo assim?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        motorista.VencimentoCNH = DateTime.Parse(MktVencCNH.Text.Trim());
                    }
                    else
                    {
                        return;
                    }
                }

                motorista.CategoriaCNH = TxtCategCNH.Text.Trim();

                /*Validar CPF*/
                string cpf = MktCPF.Text.Replace(".", "").Replace("-", "").Trim();
                bool validarCPF = Negocio.Utilitario.ValidarCPF.CPF(cpf);
                if (validarCPF == true)
                {
                    motorista.CPF = cpf;
                }
                else
                {
                    if (MessageBox.Show("CPF inválido.\n\nDeseja cadastrar mesmo assim?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        motorista.CPF = cpf;
                    }
                    else
                    {
                        return;
                    }
                }

                motorista.RG = TxtRG.Text.Trim();

                if (CbAtivo.Checked)
                {
                    motorista.Ativo = 'A';
                }
                else
                {
                    motorista.Ativo = 'D';
                }

                /*Insert, Update, Delete*/
                switch (opc)
                {
                    case 'I':
                        Negocio.Motorista.Insert.Register(motorista);
                        break;
                    case 'U':
                        Negocio.Motorista.Update.Register(motorista);
                        break;
                    case 'D':
                        Negocio.Motorista.Delete.Register(motorista);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                /*Listar registros após cada Insert, Update, Delete*/
                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");

                /*Limpar os campos*/
                Negocio.Utilitario.LimparCampos.LimparTextBox(this.Controls);
                Negocio.Utilitario.LimparCampos.LimparMaskedTextBox(this.Controls);

                /*Librar BtnGravar, e desativar os BtnAlterar, BtnExcluir*/
                Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(false, BtnAlterar, BtnExcluir, BtnGravar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmCadMotorista_Load(object sender, EventArgs e)
        {
            ListRegister("%%");
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

        private void DgvMotoristas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            idMotorista = int.Parse(DgvMotoristas.Rows[e.RowIndex].Cells["Id"].Value.ToString());
            TxtNome.Text = DgvMotoristas.Rows[e.RowIndex].Cells["Nome"].Value.ToString();
            TxtCNH.Text = DgvMotoristas.Rows[e.RowIndex].Cells["NumCNH"].Value.ToString();
            MktVencCNH.Text = DgvMotoristas.Rows[e.RowIndex].Cells["VencimentoCNH"].Value.ToString();
            TxtCategCNH.Text = DgvMotoristas.Rows[e.RowIndex].Cells["CategoriaCNH"].Value.ToString();
            MktCPF.Text = DgvMotoristas.Rows[e.RowIndex].Cells["CPF"].Value.ToString();
            TxtRG.Text = DgvMotoristas.Rows[e.RowIndex].Cells["RG"].Value.ToString();

            string ativo = DgvMotoristas.Rows[e.RowIndex].Cells["Ativo"].Value.ToString();
            if (ativo.Trim() == "Ativo")
            {
                CbAtivo.Checked = true;
            }
            else
            {
                CbAtivo.Checked = false;
            }
            /*Liberar os BtnAlterar e BtnExcluir. Desativar o BtnGravar*/
            Negocio.Utilitario.HabilitarBotoes.AlterarExcluir(true, BtnAlterar, BtnExcluir, BtnGravar);
        }

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
        }
    }
}
