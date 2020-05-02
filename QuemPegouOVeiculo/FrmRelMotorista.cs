using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmRelMotorista : Form
    {
        bool abrir = true;
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
            visualizarRelatorio.MdiParent = FrmPrincipal.ActiveForm;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmVisualizarRelatorio)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                visualizarRelatorio.Show();
            }
        }

        private void FrmRelMotorista_Load(object sender, EventArgs e)
        {
            CbxListMotorista.SelectedIndex = 0;
        }
    }
}
