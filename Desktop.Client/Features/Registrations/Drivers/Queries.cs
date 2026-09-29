using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement.Desktop.Client.Features.Registrations.Drivers
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiClient.List("drivers", ApiClient.Filters("search", ApiClient.NormalizeSearch(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiClient.ListAsync("drivers", ApiClient.Filters("search", ApiClient.NormalizeSearch(search)), cancellationToken);
        public static DataTable RegisterDriverActive(string search)
        {
            var table = search == "%" ? ApiClient.List("drivers")
                : ApiClient.List("drivers", ApiClient.Filters("active", search == "A"));
            return table;
        }
        public static DataTable IdAndNameActive() => ApiClient.List("drivers", ApiClient.Filters("active", true));
        public static Task<DataTable> IdAndNameActiveAsync(CancellationToken cancellationToken) =>
            ApiClient.ListAsync("drivers", ApiClient.Filters("active", true), cancellationToken);
    }
}
