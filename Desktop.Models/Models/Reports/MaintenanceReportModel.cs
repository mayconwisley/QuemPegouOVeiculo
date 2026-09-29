using System;

namespace FleetManagement.Desktop.Models.Reports
{
    public class MaintenanceReportModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Model { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
