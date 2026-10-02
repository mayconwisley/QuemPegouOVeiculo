using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.Desktop.Client.Infrastructure.Api
{
    public sealed class ReservationRecord
    {
        public int Id { get; internal set; }
        public int VehicleId { get; internal set; }
        public int DriverId { get; internal set; }
        public DateTime StartUtc { get; internal set; }
        public DateTime EndUtc { get; internal set; }
        public string Purpose { get; internal set; }
        public string Status { get; internal set; }
        public string Vehicle { get; internal set; }
        public string Driver { get; internal set; }
        public int? MovementId { get; internal set; }
        public string Start => StartUtc.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
        public string End => EndUtc.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
        public string StatusLabel => Status == "Confirmed" ? "Confirmada" :
            Status == "InUse" ? "Em uso" : Status == "Completed" ? "Concluída" : "Cancelada";
    }

    public sealed class PlanningDriverOption
    {
        public int Id { get; internal set; }
        public string Label { get; internal set; }
        public bool Active { get; internal set; }
    }

    public static class ReservationClient
    {
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        public static async Task<IList<PlanningDriverOption>> GetDriversAsync(
            CancellationToken ct = default(CancellationToken))
        {
            var drivers = new List<PlanningDriverOption>();
            for (var page = 1; ; page++)
            {
                var data = (Dictionary<string, object>)await ApiClient.GetAsync(
                    "queries/drivers?page=" + page + "&pageSize=100", ct)
                    .ConfigureAwait(false);
                foreach (Dictionary<string, object> value in (object[])data["items"])
                    drivers.Add(new PlanningDriverOption
                    {
                        Id = Number(value["id"]),
                        Label = Convert.ToString(value["name"], Invariant) +
                            (Convert.ToBoolean(value["active"], Invariant) ? "" : " (inativo)"),
                        Active = Convert.ToBoolean(value["active"], Invariant)
                    });
                if (drivers.Count >= Number(data["total"])) return drivers;
            }
        }

        public static async Task<PlanningPage<ReservationRecord>> GetAsync(int page,
            int? vehicleId = null, string status = null,
            CancellationToken ct = default(CancellationToken))
        {
            var path = "reservations?page=" + page + "&pageSize=50" +
                (vehicleId.HasValue ? "&vehicleId=" + vehicleId.Value : "") +
                (string.IsNullOrEmpty(status) ? "" : "&status=" + Uri.EscapeDataString(status));
            var data = (Dictionary<string, object>)await ApiClient.GetAsync(path, ct).ConfigureAwait(false);
            var items = new List<ReservationRecord>();
            foreach (Dictionary<string, object> value in (object[])data["items"])
                items.Add(new ReservationRecord
                {
                    Id = Number(value["id"]), VehicleId = Number(value["vehicleId"]),
                    DriverId = Number(value["driverId"]),
                    StartUtc = DateTime.Parse(Convert.ToString(value["startUtc"], Invariant),
                        Invariant, DateTimeStyles.RoundtripKind),
                    EndUtc = DateTime.Parse(Convert.ToString(value["endUtc"], Invariant),
                        Invariant, DateTimeStyles.RoundtripKind),
                    Purpose = Convert.ToString(value["purpose"], Invariant),
                    Status = Convert.ToString(value["status"], Invariant),
                    Vehicle = Convert.ToString(value["plate"], Invariant) + " - " +
                        Convert.ToString(value["vehicleModel"], Invariant),
                    Driver = Convert.ToString(value["driverName"], Invariant),
                    MovementId = value["movementId"] == null ? (int?)null : Number(value["movementId"])
                });
            return new PlanningPage<ReservationRecord> { Items = items, Total = Number(data["total"]) };
        }

        public static Task CreateAsync(int vehicleId, int driverId, DateTime start,
            DateTime end, string purpose, CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PostAsync("reservations", Body(vehicleId, driverId, start, end, purpose), ct);

        public static Task UpdateAsync(int id, int vehicleId, int driverId, DateTime start,
            DateTime end, string purpose, CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PutAsync("reservations/" + id,
                Body(vehicleId, driverId, start, end, purpose), ct);

        public static Task CancelAsync(int id, CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PostAsync("reservations/" + id + "/cancel", null, ct);

        public static Task StartAsync(int id, int initialMileage, string description,
            CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PostAsync("reservations/" + id + "/start", new Dictionary<string, object>
            {
                ["initialMileage"] = initialMileage, ["description"] = description
            }, ct);

        private static Dictionary<string, object> Body(int vehicleId, int driverId,
            DateTime start, DateTime end, string purpose) => new Dictionary<string, object>
            {
                ["vehicleId"] = vehicleId, ["driverId"] = driverId,
                ["startUtc"] = ApiClient.Utc(start), ["endUtc"] = ApiClient.Utc(end),
                ["purpose"] = purpose
            };

        private static int Number(object value) => Convert.ToInt32(value, Invariant);
    }
}
