using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement.Desktop.Client.Features.Operations.Maintenance
{
    public static class Query
    {
        private const string Resource = "maintenance";
        public static DataTable Register(string search) => ApiClient.List(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiClient.ListAsync(Resource, ApiClient.Filters("search", ApiClient.NormalizeSearch(search)), cancellationToken);
        public static DataTable RegisterAll() => ApiClient.List(Resource);
        public static DataTable RegisterVehicle(int vehicleId) => ApiClient.List(Resource, ApiClient.Filters("vehicleId", vehicleId));
        public static DataTable RegisterPeriod(DateTime start, DateTime end) => ApiClient.List(Resource,
            ApiClient.Filters("fromDate", ApiClient.Date(start), "toDate", ApiClient.Date(end)));
    }
}
