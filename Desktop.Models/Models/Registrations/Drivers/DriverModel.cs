using System;

namespace FleetManagement.Desktop.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiration { get; set; }
        public string LicenseCategory { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public char Active { get; set; }

    }
}
