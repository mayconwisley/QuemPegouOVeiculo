using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.Desktop.Client.Infrastructure.Api
{
    public static class ExportClient
    {
        private static readonly HashSet<string> Resources = new HashSet<string>(StringComparer.Ordinal)
        {
            "drivers", "vehicles", "movements", "refuelings", "fines", "maintenance",
            "vehicle-statuses", "license-expirations", "reservations", "maintenance-plans"
        };

        public static Task DownloadAsync(string resource, string destination,
            IDictionary<string, string> filters = null,
            CancellationToken ct = default(CancellationToken))
        {
            if (!Resources.Contains(resource))
                throw new ArgumentException("Tipo de exportação inválido.", nameof(resource));
            var query = filters == null ? "" : string.Join("&", filters
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .Select(x => Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value)));
            return ApiClient.DownloadAsync("exports/" + resource + ".csv" +
                (query.Length == 0 ? "" : "?" + query), destination, ct);
        }
    }
}
