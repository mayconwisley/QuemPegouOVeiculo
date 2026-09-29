using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement.Desktop.Client.Features.Operations.VehicleStatuses
{
    public static class Query
    {
        private const string Resource = "vehicle-statuses";
        public static DataTable Register(string search) => ApiClient.List(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiClient.ListAsync(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)), cancellationToken);
        public static DataTable RegisterAll() => ApiClient.List(Resource);
        public static DataTable RegisterVehicle(int vehicleId) => ApiClient.List(Resource, ApiClient.Filters("vehicleId", vehicleId));
        public static DataTable RegisterDateStart(DateTime start, DateTime end) => Period(start, end, "start");
        public static DataTable RegisterDateFinal(DateTime start, DateTime end) => Period(start, end, "end");
        public static DataTable RegisterDateFinalNull() => ApiClient.List(Resource, ApiClient.Filters("isOpen", true));
        private static DataTable Period(DateTime start, DateTime end, string fieldName) => ApiClient.List(Resource,
            ApiClient.Filters("startUtc", ApiClient.StartUtc(start), "endUtc", ApiClient.EndUtc(end), "dateField", fieldName));
    }
}
