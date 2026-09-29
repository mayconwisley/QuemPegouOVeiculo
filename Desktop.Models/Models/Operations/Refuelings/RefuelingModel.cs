using System;

namespace FleetManagement.Desktop.Models
{
    public class RefuelingModel
    {
        public int Id { get; set; }
        public VehicleModel Vehicle { get; set; }
        public DriverModel Driver { get; set; }
        public string InitialMileage { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Liters { get; set; }
        public string Description { get; set; }

    }
}
