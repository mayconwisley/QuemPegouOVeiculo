using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Operations.Fines
{
    public static class Insert { public static bool Register(FineModel x) => ApiClient.Save("fines", 0, 'I', RequestBodies.Fine(x)); }
    public static class Update { public static bool Register(FineModel x) => ApiClient.Save("fines", x.Id, 'U', RequestBodies.Fine(x)); }
    public static class Delete { public static bool Register(FineModel x) => ApiClient.Save("fines", x.Id, 'D'); }
}
