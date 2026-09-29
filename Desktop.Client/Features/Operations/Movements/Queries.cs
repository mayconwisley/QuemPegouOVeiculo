using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement.Desktop.Client.Features.Operations.Movements
{
    public static class Query
    {
        private const string Resource = "movements";
        public static DataTable Register(string search) => ApiClient.List(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiClient.ListAsync(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)), cancellationToken);
        public static DataTable RegisterAll() => ApiClient.List(Resource);
        public static DataTable RegisterArrivalNull() => ApiClient.List(Resource, ApiClient.Filters("isOpen", true));
        public static Task<DataTable> RegisterArrivalNullAsync(CancellationToken cancellationToken) =>
            ApiClient.ListAsync(Resource, ApiClient.Filters("isOpen", true), cancellationToken);
        public static DataTable RegisterVehicle(int vehicleId) => ApiClient.List(Resource, ApiClient.Filters("vehicleId", vehicleId));
        public static DataTable RegisterDriver(int driverId) => ApiClient.List(Resource, ApiClient.Filters("driverId", driverId));
        public static DataTable RegisterVehicleDriver(int vehicleId, int driverId) =>
            ApiClient.List(Resource, ApiClient.Filters("vehicleId", vehicleId, "driverId", driverId));
        public static DataTable RegisterDateExit(DateTime start, DateTime end) => Period(start, end, "departure");
        public static DataTable RegisterDateArrival(DateTime start, DateTime end) => Period(start, end, "arrival");
        public static DataTable RegisterDateExitVehicle(DateTime start, DateTime end, int vehicleId) => Period(start, end, "departure", vehicleId);
        public static DataTable RegisterDateArrivalVehicle(DateTime start, DateTime end, int vehicleId) => Period(start, end, "arrival", vehicleId);
        public static DataTable RegisterDateExitDriver(DateTime start, DateTime end, int driverId) => Period(start, end, "departure", null, driverId);
        public static DataTable RegisterDateArrivalDriver(DateTime start, DateTime end, int driverId) => Period(start, end, "arrival", null, driverId);
        public static DataTable RegisterDateExitNull() => RegisterArrivalNull();
        private static DataTable Period(DateTime start, DateTime end, string fieldName, int? vehicle = null, int? driver = null) =>
            ApiClient.List(Resource, ApiClient.Filters("startUtc", ApiClient.StartUtc(start),
                "endUtc", ApiClient.EndUtc(end), "dateField", fieldName, "vehicleId", vehicle, "driverId", driver));
    }
}
