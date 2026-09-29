using System;
using System.Collections.Generic;
using FleetManagement.Desktop.Client.Infrastructure.Api;
using FleetManagement.Desktop.Models;

namespace FleetManagement.Desktop.Client.Infrastructure.Api
{
    internal static class RequestBodies
    {
        internal static Dictionary<string, object> Driver(DriverModel x) => new Dictionary<string, object>
        {
            ["name"] = x.Name, ["licenseNumber"] = x.LicenseNumber, ["licenseExpiration"] = ApiClient.Date(x.LicenseExpiration),
            ["licenseCategory"] = x.LicenseCategory, ["cpf"] = x.CPF, ["rg"] = x.RG, ["active"] = x.Active == 'A'
        };
        internal static Dictionary<string, object> Vehicle(VehicleModel x) => new Dictionary<string, object>
        {
            ["plate"] = x.Plate, ["model"] = x.Model, ["chassis"] = x.Chassis,
            ["renavam"] = x.Renavam, ["active"] = x.Status == 'A'
        };
        internal static Dictionary<string, object> Movement(VehicleMovementModel x)
        {
            if (!x.DepartureAt.HasValue)
                throw new ArgumentException("Informe a data e hora de saída.");
            return new Dictionary<string, object>
            {
                ["vehicleId"] = x.Vehicle.Id, ["driverId"] = x.Driver.Id,
                ["departureUtc"] = ApiClient.Utc(x.DepartureAt.Value),
                ["arrivalUtc"] = x.ArrivalAt.HasValue ? (object)ApiClient.Utc(x.ArrivalAt.Value) : null,
                ["initialMileage"] = ApiClient.Mileage(x.InitialMileage, "KM inicial"),
                ["finalMileage"] = ApiClient.OptionalMileage(x.FinalMileage, "KM final"),
                ["description"] = x.Description
            };
        }
        internal static Dictionary<string, object> Refueling(RefuelingModel x) => new Dictionary<string, object>
        {
            ["vehicleId"] = x.Vehicle.Id, ["driverId"] = x.Driver.Id,
            ["mileage"] = ApiClient.Mileage(x.InitialMileage, "KM inicial"),
            ["date"] = ApiClient.Date(x.Date), ["amount"] = x.Amount, ["liters"] = x.Liters,
            ["description"] = x.Description
        };
        internal static Dictionary<string, object> Fine(FineModel x) => new Dictionary<string, object>
        {
            ["vehicleId"] = x.Vehicle.Id, ["driverId"] = x.Driver.Id,
            ["date"] = ApiClient.Date(x.Date), ["amount"] = x.Amount, ["points"] = x.Points,
            ["description"] = x.Description
        };
        internal static Dictionary<string, object> Maintenance(MaintenanceModel x) => new Dictionary<string, object>
        {
            ["vehicleId"] = x.Vehicle.Id, ["date"] = ApiClient.Date(x.Date),
            ["amount"] = x.Amount, ["description"] = x.Description
        };
        internal static Dictionary<string, object> VehicleStatus(VehicleStatusModel x)
        {
            if (!x.StartAt.HasValue)
                throw new ArgumentException("Informe a data e hora inicial do status.");
            return new Dictionary<string, object>
            {
                ["vehicleId"] = x.Vehicle.Id,
                ["startUtc"] = ApiClient.Utc(x.StartAt.Value),
                ["endUtc"] = x.EndAt.HasValue ? (object)ApiClient.Utc(x.EndAt.Value) : null,
                ["description"] = x.Description
            };
        }
        internal static Dictionary<string, object> LicenseExpiration(LicenseExpirationModel x) => new Dictionary<string, object>
        {
            ["driverId"] = x.Driver.Id, ["date"] = ApiClient.Date(x.Date),
            ["expired"] = x.Status == 'V'
        };
    }
}
