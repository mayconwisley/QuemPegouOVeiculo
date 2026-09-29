using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace FleetManagement
{
    public partial class ReportViewerForm : Form
    {
        string strSearch;
        DataTable dtReport;
        int operation = 0, vehicleId = 0, driverId = 0;
        DateTime dateTime, dateTime1;
        public ReportViewerForm()
        {
            InitializeComponent();
        }

        public ReportViewerForm(string search, int iOpc)
        {
            InitializeComponent();
            strSearch = search;
            operation = iOpc;
        }

        public ReportViewerForm(string search, int iOpc, int iVehicleDriver)
        {
            InitializeComponent();
            strSearch = search;
            operation = iOpc;
            vehicleId = iVehicleDriver;
            driverId = iVehicleDriver;
        }
        public ReportViewerForm(string search, int iOpc, int iVehicle, int iDriver)
        {
            InitializeComponent();
            strSearch = search;
            operation = iOpc;
            vehicleId = iVehicle;
            driverId = iDriver;
        }
        public ReportViewerForm(string search, int iOpc, DateTime dtDateTime, DateTime dtDateTime1)
        {
            InitializeComponent();
            strSearch = search;
            operation = iOpc;
            dateTime = dtDateTime;
            dateTime1 = dtDateTime1;
        }


        public ReportViewerForm(string search, int iOpc, DateTime dtDateTime, DateTime dtDateTime1, int iVehicleDriver)
        {
            InitializeComponent();
            strSearch = search;
            operation = iOpc;
            dateTime = dtDateTime;
            dateTime1 = dtDateTime1;
            vehicleId = iVehicleDriver;
            driverId = iVehicleDriver;
        }
        private void ListReport(string search)
        {

            switch (operation)
            {
                case 1: // Listar Cadastro de Veiculos
                    dtReport = new DataTable();
                    dtReport = FleetManagement.Desktop.Client.Features.Registrations.Vehicles.Query.RegisterVehicleStaus(search);
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


                    this.ReportViewerControl.LocalReport.DataSources.Clear();
                    this.ReportViewerControl.LocalReport.DataSources.Add(new ReportDataSource("VehicleDataSet", dtReport));
                    this.ReportViewerControl.LocalReport.ReportPath = @"Reports\Templates\VehicleReport.rdlc";
                    this.ReportViewerControl.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 2: //Listas Cadastro de Motoristas
                    dtReport = new DataTable();
                    dtReport = FleetManagement.Desktop.Client.Features.Registrations.Drivers.Query.RegisterDriverActive(search);
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

                    this.ReportViewerControl.LocalReport.DataSources.Clear();
                    this.ReportViewerControl.LocalReport.DataSources.Add(new ReportDataSource("DriverDataSet", dtReport));
                    this.ReportViewerControl.LocalReport.ReportPath = @"Reports\Templates\DriverReport.rdlc";
                    this.ReportViewerControl.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 3: //Listas Status Veiculos
                    dtReport = new DataTable();
                    if (search == "ALL")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Query.RegisterVehicle(vehicleId);
                        search = "Veículos";
                    }
                    else if (search == "DTIni")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Query.RegisterDateStart(dateTime, dateTime1);
                        search = "Período - Por data de Início - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTFin")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Query.RegisterDateFinal(dateTime, dateTime1);
                        search = " Período - Por data de Final - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "NULL")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses.Query.RegisterDateFinalNull();
                        search = "Todas com final nulo";
                    }

                    this.ReportViewerControl.LocalReport.DataSources.Clear();
                    this.ReportViewerControl.LocalReport.DataSources.Add(new ReportDataSource("VehicleStatusDataSet", dtReport));
                    this.ReportViewerControl.LocalReport.ReportPath = @"Reports\Templates\VehicleStatusReport.rdlc";
                    this.ReportViewerControl.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 4: //Listas Controle de Veiculo
                    dtReport = new DataTable();


                    if (search == "ALL")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterVehicle(vehicleId);
                        search = "Veículos";
                    }
                    else if (search == "MOT")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDriver(driverId);
                        search = "Motoristas";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterVehicleDriver(vehicleId, driverId);
                        search = "Veículo e Motorista";
                    }
                    else if (search == "DTSai")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDateExit(dateTime, dateTime1);
                        search = "Período - Por data de Saída - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTChe")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDateArrival(dateTime, dateTime1);
                        search = " Período - Por data de Chegada - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTSaiVei")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDateExitVehicle(dateTime, dateTime1, vehicleId);
                        search = "Período - Por data de Saída - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTCheVei")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDateArrivalVehicle(dateTime, dateTime1, vehicleId);
                        search = " Período - Por data de Chegada - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTSaiMot")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDateExitDriver(dateTime, dateTime1, driverId);
                        search = "Período - Por data de Saída - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTCheMot")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDateArrivalDriver(dateTime, dateTime1, driverId);
                        search = " Período - Por data de Chegada - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "NULL")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Movements.Query.RegisterDateExitNull();
                        search = "Todas com chegada nulo";
                    }

                    this.ReportViewerControl.LocalReport.DataSources.Clear();
                    this.ReportViewerControl.LocalReport.DataSources.Add(new ReportDataSource("VehicleMovementDataSet", dtReport));
                    this.ReportViewerControl.LocalReport.ReportPath = @"Reports\Templates\VehicleMovementReport.rdlc";
                    this.ReportViewerControl.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 5: //Lista Manutenção

                    dtReport = new DataTable();
                    if (search == "ALL")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Maintenance.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Maintenance.Query.RegisterVehicle(vehicleId);
                        search = "Veículo";
                    }
                    else if (search == "DTPer")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Maintenance.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.ReportViewerControl.LocalReport.DataSources.Clear();
                    this.ReportViewerControl.LocalReport.DataSources.Add(new ReportDataSource("MaintenanceDataSet", dtReport));
                    this.ReportViewerControl.LocalReport.ReportPath = @"Reports\Templates\MaintenanceReport.rdlc";
                    this.ReportViewerControl.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 6: //Lista Combustivel

                    dtReport = new DataTable();
                    if (search == "ALL")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterVehicle(vehicleId);
                        search = "Veículo";
                    }
                    else if (search == "MOT")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterDriver(driverId);
                        search = "Motorista";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterVehicleDriver(vehicleId, driverId);
                        search = "Veiculo e Motorista";
                    }
                    else if (search == "DTPer")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerVei")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterPeriodVehicle(dateTime, dateTime1, vehicleId);
                        search = "Período - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerMot")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Refuelings.Query.RegisterPeriodDriver(dateTime, dateTime1, driverId);
                        search = "Período - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.ReportViewerControl.LocalReport.DataSources.Clear();
                    this.ReportViewerControl.LocalReport.DataSources.Add(new ReportDataSource("RefuelingDataSet", dtReport));
                    this.ReportViewerControl.LocalReport.ReportPath = @"Reports\Templates\RefuelingReport.rdlc";
                    this.ReportViewerControl.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                case 7: //Lista Multa

                    dtReport = new DataTable();
                    if (search == "ALL")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterAll();
                        search = "Todos";
                    }
                    else if (search == "VEI")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterVehicle(vehicleId);
                        search = "Veículo";
                    }
                    else if (search == "MOT")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterDriver(driverId);
                        search = "Motorista";
                    }
                    else if (search == "VEIMOT")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterVehicleDriver(vehicleId, driverId);
                        search = "Veiculo e Motorista";
                    }
                    else if (search == "DTPer")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterPeriod(dateTime, dateTime1);
                        search = "Período - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerVei")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterPeriodVehicle(dateTime, dateTime1, vehicleId);
                        search = "Período - Veículo - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }
                    else if (search == "DTPerMot")
                    {
                        dtReport = FleetManagement.Desktop.Client.Features.Operations.Fines.Query.RegisterPeriodDriver(dateTime, dateTime1, driverId);
                        search = "Período - Motorista - " + dateTime.ToString("dd/MM/yyyy") + " a " + dateTime1.ToString("dd/MM/yyyy");
                    }

                    this.ReportViewerControl.LocalReport.DataSources.Clear();
                    this.ReportViewerControl.LocalReport.DataSources.Add(new ReportDataSource("FineDataSet", dtReport));
                    this.ReportViewerControl.LocalReport.ReportPath = @"Reports\Templates\FineReport.rdlc";
                    this.ReportViewerControl.LocalReport.SetParameters(new ReportParameter("Search", search));

                    break;
                default:
                    break;
            }

            this.ReportViewerControl.SetDisplayMode(DisplayMode.PrintLayout);
            this.ReportViewerControl.ZoomMode = ZoomMode.Percent;
            this.ReportViewerControl.ZoomPercent = 100;
            this.ReportViewerControl.RefreshReport();
        }

        private void ReportViewerForm_Load(object sender, EventArgs e)
        {
            try
            {
                ListReport(strSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao carregar relatório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
