using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FleetManagement.Desktop.Client.Infrastructure.Api
{
    public sealed class UserSession
    {
        public int Id { get; internal set; }
        public string Username { get; internal set; }
        public string Role { get; internal set; }
        public DateTime ExpiresAtUtc { get; internal set; }
        public bool IsAdministrator => Role == "Administrator";
        public bool CanWrite => Role == "Administrator" || Role == "Operator";
    }

    public sealed class UserRecord
    {
        public int Id { get; internal set; }
        public string Username { get; internal set; }
        public string Role { get; internal set; }
        public bool IsActive { get; internal set; }
    }

    public sealed class DashboardAttention
    {
        public string Kind { get; internal set; }
        public int RecordId { get; internal set; }
        public string Description { get; internal set; }
        public DateTime? OccurredAtUtc { get; internal set; }
        public DateTime? DueDate { get; internal set; }
    }

    public sealed class DashboardOverview
    {
        public int ActiveVehicles { get; internal set; }
        public int ActiveDrivers { get; internal set; }
        public int OpenMovements { get; internal set; }
        public int OpenVehicleStatuses { get; internal set; }
        public int ExpiringLicenses { get; internal set; }
        public int OverdueReturns { get; internal set; }
        public int PendingChecklists { get; internal set; }
        public int DueMaintenancePlans { get; internal set; }
        public IList<DashboardAttention> Attention { get; internal set; }
    }

    public sealed class AuditRecord
    {
        public DateTime OccurredAtUtc { get; internal set; }
        public string ActorUsername { get; internal set; }
        public string EntityName { get; internal set; }
        public int EntityId { get; internal set; }
        public string Action { get; internal set; }
        public string ChangesJson { get; internal set; }
        public string Summary
        {
            get
            {
                try
                {
                    var fields = (Dictionary<string, object>)new JavaScriptSerializer().DeserializeObject(ChangesJson);
                    return string.Join("; ", fields.Select(x => FieldLabel(x.Key) + ": " + DisplayValue(x.Value)));
                }
                catch (Exception) { return "Detalhes indisponíveis"; }
            }
        }
        public string ActionLabel => Action == "Added" ? "Inclusão" : Action == "Modified" ? "Alteração" : "Exclusão";
        public string EntityLabel
        {
            get
            {
                switch (EntityName)
                {
                    case "UserAccount": return "Usuário";
                    case "Driver": return "Motorista";
                    case "Vehicle": return "Veículo";
                    case "VehicleMovement": return "Movimentação";
                    case "MovementChecklist": return "Checklist";
                    case "MaintenancePlan": return "Plano preventivo";
                    case "Refueling": return "Abastecimento";
                    case "Fine": return "Multa";
                    case "Maintenance": return "Manutenção";
                    case "VehicleStatus": return "Status de veículo";
                    case "LicenseExpiration": return "Vencimento CNH";
                    default: return EntityName;
                }
            }
        }

        private static string FieldLabel(string field)
        {
            switch (field)
            {
                case "Name": return "Nome";
                case "Username": return "Usuário";
                case "Role": return "Perfil";
                case "IsActive": case "Active": return "Ativo";
                case "Plate": return "Placa";
                case "Model": return "Modelo";
                case "Description": return "Descrição";
                case "DepartureUtc": return "Saída";
                case "ArrivalUtc": return "Chegada";
                case "ExpectedReturnUtc": return "Previsão de retorno";
                case "Phase": return "Etapa";
                case "TiresOk": return "Pneus";
                case "LightsOk": return "Luzes";
                case "FluidsOk": return "Fluidos";
                case "BodyOk": return "Lataria";
                case "NextDueDate": return "Próxima data";
                case "NextDueMileage": return "Próxima quilometragem";
                case "IntervalDays": return "Intervalo em dias";
                case "IntervalMileage": return "Intervalo em km";
                case "InitialMileage": return "KM inicial";
                case "FinalMileage": return "KM final";
                case "LicenseExpiration": return "Vencimento da CNH";
                case "LicenseNumber": return "CNH";
                case "LicenseCategory": return "Categoria da CNH";
                case "Amount": return "Valor";
                case "Liters": return "Litros";
                case "Date": return "Data";
                case "Points": return "Pontos";
                case "Mileage": return "Quilometragem";
                case "StartUtc": return "Início";
                case "EndUtc": return "Fim";
                case "VehicleId": return "Veículo ID";
                case "DriverId": return "Motorista ID";
                case "Expired": return "Vencido";
                default: return field;
            }
        }

        private static string DisplayValue(object value)
        {
            if (value is Dictionary<string, object> change)
                return DisplayValue(change["Before"]) + " → " + DisplayValue(change["After"]);
            if (value == null) return "(vazio)";
            if (value is bool flag) return flag ? "Sim" : "Não";
            var text = Convert.ToString(value, CultureInfo.InvariantCulture);
            return text == "Administrator" ? "Administrador" : text == "Operator" ? "Operador"
                : text == "Viewer" ? "Consulta" : text == "departure" ? "Saída"
                : text == "arrival" ? "Chegada" : text;
        }
    }

    public sealed class AuditPage
    {
        public int Total { get; internal set; }
        public IList<AuditRecord> Items { get; internal set; }
    }

    public static class AccessClient
    {
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;
        public static event Action SessionExpired;
        internal static string Token { get; private set; }
        public static UserSession Current { get; private set; }

        public static async Task<UserSession> LoginAsync(string username, string password,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = (Dictionary<string, object>)await ApiClient.PostAnonymousAsync("auth/login",
                new Dictionary<string, object> { ["username"] = username, ["password"] = password },
                cancellationToken).ConfigureAwait(false);
            var user = (Dictionary<string, object>)response["user"];
            Token = Convert.ToString(response["token"], Invariant);
            Current = new UserSession
            {
                Id = Convert.ToInt32(user["id"], Invariant),
                Username = Convert.ToString(user["username"], Invariant),
                Role = Convert.ToString(user["role"], Invariant),
                ExpiresAtUtc = DateTime.Parse(Convert.ToString(response["expiresAtUtc"], Invariant),
                    Invariant, DateTimeStyles.RoundtripKind)
            };
            return Current;
        }

        public static void Logout()
        {
            Token = null;
            Current = null;
        }

        internal static void Expire()
        {
            if (Current == null)
                return;
            Logout();
            SessionExpired?.Invoke();
        }

        public static async Task<IList<UserRecord>> ListUsersAsync(CancellationToken ct = default(CancellationToken))
        {
            var data = (object[])await ApiClient.GetAsync("users", ct).ConfigureAwait(false);
            var users = new List<UserRecord>();
            foreach (Dictionary<string, object> item in data)
                users.Add(new UserRecord
                {
                    Id = Convert.ToInt32(item["id"], Invariant),
                    Username = Convert.ToString(item["username"], Invariant),
                    Role = Convert.ToString(item["role"], Invariant),
                    IsActive = Convert.ToBoolean(item["isActive"], Invariant)
                });
            return users;
        }

        public static async Task CreateUserAsync(string username, string password, string role,
            CancellationToken ct = default(CancellationToken)) =>
            await ApiClient.PostAsync("users", new Dictionary<string, object>
            { ["username"] = username, ["password"] = password, ["role"] = role }, ct).ConfigureAwait(false);

        public static Task UpdateUserAsync(int id, string role, bool active,
            CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PutAsync("users/" + id, new Dictionary<string, object>
            { ["role"] = role, ["isActive"] = active }, ct);

        public static Task ResetPasswordAsync(int id, string password,
            CancellationToken ct = default(CancellationToken)) =>
            ApiClient.PutAsync("users/" + id + "/password",
                new Dictionary<string, object> { ["password"] = password }, ct);

        public static async Task<DashboardOverview> GetDashboardAsync(CancellationToken ct = default(CancellationToken))
        {
            var data = (Dictionary<string, object>)await ApiClient.GetAsync("dashboard", ct).ConfigureAwait(false);
            var attention = new List<DashboardAttention>();
            foreach (Dictionary<string, object> item in (object[])data["attention"])
                attention.Add(new DashboardAttention
                {
                    Kind = Convert.ToString(item["kind"], Invariant),
                    RecordId = Convert.ToInt32(item["recordId"], Invariant),
                    Description = Convert.ToString(item["description"], Invariant),
                    OccurredAtUtc = ParseOptionalDate(item["occurredAtUtc"]),
                    DueDate = ParseOptionalDate(item["dueDate"])
                });
            return new DashboardOverview
            {
                ActiveVehicles = Convert.ToInt32(data["activeVehicles"], Invariant),
                ActiveDrivers = Convert.ToInt32(data["activeDrivers"], Invariant),
                OpenMovements = Convert.ToInt32(data["openMovements"], Invariant),
                OpenVehicleStatuses = Convert.ToInt32(data["openVehicleStatuses"], Invariant),
                ExpiringLicenses = Convert.ToInt32(data["expiringLicenses"], Invariant),
                OverdueReturns = Convert.ToInt32(data["overdueReturns"], Invariant),
                PendingChecklists = Convert.ToInt32(data["pendingChecklists"], Invariant),
                DueMaintenancePlans = Convert.ToInt32(data["dueMaintenancePlans"], Invariant),
                Attention = attention
            };
        }

        public static async Task<AuditPage> GetAuditAsync(int page, CancellationToken ct = default(CancellationToken))
        {
            var response = (Dictionary<string, object>)await ApiClient.GetAsync("audit?page=" + page + "&pageSize=100", ct)
                .ConfigureAwait(false);
            var result = new List<AuditRecord>();
            foreach (Dictionary<string, object> item in (object[])response["items"])
                result.Add(new AuditRecord
                {
                    OccurredAtUtc = DateTime.Parse(Convert.ToString(item["occurredAtUtc"], Invariant),
                        Invariant, DateTimeStyles.RoundtripKind),
                    ActorUsername = Convert.ToString(item["actorUsername"], Invariant),
                    EntityName = Convert.ToString(item["entityName"], Invariant),
                    EntityId = Convert.ToInt32(item["entityId"], Invariant),
                    Action = Convert.ToString(item["action"], Invariant),
                    ChangesJson = Convert.ToString(item["changesJson"], Invariant)
                });
            return new AuditPage { Total = Convert.ToInt32(response["total"], Invariant), Items = result };
        }

        private static DateTime? ParseOptionalDate(object value) => value == null ? (DateTime?)null
            : DateTime.Parse(Convert.ToString(value, Invariant), Invariant, DateTimeStyles.RoundtripKind);
    }
}
