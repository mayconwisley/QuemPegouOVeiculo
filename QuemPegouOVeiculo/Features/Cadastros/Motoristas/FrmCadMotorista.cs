using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmCadMotorista : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
    {
        private readonly AsyncDataLoader<DataTable> listLoader;

        public FrmCadMotorista()
        {
            InitializeComponent();
            listLoader = new AsyncDataLoader<DataTable>(this);
            Disposed += (sender, args) => listLoader.Dispose();
        }
        MotoristaObj motorista = null;
        int idMotorista = 0;

        private void ListRegister(string search)
        {
            try
            {
                DgvMotoristas.DataSource = QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Query.Register(search);
                LblMotorista.Text = "Motoristas - " + DgvMotoristas.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Task ListRegisterAsync(string search, int delayMilliseconds = 0) =>
            listLoader.LoadAsync(
                cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Query.RegisterAsync(search, cancellationToken),
                table =>
                {
                    DgvMotoristas.DataSource = table;
                    LblMotorista.Text = "Motoristas - " + DgvMotoristas.Rows.Count.ToString("000");
                },
                delayMilliseconds);

        private void Manipulate(char opc)
        {
            motorista = new MotoristaObj();

            try
            {
                if (opc == 'D')
                {
                    QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Delete.Register(new MotoristaObj { Id = idMotorista });
                    ListRegister("%" + TxtPesquisa.Text.Trim() + "%");
                    HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);
                    return;
                }
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
                bool validarCPF = QuemPegouOVeiculo.Desktop.Client.Validation.ValidarCPF.CPF(cpf);
                if (!validarCPF)
                {
                    MessageBox.Show("CPF inválido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                motorista.CPF = cpf;

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
                        QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Insert.Register(motorista);
                        break;
                    case 'U':
                        QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Update.Register(motorista);
                        break;
                    case 'D':
                        QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Delete.Register(motorista);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }

                /*Listar registros após cada Insert, Update, Delete*/
                ListRegister("%" + TxtPesquisa.Text.Trim() + "%");

                /*Limpar os campos*/
                LimparCampos.LimparTextBox(this.Controls);
                LimparCampos.LimparMaskedTextBox(this.Controls);

                /*Librar BtnGravar, e desativar os BtnAlterar, BtnExcluir*/
                HabilitarBotoes.DefinirModoEdicao(false, BtnAlterar, BtnExcluir, BtnGravar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void FrmCadMotorista_Load(object sender, EventArgs e)
        {
            await ListRegisterAsync("%%");
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
            if (e.RowIndex < 0)
                return;

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
            HabilitarBotoes.DefinirModoEdicao(true, BtnAlterar, BtnExcluir, BtnGravar);
        }

        private async void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            await ListRegisterAsync("%" + TxtPesquisa.Text.Trim() + "%", 300);
        }
    }
}
