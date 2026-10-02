using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class ExportForm : Form
    {
        private sealed class VehicleOption
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public override string ToString() => Label;
        }

        private sealed class ExportOption
        {
            public string Label { get; set; }
            public string Resource { get; set; }
            public override string ToString() => Label;
        }

        private readonly ComboBox _resource = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 290 };
        private readonly TextBox _search = new TextBox { Width = 290 };
        private readonly ComboBox _vehicle = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 190 };
        private readonly DateTimePicker _from = new DateTimePicker { Format = DateTimePickerFormat.Short,
            ShowCheckBox = true, Checked = false, Width = 140 };
        private readonly DateTimePicker _to = new DateTimePicker { Format = DateTimePickerFormat.Short,
            ShowCheckBox = true, Checked = false, Width = 140 };
        private readonly ComboBox _status = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 155 };
        private readonly Button _export = new Button { Text = "Exportar CSV", Width = 120 };
        private readonly Label _message = new Label { AutoSize = true, MaximumSize = new Size(610, 0) };
        private CancellationTokenSource _downloadCancellation;

        public ExportForm()
        {
            Text = "Exportar dados em CSV";
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.White;
            MinimumSize = new Size(660, 370);
            _resource.Items.AddRange(new object[]
            {
                Option("Motoristas", "drivers"), Option("Veículos", "vehicles"),
                Option("Movimentações", "movements"), Option("Abastecimentos", "refuelings"),
                Option("Multas", "fines"), Option("Manutenções", "maintenance"),
                Option("Status dos veículos", "vehicle-statuses"),
                Option("Vencimentos de CNH", "license-expirations"),
                Option("Reservas", "reservations"),
                Option("Planos de manutenção", "maintenance-plans")
            });
            _resource.SelectedIndex = 0;
            _status.Items.AddRange(new object[] { "Todos", "Confirmada", "Em uso",
                "Concluída", "Cancelada" });
            _status.SelectedIndex = 0;
            AddField("Dados", _resource, 20, 18);
            AddField("Busca", _search, 335, 18);
            _vehicle.Items.Add(new VehicleOption { Id = 0, Label = "Todos os veículos" });
            _vehicle.SelectedIndex = 0;
            AddField("Veículo", _vehicle, 20, 85);
            AddField("De", _from, 230, 85);
            AddField("Até", _to, 390, 85);
            AddField("Status da reserva", _status, 20, 153);
            _export.Location = new Point(20, 235);
            _message.Location = new Point(160, 240);
            Controls.AddRange(new Control[] { _export, _message });
            _resource.SelectedIndexChanged += (sender, args) => ApplyFilterAvailability();
            _export.Click += async (sender, args) => await ExportAsync();
            FormClosing += (sender, args) => _downloadCancellation?.Cancel();
            Shown += async (sender, args) =>
            {
                try
                {
                    foreach (var vehicle in await OperationsPlanningClient.GetVehiclesAsync())
                        _vehicle.Items.Add(new VehicleOption { Id = vehicle.Id, Label = vehicle.Label });
                }
                catch (Exception ex) { _message.Text = ex.Message; }
            };
            ApplyFilterAvailability();
        }

        private static ExportOption Option(string label, string resource) =>
            new ExportOption { Label = label, Resource = resource };

        private void AddField(string label, Control field, int left, int top)
        {
            Controls.Add(new Label { Text = label, Location = new Point(left, top), AutoSize = true });
            field.Location = new Point(left, top + 24);
            Controls.Add(field);
        }

        private void ApplyFilterAvailability()
        {
            var resource = ((ExportOption)_resource.SelectedItem).Resource;
            var registration = resource == "drivers" || resource == "vehicles";
            var plans = resource == "maintenance-plans";
            var reservations = resource == "reservations";
            var dated = resource == "movements" || resource == "refuelings" ||
                resource == "fines" || resource == "maintenance" ||
                resource == "vehicle-statuses" || reservations;
            _search.Enabled = !plans && !reservations;
            _vehicle.Enabled = resource != "drivers" && resource != "vehicles" &&
                resource != "license-expirations" && !plans;
            _from.Enabled = _to.Enabled = dated;
            _status.Enabled = reservations;
            if (!dated) { _from.Checked = false; _to.Checked = false; }
            if (!reservations) _status.SelectedIndex = 0;
            if (registration || plans) _vehicle.SelectedIndex = 0;
        }

        private async Task ExportAsync()
        {
            if (_from.Enabled && _from.Checked && _to.Checked && _from.Value.Date > _to.Value.Date)
            {
                _message.Text = "O início do período deve ser anterior ao fim.";
                return;
            }
            var option = (ExportOption)_resource.SelectedItem;
            using (var dialog = new SaveFileDialog { Filter = "Arquivo CSV (*.csv)|*.csv",
                FileName = option.Resource + "-" + DateTime.Today.ToString("yyyyMMdd") + ".csv",
                OverwritePrompt = true })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                _export.Enabled = false;
                _message.Text = "Exportando...";
                _downloadCancellation = new CancellationTokenSource();
                try
                {
                    var filters = new Dictionary<string, string>();
                    if (_search.Enabled && !string.IsNullOrWhiteSpace(_search.Text))
                        filters["search"] = _search.Text.Trim();
                    var vehicle = _vehicle.SelectedItem as VehicleOption;
                    if (_vehicle.Enabled && vehicle != null && vehicle.Id > 0)
                        filters["vehicleId"] = vehicle.Id.ToString(CultureInfo.InvariantCulture);
                    if (_status.Enabled && _status.SelectedIndex > 0)
                        filters["status"] = new[] { "", "Confirmed", "InUse", "Completed",
                            "Cancelled" }[_status.SelectedIndex];
                    if (_from.Enabled && _from.Checked)
                        filters[option.Resource == "reservations" || option.Resource == "movements" ||
                            option.Resource == "vehicle-statuses" ? "startUtc" : "fromDate"] =
                            option.Resource == "reservations" || option.Resource == "movements" ||
                            option.Resource == "vehicle-statuses"
                                ? ApiDate(_from.Value.Date) : _from.Value.ToString("yyyy-MM-dd");
                    if (_to.Enabled && _to.Checked)
                        filters[option.Resource == "reservations" || option.Resource == "movements" ||
                            option.Resource == "vehicle-statuses" ? "endUtc" : "toDate"] =
                            option.Resource == "reservations" || option.Resource == "movements" ||
                            option.Resource == "vehicle-statuses"
                                ? ApiDate(_to.Value.Date.AddDays(1)) : _to.Value.ToString("yyyy-MM-dd");
                    await ExportClient.DownloadAsync(option.Resource, dialog.FileName, filters,
                        _downloadCancellation.Token);
                    if (!IsDisposed) _message.Text = "Arquivo salvo: " + dialog.FileName;
                }
                catch (OperationCanceledException) { }
                catch (Exception ex) { if (!IsDisposed) _message.Text = ex.Message; }
                finally
                {
                    _downloadCancellation.Dispose();
                    _downloadCancellation = null;
                    if (!IsDisposed) _export.Enabled = true;
                }
            }
        }

        private static string ApiDate(DateTime local) =>
            DateTime.SpecifyKind(local, DateTimeKind.Local).ToUniversalTime()
                .ToString("O", CultureInfo.InvariantCulture);
    }
}
