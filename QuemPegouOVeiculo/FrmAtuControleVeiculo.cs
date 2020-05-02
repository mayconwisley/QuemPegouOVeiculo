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
        public FrmAtuControleVeiculo()
        {
            InitializeComponent();
        }
        private void ListRegister()
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
    }
}
