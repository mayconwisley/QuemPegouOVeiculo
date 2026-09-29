using System;

namespace FleetManagement.Desktop.Models
{
    public class FineModel
    {
        public int Id { get; set; }
        public VehicleModel Vehicle { get; set; }
        public DriverModel Driver { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }

    }
}
