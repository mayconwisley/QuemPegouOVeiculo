using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuemPegouOVeiculo
{
    public partial class FrmVisualizarRelatorio : Form
    {
        string strSearch;
        DataTable dtRelatorio;
        int opc = 0, idVeiculo = 0, idMotorista = 0;
        DateTime dateTime, dateTime1;
        public FrmVisualizarRelatorio()
        {
            InitializeComponent();
        }

        public FrmVisualizarRelatorio(string search, int iOpc)
        {
            InitializeComponent();
            strSearch = search;
            opc = iOpc;
        }

        public FrmVisualizarRelatorio(string search, int iOpc, int iVeiculoMotorista)
        {
            InitializeComponent();
            strSearch = search;
            opc = iOpc;
            idVeiculo = iVeiculoMotorista;
            idMotorista = iVeiculoMotorista;
        }
        public FrmVisualizarRelatorio(string search, int iOpc, int iVeiculo, int iMotorista)
        {
            InitializeComponent();
            strSearch = search;
            opc = iOpc;
            idVeiculo = iVeiculo;
            idMotorista = iMotorista;
        }
        public FrmVisualizarRelatorio(string search, int iOpc, DateTime dtDateTime, DateTime dtDateTime1)
        {
            InitializeComponent();
            strSearch = search;
            opc = iOpc;
            dateTime = dtDateTime;
            dateTime1 = dtDateTime1;
        }


        public FrmVisualizarRelatorio(string search, int iOpc, DateTime dtDateTime, DateTime dtDateTime1, int iVeiculoMotorista)
        {
            InitializeComponent();
            strSearch = search;
            opc = iOpc;
            dateTime = dtDateTime;
            dateTime1 = dtDateTime1;
            idVeiculo = iVeiculoMotorista;
            idMotorista = iVeiculoMotorista;
        }
        private void ListarRelatorio(string search)
        {

            switch (opc)
            {
                case 1: // Listar Cadastro de Veiculos
                    dtRelatorio = new DataTable();
                    dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Veiculos.Query.RegisterVehicleStaus(search);
                    if (search == "%")
                    {
                        search = "Todos";
                    }
                    else if (search == "A")
                    {
                        search = "Ativos";
                    }
                    else if (search == "D")
                    {
                        search = "Desativados";
                    }


                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("VeiculoDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Reports\Templates\RelVeiculo.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 2: //Listas Cadastro de Motoristas
                    dtRelatorio = new DataTable();
                    dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Cadastros.Motoristas.Query.RegisterDriverActive(search);
                    if (search == "%")
                    {
                        search = "Todos";
                    }
                    else if (search == "A")
                    {
                        search = "Ativos";
                    }
                    else if (search == "D")
                    {
                        search = "Desativados";
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("MotoristaDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Reports\Templates\RelMotorista.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 3: //Listas Status Veiculos
                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Query.RegisterVehicle(idVeiculo);
                        search = "Veículos";
                    }
                    else if (search == "DTIni")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Query.RegisterDateStart(dateTime, dateTime1);
                        search = "Período - Por data de Início - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTFin")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Query.RegisterDateFinal(dateTime, dateTime1);
                        search = " Período - Por data de Final - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "NULL")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.StatusVeiculos.Query.RegisterDateFinalNull();
                        search = "Todas com final nulo";
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("StatusVeiculoDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Reports\Templates\RelStatusVeiculo.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 4: //Listas Controle de Veiculo
                    dtRelatorio = new DataTable();


                    if (search == "ALL")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterVehicle(idVeiculo);
                        search = "Veículos";
                    }
                    else if (search == "MOT")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDriver(idMotorista);
                        search = "Motoristas";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterVehicleDriver(idVeiculo, idMotorista);
                        search = "Veículo e Motorista";
                    }
                    else if (search == "DTSai")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDateArrival(dateTime, dateTime1);
                        search = "Período - Por data de Saída - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTChe")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDateExit(dateTime, dateTime1);
                        search = " Período - Por data de Chegada - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTSaiVei")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDateArrivalVehicle(dateTime, dateTime1, idVeiculo);
                        search = "Período - Por data de Saída - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTCheVei")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDateExitVehicle(dateTime, dateTime1, idVeiculo);
                        search = " Período - Por data de Chegada - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTSaiMot")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDateArrivalDriver(dateTime, dateTime1, idMotorista);
                        search = "Período - Por data de Saída - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTCheMot")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDateExitDriver(dateTime, dateTime1, idMotorista);
                        search = " Período - Por data de Chegada - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "NULL")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Movimentacoes.Query.RegisterDateExitNull();
                        search = "Todas com chegada nulo";
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleVeiculoDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Reports\Templates\RelControleVeiculo.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 5: //Lista Manutenção

                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes.Query.RegisterVehicle(idVeiculo);
                        search = "Veículo";
                    }
                    else if (search == "DTPer")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Manutencoes.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleManutencaoDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Reports\Templates\RelControleManutencao.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 6: //Lista Combustivel

                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterVehicle(idVeiculo);
                        search = "Veículo";
                    }
                    else if (search == "MOT")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterDriver(idMotorista);
                        search = "Motorista";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterVehicleDriver(idVeiculo, idMotorista);
                        search = "Veiculo e Motorista";
                    }
                    else if (search == "DTPer")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerVei")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterPeriodVehicle(dateTime, dateTime1, idVeiculo);
                        search = "Período - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerMot")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Abastecimentos.Query.RegisterPeriodDriver(dateTime, dateTime1, idMotorista);
                        search = "Período - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleCombustivelDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Reports\Templates\RelControleCombustivel.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 7: //Lista Multa

                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterVehicle(idVeiculo);
                        search = "Veículo";
                    }
                    else if (search == "MOT")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterDriver(idMotorista);
                        search = "Motorista";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterVehicleDriver(idVeiculo, idMotorista);
                        search = "Veiculo e Motorista";
                    }
                    else if (search == "DTPer")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerVei")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterPeriodVehicle(dateTime, dateTime1, idVeiculo);
                        search = "Período - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerMot")
                    {
                        dtRelatorio = QuemPegouOVeiculo.Desktop.Client.Features.Operacoes.Multas.Query.RegisterPeriodDriver(dateTime, dateTime1, idMotorista);
                        search = "Período - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleMultaDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Reports\Templates\RelControleMulta.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                default:
                    break;
            }

            this.RptVisualizadorRelatorio.SetDisplayMode(DisplayMode.PrintLayout);
            this.RptVisualizadorRelatorio.ZoomMode = ZoomMode.Percent;
            this.RptVisualizadorRelatorio.ZoomPercent = 100;
            this.RptVisualizadorRelatorio.RefreshReport();
        }

        private void FrmVisualizarRelatorio_Load(object sender, EventArgs e)
        {
            try
            {
                ListarRelatorio(strSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao carregar relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
