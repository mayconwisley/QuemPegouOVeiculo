using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement.Desktop.Client.Features.Registrations.Vehicles
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiClient.List("vehicles", ApiClient.Filters("search", ApiClient.NormalizeSearch(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiClient.ListAsync("vehicles", ApiClient.Filters("search", ApiClient.NormalizeSearch(search)), cancellationToken);
        public static DataTable RegisterVehicleStaus(string search) => search == "%"
            ? ApiClient.List("vehicles") : ApiClient.List("vehicles", ApiClient.Filters("active", search == "A"));
        public static DataTable IdAndModelActive() => ApiClient.List("vehicles", ApiClient.Filters("active", true));
        public static Task<DataTable> IdAndModelActiveAsync(CancellationToken cancellationToken) =>
            ApiClient.ListAsync("vehicles", ApiClient.Filters("active", true), cancellationToken);
        public static string LatestVehicleMileage(int vehicleId) => ApiClient.LatestMileage(vehicleId, "movement");
        public static Task<string> LatestVehicleMileageAsync(int vehicleId, CancellationToken cancellationToken) =>
            ApiClient.LatestMileageAsync(vehicleId, "movement", cancellationToken);
    }
}
