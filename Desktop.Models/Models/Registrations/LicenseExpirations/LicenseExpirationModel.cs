using System;

namespace FleetManagement.Desktop.Models
{
    public class LicenseExpirationModel
    {
        public int Id { get; set; }
        public DriverModel Driver { get; set; }
        public DateTime Date { get; set; }
        public char Status { get; set; }

    }
}
