using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Operations.Maintenance
{
    public static class Insert { public static bool Register(MaintenanceModel x) => ApiClient.Save("maintenance", 0, 'I', RequestBodies.Maintenance(x)); }
    public static class Update { public static bool Register(MaintenanceModel x) => ApiClient.Save("maintenance", x.Id, 'U', RequestBodies.Maintenance(x)); }
    public static class Delete { public static bool Register(MaintenanceModel x) => ApiClient.Save("maintenance", x.Id, 'D'); }
}
