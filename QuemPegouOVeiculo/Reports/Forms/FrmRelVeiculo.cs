using System;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class FrmRelVeiculo : QuemPegouOVeiculo.Shared.Presentation.ThemedForm
    {
        public FrmRelVeiculo()
        {
            InitializeComponent();
        }
        string search;
        private void BtnListar_Click(object sender, EventArgs e)
        {


            FrmVisualizarRelatorio visualizarRelatorio = new FrmVisualizarRelatorio(search, 1);
            MdiForms.AbrirSubstituindo(this, visualizarRelatorio);
        }

        private void CbxListVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CbxListVeiculo.SelectedIndex)
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

        private void FrmRelVeiculo_Load(object sender, EventArgs e)
        {
            CbxListVeiculo.SelectedIndex = 0;
        }
    }
}
