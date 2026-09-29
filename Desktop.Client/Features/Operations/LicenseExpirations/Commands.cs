using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Operations.LicenseExpirations
{
    public static class Insert { public static bool Register(LicenseExpirationModel x) => ApiClient.Save("license-expirations", 0, 'I', RequestBodies.LicenseExpiration(x)); }
    public static class Update { public static bool Register(LicenseExpirationModel x) => ApiClient.Save("license-expirations", x.Id, 'U', RequestBodies.LicenseExpiration(x)); }
    public static class Delete { public static bool Register(LicenseExpirationModel x) => ApiClient.Save("license-expirations", x.Id, 'D'); }
}
