using System;

namespace FleetManagement.Desktop.Models
{
    public class MaintenanceModel
    {
        public int Id { get; set; }
        public VehicleModel Vehicle { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

    }
}
