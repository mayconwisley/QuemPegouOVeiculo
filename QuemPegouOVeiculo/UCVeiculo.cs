using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class UCVeiculo : UserControl
    {
        public UCVeiculo()
        {
            InitializeComponent();
        }

        /*Listar Id e Modelo dos Veiculos ativos*/

        private void ListIdAndModelActiveVeiculo()
        {
            try
            {
                CbxVeiculo.DataSource = Negocio.Veiculo.Query.IdAndModelActive();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Retornar os valores selecionado no ComboBox Veiculos
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        private string modelo;
        public string Modelo
        {
            get
            {
                return modelo;
            }
        }
        #endregion

        private void UCVeiculo_Load(object sender, EventArgs e)
        {
            ListIdAndModelActiveVeiculo();
        }

        private void CbxVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxVeiculo.SelectedValue != null)
            {
                id = int.Parse(CbxVeiculo.SelectedValue.ToString());
                modelo = CbxVeiculo.Text.ToString();
            }
            else
            {
                return;
            }

        }
    }
}
