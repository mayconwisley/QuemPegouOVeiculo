using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Registrations.Vehicles
{
    public static class Insert { public static bool Register(VehicleModel x) => ApiClient.Save("vehicles", 0, 'I', RequestBodies.Vehicle(x)); }
    public static class Update { public static bool Register(VehicleModel x) => ApiClient.Save("vehicles", x.Id, 'U', RequestBodies.Vehicle(x)); }
    public static class Delete { public static bool Register(VehicleModel x) => ApiClient.Save("vehicles", x.Id, 'D'); }
}
