using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.Desktop.Client.Infrastructure.Api
{
    public sealed class PlanningPage<T>
    {
        public IList<T> Items { get; internal set; }
        public int Total { get; internal set; }
    }

    public sealed class MovementPlanningRecord
    {
        public int Id { get; internal set; }
        public string Vehicle { get; internal set; }
        public string Driver { get; internal set; }
        public DateTime DepartureUtc { get; internal set; }
        public DateTime? ArrivalUtc { get; internal set; }
        public DateTime? ExpectedReturnUtc { get; internal set; }
        public string Departure => DepartureUtc.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
        public string ExpectedReturn => ExpectedReturnUtc?.ToLocalTime().ToString("dd/MM/yyyy HH:mm") ?? "";
    }

    public sealed class ChecklistRecord
    {
        public string Phase { get; internal set; }
        public bool TiresOk { get; internal set; }
        public bool LightsOk { get; internal set; }
        public bool FluidsOk { get; internal set; }
        public bool BodyOk { get; internal set; }
        public string Notes { get; internal set; }
    }

    public sealed class MaintenancePlanRecord
    {
        public int Id { get; internal set; }
        public int VehicleId { get; internal set; }
        public string Name { get; internal set; }
        public int? IntervalDays { get; internal set; }
        public int? IntervalMileage { get; internal set; }
        public DateTime? NextDueDate { get; internal set; }
        public int? NextDueMileage { get; internal set; }
        public bool IsActive { get; internal set; }
        public string NextDue => (NextDueDate?.ToString("dd/MM/yyyy") ?? "") +
            (NextDueMileage.HasValue ? " / " + NextDueMileage.Value.ToString("N0") + " km" : "");
    }

    public sealed class PlanningVehicleOption
    {
        public int Id { get; internal set; }
        public string Label { get; internal set; }
    }

    public static class OperationsPlanningClient
    {
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        public static async Task<IList<PlanningVehicleOption>> GetVehiclesAsync(
            CancellationToken ct = default(CancellationToken))
        {
            var vehicles = new List<PlanningVehicleOption>();
            for (var page = 1; ; page++)
            {
                var data = (Dictionary<string, object>)await ApiClient.GetAsync(
                    "queries/vehicles?page=" + page + "&pageSize=100", ct).ConfigureAwait(false);
                foreach (Dictionary<string, object> value in (object[])data["items"])
                    vehicles.Add(new PlanningVehicleOption
                    {
                        Id = Number(value["id"]),
                        Label = Convert.ToString(value["plate"], Invariant) + " - " +
                            Convert.ToString(value["model"], Invariant)
                    });
                if (vehicles.Count >= Number(data["total"]))
                    return vehicles;
            }
        }

        public static async Task<PlanningPage<MovementPlanningRecord>> GetMovementsAsync(bool open,
            int page, CancellationToken ct = default(CancellationToken))
        {
            var path = "queries/movements?isOpen=" + (open ? "true" : "false") +
                "&page=" + page + "&pageSize=100";
            var data = (Dictionary<string, object>)await ApiClient.GetAsync(path, ct).ConfigureAwait(false);
            var items = new List<MovementPlanningRecord>();
            foreach (Dictionary<string, object> value in (object[])data["items"])
                items.Add(new MovementPlanningRecord
                {
                    Id = Number(value["id"]),
                    Vehicle = Convert.ToString(value["plate"], Invariant) + " - " +
                        Convert.ToString(value["model"], Invariant),
                    Driver = Convert.ToString(value["name"], Invariant),
                    DepartureUtc = ParseDate(value["departureUtc"]).Value,
                    ArrivalUtc = ParseDate(value["arrivalUtc"]),
                    ExpectedReturnUtc = ParseDate(value["expectedReturnUtc"])
                });
            return new PlanningPage<MovementPlanningRecord> { Items = items, Total = Number(data["total"]) };
        }

        public static Task ScheduleReturnAsync(int movementId, DateTime? due,
            CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PutAsync("movements/" + movementId + "/expected-return",
                new Dictionary<string, object>
                { ["expectedReturnUtc"] = due.HasValue ? (object)ApiClient.Utc(due.Value) : null }, ct);

        public static async Task<IList<ChecklistRecord>> GetChecklistsAsync(int movementId,
            CancellationToken ct = default(CancellationToken))
        {
            var data = (object[])await ApiClient.GetAsync("movements/" + movementId + "/checklists", ct)
                .ConfigureAwait(false);
            var items = new List<ChecklistRecord>();
            foreach (Dictionary<string, object> value in data)
                items.Add(new ChecklistRecord
                {
                    Phase = Convert.ToString(value["phase"], Invariant),
                    TiresOk = Convert.ToBoolean(value["tiresOk"], Invariant),
                    LightsOk = Convert.ToBoolean(value["lightsOk"], Invariant),
                    FluidsOk = Convert.ToBoolean(value["fluidsOk"], Invariant),
                    BodyOk = Convert.ToBoolean(value["bodyOk"], Invariant),
                    Notes = Convert.ToString(value["notes"], Invariant)
                });
            return items;
        }

        public static Task SaveChecklistAsync(int movementId, string phase, bool tiresOk,
            bool lightsOk, bool fluidsOk, bool bodyOk, string notes,
            CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PutAsync("movements/" + movementId + "/checklists/" + phase,
                new Dictionary<string, object>
                {
                    ["tiresOk"] = tiresOk, ["lightsOk"] = lightsOk,
                    ["fluidsOk"] = fluidsOk, ["bodyOk"] = bodyOk,
                    ["notes"] = notes, ["checkedAtUtc"] = DateTime.UtcNow.ToString("O", Invariant)
                }, ct);

        public static async Task<PlanningPage<MaintenancePlanRecord>> GetPlansAsync(int page,
            CancellationToken ct = default(CancellationToken))
        {
            var data = (Dictionary<string, object>)await ApiClient.GetAsync(
                "maintenance-plans?page=" + page + "&pageSize=100", ct).ConfigureAwait(false);
            var items = new List<MaintenancePlanRecord>();
            foreach (Dictionary<string, object> value in (object[])data["items"])
                items.Add(new MaintenancePlanRecord
                {
                    Id = Number(value["id"]), VehicleId = Number(value["vehicleId"]),
                    Name = Convert.ToString(value["name"], Invariant),
                    IntervalDays = OptionalNumber(value["intervalDays"]),
                    IntervalMileage = OptionalNumber(value["intervalMileage"]),
                    NextDueDate = ParseDate(value["nextDueDate"]),
                    NextDueMileage = OptionalNumber(value["nextDueMileage"]),
                    IsActive = Convert.ToBoolean(value["isActive"], Invariant)
                });
            return new PlanningPage<MaintenancePlanRecord> { Items = items, Total = Number(data["total"]) };
        }

        public static Task CreatePlanAsync(int vehicleId, string name, int? intervalDays,
            int? intervalMileage, DateTime? nextDueDate, int? nextDueMileage, bool active,
            CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PostAsync("maintenance-plans", PlanBody(vehicleId, name, intervalDays,
                intervalMileage, nextDueDate, nextDueMileage, active), ct);

        public static Task UpdatePlanAsync(int id, int vehicleId, string name, int? intervalDays,
            int? intervalMileage, DateTime? nextDueDate, int? nextDueMileage, bool active,
            CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PutAsync("maintenance-plans/" + id, PlanBody(vehicleId, name, intervalDays,
                intervalMileage, nextDueDate, nextDueMileage, active), ct);

        public static Task CompletePlanAsync(int id, DateTime date, int mileage, decimal amount,
            string notes, CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PostAsync("maintenance-plans/" + id + "/complete", new Dictionary<string, object>
            {
                ["date"] = ApiClient.Date(date), ["mileage"] = mileage,
                ["amount"] = amount, ["notes"] = notes
            }, ct);

        private static Dictionary<string, object> PlanBody(int vehicleId, string name, int? intervalDays,
            int? intervalMileage, DateTime? nextDueDate, int? nextDueMileage, bool active) =>
            new Dictionary<string, object>
            {
                ["vehicleId"] = vehicleId, ["name"] = name,
                ["intervalDays"] = intervalDays, ["intervalMileage"] = intervalMileage,
                ["nextDueDate"] = nextDueDate.HasValue ? (object)ApiClient.Date(nextDueDate.Value) : null,
                ["nextDueMileage"] = nextDueMileage, ["isActive"] = active
            };

        private static int Number(object value) => Convert.ToInt32(value, Invariant);
        private static int? OptionalNumber(object value) => value == null ? (int?)null : Number(value);
        private static DateTime? ParseDate(object value) => value == null ? (DateTime?)null
            : DateTime.Parse(Convert.ToString(value, Invariant), Invariant, DateTimeStyles.RoundtripKind);
    }
}
