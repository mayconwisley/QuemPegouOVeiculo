using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Registrations.Drivers
{
    public static class Insert { public static bool Register(DriverModel x) => ApiClient.Save("drivers", 0, 'I', RequestBodies.Driver(x)); }
    public static class Update { public static bool Register(DriverModel x) => ApiClient.Save("drivers", x.Id, 'U', RequestBodies.Driver(x)); }
    public static class Delete { public static bool Register(DriverModel x) => ApiClient.Save("drivers", x.Id, 'D'); }
}
