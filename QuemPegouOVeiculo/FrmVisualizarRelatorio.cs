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
                    dtRelatorio = Negocio.Veiculo.Query.RegisterVehicleStaus(search);
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
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Relatorios\RelVeiculo.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 2: //Listas Cadastro de Motoristas
                    dtRelatorio = new DataTable();
                    dtRelatorio = Negocio.Motorista.Query.RegisterDriverActive(search);
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
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Relatorios\RelMotorista.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 3: //Listas Status Veiculos
                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = Negocio.Controle.StatusVeiculo.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = Negocio.Controle.StatusVeiculo.Query.RegisterVehicle(idVeiculo);
                        search = "Veículos";
                    }
                    else if (search == "DTIni")
                    {
                        dtRelatorio = Negocio.Controle.StatusVeiculo.Query.RegisterDateStart(dateTime, dateTime1);
                        search = "Período - Por data de Início - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTFin")
                    {
                        dtRelatorio = Negocio.Controle.StatusVeiculo.Query.RegisterDateFinal(dateTime, dateTime1);
                        search = " Período - Por data de Final - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "NULL")
                    {
                        dtRelatorio = Negocio.Controle.StatusVeiculo.Query.RegisterDateFinalNull();
                        search = "Todas com final nulo";
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("StatusVeiculoDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Relatorios\RelStatusVeiculo.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 4: //Listas Controle de Veiculo
                    dtRelatorio = new DataTable();


                    if (search == "ALL")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterVehicle(idVeiculo);
                        search = "Veículos";
                    }
                    else if (search == "MOT")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDriver(idMotorista);
                        search = "Motoristas";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterVehicleDriver(idVeiculo, idMotorista);
                        search = "Veículo e Motorista";
                    }
                    else if (search == "DTSai")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDateArrival(dateTime, dateTime1);
                        search = "Período - Por data de Saída - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTChe")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDateExit(dateTime, dateTime1);
                        search = " Período - Por data de Chegada - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTSaiVei")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDateArrivalVehicle(dateTime, dateTime1, idVeiculo);
                        search = "Período - Por data de Saída - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTCheVei")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDateExitVehicle(dateTime, dateTime1, idVeiculo);
                        search = " Período - Por data de Chegada - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTSaiMot")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDateArrivalDriver(dateTime, dateTime1, idMotorista);
                        search = "Período - Por data de Saída - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTCheMot")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDateExitDriver(dateTime, dateTime1, idMotorista);
                        search = " Período - Por data de Chegada - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "NULL")
                    {
                        dtRelatorio = Negocio.Controle.Veiculo.Query.RegisterDateExitNull();
                        search = "Todas com chegada nulo";
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleVeiculoDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Relatorios\RelControleVeiculo.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 5: //Lista Manutenção

                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = Negocio.Controle.Mecanica.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = Negocio.Controle.Mecanica.Query.RegisterVehicle(idVeiculo);
                        search = "Veículo";
                    }
                    else if (search == "DTPer")
                    {
                        dtRelatorio = Negocio.Controle.Mecanica.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleManutencaoDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Relatorios\RelControleManutencao.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 6: //Lista Combustivel

                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = Negocio.Controle.Combustivel.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = Negocio.Controle.Combustivel.Query.RegisterVehicle(idVeiculo);
                        search = "Veículo";
                    }
                    else if (search == "MOT")
                    {
                        dtRelatorio = Negocio.Controle.Combustivel.Query.RegisterDriver(idMotorista);
                        search = "Motorista";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtRelatorio = Negocio.Controle.Combustivel.Query.RegisterVehicleDriver(idVeiculo, idMotorista);
                        search = "Veiculo e Motorista";
                    }
                    else if (search == "DTPer")
                    {
                        dtRelatorio = Negocio.Controle.Combustivel.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerVei")
                    {
                        dtRelatorio = Negocio.Controle.Combustivel.Query.RegisterPeriodVehicle(dateTime, dateTime1, idVeiculo);
                        search = "Período - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerMot")
                    {
                        dtRelatorio = Negocio.Controle.Combustivel.Query.RegisterPeriodDriver(dateTime, dateTime1, idMotorista);
                        search = "Período - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleCombustivelDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Relatorios\RelControleCombustivel.rdlc";
                    this.RptVisualizadorRelatorio.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 7: //Lista Multa

                    dtRelatorio = new DataTable();
                    if (search == "ALL")
                    {
                        dtRelatorio = Negocio.Controle.Multa.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtRelatorio = Negocio.Controle.Multa.Query.RegisterVehicle(idVeiculo);
                        search = "Veículo";
                    }
                    else if (search == "MOT")
                    {
                        dtRelatorio = Negocio.Controle.Multa.Query.RegisterDriver(idMotorista);
                        search = "Motorista";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtRelatorio = Negocio.Controle.Multa.Query.RegisterVehicleDriver(idVeiculo, idMotorista);
                        search = "Veiculo e Motorista";
                    }
                    else if (search == "DTPer")
                    {
                        dtRelatorio = Negocio.Controle.Multa.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerVei")
                    {
                        dtRelatorio = Negocio.Controle.Multa.Query.RegisterPeriodVehicle(dateTime, dateTime1, idVeiculo);
                        search = "Período - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerMot")
                    {
                        dtRelatorio = Negocio.Controle.Multa.Query.RegisterPeriodDriver(dateTime, dateTime1, idMotorista);
                        search = "Período - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Clear();
                    this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(new ReportDataSource("ControleMultaDataSet", dtRelatorio));
                    this.RptVisualizadorRelatorio.LocalReport.ReportPath = @"Relatorios\RelControleMulta.rdlc";
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
            ListarRelatorio(strSearch);
        }
    }
}
