using System;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmRelMotorista : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
    {
        string search;

        public FrmRelMotorista()
        {
            InitializeComponent();
        }

        private void CbxListMotorista_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CbxListMotorista.SelectedIndex)
            {
                case 0:
                    search = "%";
                    break;
                case 1:
                    search = "A";
                    break;
                case 2:
                    search = "D";
                    break;
                default:
                    break;
            }


        }
        private void BtnListar_Click(object sender, EventArgs e)
        {

            FrmVisualizarRelatorio visualizarRelatorio = new FrmVisualizarRelatorio(search, 2);
            MdiForms.AbrirSubstituindo(this, visualizarRelatorio);
        }

        private void FrmRelMotorista_Load(object sender, EventArgs e)
        {
            CbxListMotorista.SelectedIndex = 0;
        }
    }
}
