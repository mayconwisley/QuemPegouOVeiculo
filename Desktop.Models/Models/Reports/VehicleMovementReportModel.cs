using System;

namespace FleetManagement.Desktop.Models.Reports
{
    public class VehicleMovementReportModel
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public DateTime DepartureAt { get; set; }
        public DateTime ArrivalAt { get; set; }
        public int Days { get; set; }
        public string Hours { get; set; }
        public string Description { get; set; }
        public string InitialMileage { get; set; }
        public string FinalMileage { get; set; }
        public string TotalMileage { get; set; }
        public string Status { get; set; }
    }
}
