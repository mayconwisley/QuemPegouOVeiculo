using Objeto;
using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmAtuControleVeiculo_0 : Form
    {
        ControleVeiculoObj controleVeiculo;
        int idControle;
        DateTime dtDataHoraChegada;

        FrmAtuControleVeiculo form1 = null;

        public FrmAtuControleVeiculo_0()
        {
            InitializeComponent();
        }

        public FrmAtuControleVeiculo_0(int idCont)
        {
            InitializeComponent();
            idControle = idCont;
        }

        public FrmAtuControleVeiculo_0(int idCont, FrmAtuControleVeiculo form)
        {
            InitializeComponent();
            form1 = form;
            idControle = idCont;
        }

        private void Manipaulte(char opc)
        {
            controleVeiculo = new ControleVeiculoObj();
            try
            {
                DateTime.TryParse(MktDtChegada.Text.Trim(), out dtDataHoraChegada);
                controleVeiculo.Id = idControle;

                if (dtDataHoraChegada.Date == DateTime.Parse("01/01/0001 00:00:00"))
                {
                    controleVeiculo.DataHoraChegada = null;
                }
                else
                {
                    controleVeiculo.DataHoraChegada = dtDataHoraChegada;
                }
                controleVeiculo.KmFinal = TxtKmFinal.Text.Trim();


                switch (opc)
                {
                    case 'U':
                        Negocio.Controle.Veiculo.Update.RegisterControl(controleVeiculo);
                        break;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FrmAtuControleVeiculo_0_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                form1.ListRegister();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnGravar_Click(object sender, EventArgs e)
        {
            Manipaulte('U');
            Close();
        }

        private void FrmAtuControleVeiculo_0_Load(object sender, EventArgs e)
        {
            MktDtChegada.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }


    }
}
