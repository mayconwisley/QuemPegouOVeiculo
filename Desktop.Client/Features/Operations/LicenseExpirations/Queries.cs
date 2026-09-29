using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement.Desktop.Client.Features.Operations.LicenseExpirations
{
    public static class Query
    {
        public static DataTable Register(string search) => ApiClient.List("license-expirations",
            ApiClient.Filters("search", ApiClient.NormalizeSearch(search)));
        public static Task<DataTable> RegisterAsync(string search, CancellationToken cancellationToken) =>
            ApiClient.ListAsync("license-expirations", ApiClient.Filters("search", ApiClient.NormalizeSearch(search)),
                cancellationToken);
    }
}
