using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuemPegouOVeiculo.Shared.Presentation;

namespace QuemPegouOVeiculo
{
    public partial class USVeiculoMotorista : UserControl
    {
        private readonly AsyncDataLoader<DataTable> veiculosLoader;
        private readonly AsyncDataLoader<DataTable> motoristasLoader;
        private readonly AsyncDataLoader<string> quilometragemLoader;
        private Form form;

        public USVeiculoMotorista()
        {
            InitializeComponent();
            veiculosLoader = new AsyncDataLoader<DataTable>(this);
            motoristasLoader = new AsyncDataLoader<DataTable>(this);
            quilometragemLoader = new AsyncDataLoader<string>(this);
            Disposed += DisposeLoaders;
        }


        public USVeiculoMotorista(Form form)
            : this()
        {
            this.form = form;
        }

        private void DisposeLoaders(object sender, EventArgs args)
        {
            veiculosLoader.Dispose();
            motoristasLoader.Dispose();
            quilometragemLoader.Dispose();
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

        private async void USVeiculoMotorista_Load(object sender, EventArgs e)
        {
            await Task.WhenAll(
                veiculosLoader.LoadAsync(
                    cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Veiculos.Query
                        .IdAndModelActiveAsync(cancellationToken),
                    table => CbxVeiculo.DataSource = table),
                motoristasLoader.LoadAsync(
                    cancellationToken => QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Query
                        .IdAndNameActiveAsync(cancellationToken),
                    table => CbxMotorista.DataSource = table));
        }

        public async void CbxVeiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxVeiculo.SelectedValue == null
                || !int.TryParse(CbxVeiculo.SelectedValue.ToString(), out idVeiculo))
                return;

            modeloVeiculo = CbxVeiculo.Text.ToString();
            var veiculoId = idVeiculo;
            var ownerForm = form ?? FindForm();

            await quilometragemLoader.LoadAsync(
                cancellationToken => ownerForm?.Name == "FrmContCombustivel"
                    ? QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query
                        .UltimoKmVeiculoAsync(veiculoId, cancellationToken)
                    : QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Veiculos.Query
                        .UltimoKmVeiculoAsync(veiculoId, cancellationToken),
                quilometragem =>
                {
                    kmFinalVeiculo = quilometragem;
                    OnKmFinal();
                });
        }

        private void CbxMotorista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbxMotorista.SelectedValue != null
                && int.TryParse(CbxMotorista.SelectedValue.ToString(), out idMotorista))
            {
                nomeMotorista = CbxMotorista.Text.ToString();
            }
        }
    }
}
