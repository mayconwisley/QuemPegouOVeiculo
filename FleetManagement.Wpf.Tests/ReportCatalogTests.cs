using System.Text.Json.Nodes;
using FleetManagement.Wpf.Features.Reports;
using Xunit;

namespace FleetManagement.Wpf.Tests;

public sealed class ReportCatalogTests
{
    [Fact]
    public void LegacyReportModels_AreAvailableWithTheirOperationalColumns()
    {
        var expected = new Dictionary<string, string[]>
        {
            ["drivers"] = ["name", "cpf", "licenseNumber", "licenseCategory", "licenseExpiration", "rg", "active"],
            ["vehicles"] = ["plate", "model", "renavam", "chassis", "active"],
            ["vehicle-statuses"] = ["model", "startUtc", "endUtc", "description"],
            ["movements"] = ["departureUtc", "arrivalUtc", "durationDays", "durationHours", "name", "model", "initialMileage", "finalMileage", "mileageTotal", "description", "movementState"],
            ["maintenance"] = ["date", "model", "description", "amount"],
            ["refuelings"] = ["date", "model", "name", "mileage", "description", "amount", "liters"],
            ["fines"] = ["date", "name", "model", "description", "points", "amount"]
        };

        foreach (var (key, columns) in expected)
        {
            var report = Assert.Single(ReportCatalog.All, item => item.Key == key);
            Assert.Equal(columns, report.Columns.Select(column => column.Key));
            Assert.False(string.IsNullOrWhiteSpace(report.Title));
            Assert.False(string.IsNullOrWhiteSpace(report.Resource.ListPath));
        }
    }

    [Fact]
    public void MovementReport_CalculatesDurationMileageAndState()
    {
        var completed = new ReportRow(new JsonObject
        {
            ["departureUtc"] = "2026-10-01T10:00:00Z",
            ["arrivalUtc"] = "2026-10-03T15:00:00Z",
            ["initialMileage"] = 1000,
            ["finalMileage"] = 1125
        });

        Assert.Equal("2", completed["durationDays"]);
        Assert.Equal("5", completed["durationHours"]);
        Assert.Equal("125", completed["mileageTotal"]);
        Assert.Equal("Concluída", completed["movementState"]);

        var open = new ReportRow(new JsonObject { ["departureUtc"] = "2026-10-01T10:00:00Z" });
        Assert.Equal("", open["durationDays"]);
        Assert.Equal("Em aberto", open["movementState"]);
    }
}
