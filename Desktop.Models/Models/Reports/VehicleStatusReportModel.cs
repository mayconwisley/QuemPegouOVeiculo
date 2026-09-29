using System;

namespace FleetManagement.Desktop.Models.Reports
{
    public class VehicleStatusReportModel
    {

        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Model { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string Description { get; set; }
    }
}
