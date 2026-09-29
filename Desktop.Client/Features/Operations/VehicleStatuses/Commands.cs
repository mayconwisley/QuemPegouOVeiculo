using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses
{
    public static class Insert { public static bool Register(VehicleStatusModel x) => ApiClient.Save("vehicle-statuses", 0, 'I', RequestBodies.VehicleStatus(x)); }
    public static class Update { public static bool Register(VehicleStatusModel x) => ApiClient.Save("vehicle-statuses", x.Id, 'U', RequestBodies.VehicleStatus(x)); }
    public static class Delete { public static bool Register(VehicleStatusModel x) => ApiClient.Save("vehicle-statuses", x.Id, 'D'); }
}
