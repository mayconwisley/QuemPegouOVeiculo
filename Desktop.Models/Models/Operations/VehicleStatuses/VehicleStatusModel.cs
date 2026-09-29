using System;

namespace FleetManagement.Desktop.Models
{
    public class VehicleStatusModel
    {
        public int Id { get; set; }
        public VehicleModel Vehicle { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        public string Description { get; set; }

    }
}
