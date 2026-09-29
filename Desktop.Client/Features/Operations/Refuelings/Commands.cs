using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Operations.Refuelings
{
    public static class Insert { public static bool Register(RefuelingModel x) => ApiClient.Save("refuelings", 0, 'I', RequestBodies.Refueling(x)); }
    public static class Update { public static bool Register(RefuelingModel x) => ApiClient.Save("refuelings", x.Id, 'U', RequestBodies.Refueling(x)); }
    public static class Delete { public static bool Register(RefuelingModel x) => ApiClient.Save("refuelings", x.Id, 'D'); }
}
