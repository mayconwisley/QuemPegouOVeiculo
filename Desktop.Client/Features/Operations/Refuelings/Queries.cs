using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement.Desktop.Client.Features.Operations.Refuelings
{
    public static class Query
    {
        private const string Resource = "refuelings";
        public static DataTable Register(string search) => ApiClient.List(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiClient.ListAsync(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)), cancellationToken);
        public static DataTable RegisterAll() => ApiClient.List(Resource);
        public static DataTable RegisterVehicle(int vehicleId) => ApiClient.List(Resource, ApiClient.Filters("vehicleId", vehicleId));
        public static DataTable RegisterDriver(int driverId) => ApiClient.List(Resource, ApiClient.Filters("driverId", driverId));
        public static DataTable RegisterVehicleDriver(int vehicleId, int driverId) =>
            ApiClient.List(Resource, ApiClient.Filters("vehicleId", vehicleId, "driverId", driverId));
        public static DataTable RegisterPeriod(DateTime start, DateTime end) => Period(start, end);
        public static DataTable RegisterPeriodVehicle(DateTime start, DateTime end, int vehicleId) => Period(start, end, vehicleId);
        public static DataTable RegisterPeriodDriver(DateTime start, DateTime end, int driverId) => Period(start, end, null, driverId);
        public static string LatestVehicleMileage(int vehicleId) => ApiClient.LatestMileage(vehicleId, "refueling");
        public static Task<string> LatestVehicleMileageAsync(int vehicleId, CancellationToken cancellationToken) =>
            ApiClient.LatestMileageAsync(vehicleId, "refueling", cancellationToken);
        private static DataTable Period(DateTime start, DateTime end, int? vehicle = null, int? driver = null) =>
            ApiClient.List(Resource, ApiClient.Filters("fromDate", ApiClient.Date(start), "toDate", ApiClient.Date(end),
                "vehicleId", vehicle, "driverId", driver));
    }
}
