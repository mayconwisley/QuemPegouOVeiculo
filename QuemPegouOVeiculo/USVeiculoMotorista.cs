using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class USVeiculoMotorista : UserControl
    {
        Form form = new Form();
        public USVeiculoMotorista()
        {
            InitializeComponent();
        }


        public USVeiculoMotorista(Form form)
        {
            InitializeComponent();
            this.form = form;
        }

        /*Listar Id e Nome dos Motoristas ativos*/
        private void ListIdAndNameActiveMotorista()
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

        #region Retornar os valores selecionado no ComboBox Motorista
        private int idMotorista;
        public int IdMotorista
        {
            get
            {
                return idMotorista;
            }
        }
        private string nomeMotorista;
        public string NomeMotorista
        {
            get
            {
                return nomeMotorista;
            }
        }
        #endregion

        #region Retornar os valores selecionado no ComboBox Veiculos
        private int idVeiculo;
        public int IdVeiculo
        {
            get
            {
                return idVeiculo;
            }
        }
        private string modeloVeiculo;
        public string ModeloVeiculo
        {
            get
            {
                return modeloVeiculo;
            }
        }

        private string kmFinalVeiculo;
        public string KmFinalVeiculo
        {
            get
            {
                return kmFinalVeiculo;
            }
        }
        #endregion

        public delegate void KmFimVeiculo(string kmfinal);
        public event KmFimVeiculo KmFinal;

        protected virtual void OnKmFinal()
        {
            if (KmFinal != null)
            {
                KmFinal(kmFinalVeiculo);
            }
        }

        private void USVeiculoMotorista_Load(object sender, EventArgs e)
        {
            ListIdAndModelActiveVeiculo();
            ListIdAndNameActiveMotorista();
        }

        public void CbxVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxVeiculo.SelectedValue != null)
            {
                idVeiculo = int.Parse(CbxVeiculo.SelectedValue.ToString());
                modeloVeiculo = CbxVeiculo.Text.ToString();
            }
            else
            {
                return;
            }

            try
            {
                if (this.form.Name == "FrmContCombustivel")
                {
                    kmFinalVeiculo = Negocio.Controle.Combustivel.Query.UltimoKmVeiculo(idVeiculo);
                }
                else
                {
                    kmFinalVeiculo = Negocio.Veiculo.Query.UltimoKmVeiculo(idVeiculo);
                }

                OnKmFinal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CbxMotorista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxMotorista.SelectedValue != null)
            {
                idMotorista = int.Parse(CbxMotorista.SelectedValue.ToString());
                nomeMotorista = CbxMotorista.Text.ToString();
            }
        }
    }
}
