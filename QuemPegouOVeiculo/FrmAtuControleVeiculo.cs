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
    public partial class FrmAtuControleVeiculo : Form
    {
        int idCont;
        bool abrir = true;
        public FrmAtuControleVeiculo()
        {
            InitializeComponent();
        }
        public void ListRegister()
        {
            try
            {
                DgvControleVeiculo.DataSource = Negocio.Controle.Veiculo.Query.RegisterArrivalNull();
                lblInfo.Text = "Controle Veiculo - " + DgvControleVeiculo.Rows.Count.ToString("000");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmAtuControleVeiculo_Load(object sender, EventArgs e)
        {
            ListRegister();
        }

        private void DgvControleVeiculo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            idCont = int.Parse(DgvControleVeiculo.Rows[e.RowIndex].Cells["Id"].Value.ToString());



            FrmAtuControleVeiculo_0 controleVeiculo_0 = new FrmAtuControleVeiculo_0(idCont, this);
            controleVeiculo_0.MdiParent = FrmPrincipal.ActiveForm;
            foreach (Form item in Application.OpenForms)
            {
                if (item is FrmAtuControleVeiculo_0)
                {
                    item.Focus();
                    abrir = false;
                }
            }
            if (abrir)
            {
                controleVeiculo_0.Show();
            }
        }
    }
}
