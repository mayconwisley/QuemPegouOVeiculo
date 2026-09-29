using QuemPegouOVeiculo.Desktop.Models;
using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmAtuControleVeiculo_0 : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
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

        private bool Manipaulte(char opc)
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
                        QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Update.RegisterControl(controleVeiculo);
                        return true;
                    default:
                        MessageBox.Show("Opção não encontrada");
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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
            if (Manipaulte('U'))
                Close();
        }

        private void FrmAtuControleVeiculo_0_Load(object sender, EventArgs e)
        {
            MktDtChegada.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }


    }
}
