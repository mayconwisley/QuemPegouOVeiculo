using System;

namespace FleetManagement.Desktop.Models.Reports
{
    public class RefuelingReportModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Model { get; set; }
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string InitialMileage { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Liters { get; set; }
        public string Description { get; set; }
    }
}
