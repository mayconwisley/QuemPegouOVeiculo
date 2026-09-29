using System;

namespace FleetManagement.Desktop.Models
{
    public class VehicleMovementModel
    {
        public int Id { get; set; }
        public VehicleModel Vehicle { get; set; }
        public DriverModel Driver { get; set; }
        public DateTime? DepartureAt { get; set; }
        public DateTime? ArrivalAt { get; set; }
        public string Description { get; set; }
        public string InitialMileage { get; set; }
        public string FinalMileage { get; set; }
        public char Status { get; set; }



    }
}
