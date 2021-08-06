using System;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class UCMotorista : UserControl
    {
        public UCMotorista()
        {
            InitializeComponent();
        }

        /*Listar Id e Nome dos Motoristas ativos*/
        private void ListIdAndNameActive()
        {
            try
            {
                CbxMotorista.DataSource = Negocio.Motorista.Query.IdAndNameActive();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region Retornar os valores selecionado no ComboBox

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        private string nome;
        public string Nome
        {
            get
            {
                return nome;
            }
        }
        #endregion
        private void UCMotorista_Load(object sender, EventArgs e)
        {
            ListIdAndNameActive();
        }

        private void CbxMotorista_SelectedIndexChanged(object sender, EventArgs e)
        {

            id = int.Parse(CbxMotorista.SelectedValue.ToString());
            nome = CbxMotorista.Text.ToString();
        }
    }
}
