using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace FleetManagement.Desktop.Client.Infrastructure.Api
{
    internal static class LegacyTableMapper
    {
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        internal static DataTable CreateTable(string resource)
        {
            var table = new DataTable(resource);
            switch (resource)
            {
                case "drivers":
                    Columns(table, "Id:int", "Name", "LicenseNumber", "LicenseExpiration:date", "LicenseCategory", "CPF", "RG", "Active"); break;
                case "vehicles":
                    Columns(table, "Id:int", "Plate", "Model", "Chassis", "Renavam", "Status"); break;
                case "movements":
                    Columns(table, "Id:int", "VehicleId:int", "Model", "DriverId:int", "Name",
                        "DepartureAt:date", "ArrivalAt:date", "Days:int", "Hours", "Description",
                        "InitialMileage", "FinalMileage", "TotalMileage", "Status"); break;
                case "refuelings":
                    Columns(table, "Id:int", "VehicleId:int", "Model", "DriverId:int", "Name", "InitialMileage",
                        "Date:date", "Amount:decimal", "Liters:decimal", "Description"); break;
                case "fines":
                    Columns(table, "Id:int", "VehicleId:int", "Name", "Model", "DriverId:int",
                        "Date:date", "Amount:decimal", "Points:int", "Description"); break;
                case "maintenance":
                    Columns(table, "Id:int", "VehicleId:int", "Model", "Date:date", "Amount:decimal", "Description"); break;
                case "vehicle-statuses":
                    Columns(table, "Id:int", "VehicleId:int", "Model", "StartAt:date", "EndAt:date", "Description"); break;
                case "license-expirations":
                    Columns(table, "Id:int", "Date:date", "DriverId:int", "Name", "Status"); break;
                default: throw new ArgumentException("Recurso desconhecido: " + resource);
            }
            return table;
        }

        private static void Columns(DataTable table, params string[] definitions)
        {
            foreach (var definition in definitions)
            {
                var parts = definition.Split(':');
                var type = parts.Length == 1 ? typeof(string)
                    : parts[1] == "int" ? typeof(int)
                    : parts[1] == "decimal" ? typeof(decimal) : typeof(DateTime);
                table.Columns.Add(parts[0], type);
            }
        }

        internal static void AddRow(DataTable table, string resource, Dictionary<string, object> item)
        {
            switch (resource)
            {
                case "drivers":
                    table.Rows.Add(Integer(item, "id"), Text(item, "name"), Text(item, "licenseNumber"), LocalDate(item, "licenseExpiration"),
                        Text(item, "licenseCategory"), Text(item, "cpf"), Text(item, "rg"), Boolean(item, "active") ? "Ativo" : "Desativado"); break;
                case "vehicles":
                    table.Rows.Add(Integer(item, "id"), Text(item, "plate"), Text(item, "model"), Text(item, "chassis"),
                        Text(item, "renavam"), Boolean(item, "active") ? "Ativo" : "Desativado"); break;
                case "movements":
                    var departure = (DateTime)LocalDate(item, "departureUtc");
                    var arrival = LocalDate(item, "arrivalUtc");
                    var duration = arrival == DBNull.Value ? TimeSpan.Zero : (DateTime)arrival - departure;
                    var initialMileage = Integer(item, "initialMileage");
                    var finalMileage = item["finalMileage"] == null ? (int?)null : Integer(item, "finalMileage");
                    table.Rows.Add(Integer(item, "id"), Integer(item, "vehicleId"), Text(item, "model"),
                        Integer(item, "driverId"), Text(item, "name"), departure, arrival, duration.Days,
                        arrival == DBNull.Value ? "" : duration.ToString(@"hh\:mm", Invariant), Text(item, "description"),
                        initialMileage.ToString(Invariant), finalMileage.HasValue ? finalMileage.Value.ToString(Invariant) : "",
                        finalMileage.HasValue ? (finalMileage.Value - initialMileage).ToString(Invariant) : "",
                        arrival == DBNull.Value ? "S" : "C"); break;
                case "refuelings":
                    table.Rows.Add(Integer(item, "id"), Integer(item, "vehicleId"), Text(item, "model"),
                        Integer(item, "driverId"), Text(item, "name"), Integer(item, "mileage").ToString(Invariant),
                        LocalDate(item, "date"), Decimal(item, "amount"), Decimal(item, "liters"), Text(item, "description")); break;
                case "fines":
                    table.Rows.Add(Integer(item, "id"), Integer(item, "vehicleId"), Text(item, "name"), Text(item, "model"),
                        Integer(item, "driverId"), LocalDate(item, "date"), Decimal(item, "amount"),
                        Integer(item, "points"), Text(item, "description")); break;
                case "maintenance":
                    table.Rows.Add(Integer(item, "id"), Integer(item, "vehicleId"), Text(item, "model"),
                        LocalDate(item, "date"), Decimal(item, "amount"), Text(item, "description")); break;
                case "vehicle-statuses":
                    table.Rows.Add(Integer(item, "id"), Integer(item, "vehicleId"), Text(item, "model"),
                        LocalDate(item, "startUtc"), LocalDate(item, "endUtc"), Text(item, "description")); break;
                case "license-expirations":
                    table.Rows.Add(Integer(item, "id"), LocalDate(item, "date"), Integer(item, "driverId"),
                        Text(item, "name"), Boolean(item, "expired") ? "Vencido" : "Não Vencido"); break;
            }
        }

        private static string Text(Dictionary<string, object> item, string name) =>
            item[name] == null ? "" : Convert.ToString(item[name], Invariant);
        private static int Integer(Dictionary<string, object> item, string name) => Convert.ToInt32(item[name], Invariant);
        private static decimal Decimal(Dictionary<string, object> item, string name) => Convert.ToDecimal(item[name], Invariant);
        private static bool Boolean(Dictionary<string, object> item, string name) => Convert.ToBoolean(item[name], Invariant);
        private static object LocalDate(Dictionary<string, object> item, string name)
        {
            if (item[name] == null)
                return DBNull.Value;
            var data = DateTime.Parse(Text(item, name), Invariant, DateTimeStyles.RoundtripKind);
            return data.Kind == DateTimeKind.Utc ? data.ToLocalTime() : data;
        }
    }
}
