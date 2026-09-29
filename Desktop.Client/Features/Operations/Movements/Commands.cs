using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Features.Operations.Movements
{
    public static class Insert { public static bool Register(VehicleMovementModel x) => ApiClient.Save("movements", 0, 'I', RequestBodies.Movement(x)); }
    public static class Update
    {
        public static bool Register(VehicleMovementModel x) => ApiClient.Save("movements", x.Id, 'U', RequestBodies.Movement(x));
        public static bool CompleteMovement(VehicleMovementModel x) => ApiClient.CompleteMovement(x.Id, x.ArrivalAt, x.FinalMileage);
    }
    public static class Delete { public static bool Register(VehicleMovementModel x) => ApiClient.Save("movements", x.Id, 'D'); }
}
