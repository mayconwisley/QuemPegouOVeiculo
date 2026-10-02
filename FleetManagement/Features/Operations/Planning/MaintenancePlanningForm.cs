using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class MaintenancePlanningForm : Form
    {
        private readonly DataGridView _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true,
            AutoGenerateColumns = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false, AllowUserToAddRows = false, RowHeadersVisible = false };
        private readonly ComboBox _vehicle = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
        private readonly TextBox _name = new TextBox { Width = 280 };
        private readonly CheckBox _active = new CheckBox { Text = "Ativo", Checked = true, AutoSize = true };
        private readonly CheckBox _byDate = new CheckBox { Text = "Por data", AutoSize = true };
        private readonly CheckBox _byMileage = new CheckBox { Text = "Por km", AutoSize = true };
        private readonly NumericUpDown _days = new NumericUpDown { Minimum = 1, Maximum = int.MaxValue, Value = 180, Width = 80 };
        private readonly NumericUpDown _kilometers = new NumericUpDown { Minimum = 1, Maximum = int.MaxValue, Value = 10000, Width = 100 };
        private readonly DateTimePicker _nextDate = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 110 };
        private readonly NumericUpDown _nextMileage = new NumericUpDown { Minimum = 0,
            Maximum = int.MaxValue, Increment = 100, ThousandsSeparator = true, Width = 120 };
        private readonly DateTimePicker _completedOn = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 110 };
        private readonly NumericUpDown _completedMileage = new NumericUpDown { Minimum = 0,
            Maximum = int.MaxValue, Increment = 100, ThousandsSeparator = true, Width = 120 };
        private readonly NumericUpDown _amount = new NumericUpDown { Minimum = 0, Maximum = 1000000000,
            DecimalPlaces = 2, ThousandsSeparator = true, Width = 120 };
        private readonly TextBox _notes = new TextBox { Width = 270 };
        private readonly Label _message = new Label { AutoSize = true };
        private readonly Button _previous = new Button { Text = "Anterior", Width = 85 };
        private readonly Button _next = new Button { Text = "Próxima", Width = 85 };
        private readonly Button _new = new Button { Text = "Novo", Width = 85 };
        private readonly Button _save = new Button { Text = "Salvar plano", Width = 115 };
        private readonly Button _complete = new Button { Text = "Concluir manutenção", Width = 165 };
        private int _page = 1;
        private int _editingId;
        private PlanningPage<MaintenancePlanRecord> _result;
        private readonly Dictionary<int, string> _vehicleLabels = new Dictionary<int, string>();

        public MaintenancePlanningForm()
        {
            Text = "Manutenção preventiva";
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(244, 247, 251);
            MinimumSize = new Size(920, 690);

            var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 47, Padding = new Padding(15, 9, 0, 0) };
            toolbar.Controls.AddRange(new Control[] { _previous, _next, _new, _message });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "VehicleColumn", HeaderText = "Veículo",
                DataPropertyName = "VehicleId", Width = 175 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Serviço", DataPropertyName = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Próximo vencimento", DataPropertyName = "NextDue", Width = 235 });
            _grid.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Ativo", DataPropertyName = "IsActive", Width = 65 });

            var editor = new Panel { Dock = DockStyle.Bottom, Height = 355, BackColor = Color.White };
            editor.Controls.Add(new Label { Text = "Veículo", Location = new Point(20, 15), AutoSize = true });
            _vehicle.Location = new Point(20, 38);
            editor.Controls.Add(_vehicle);
            editor.Controls.Add(new Label { Text = "Serviço preventivo", Location = new Point(270, 15), AutoSize = true });
            _name.Location = new Point(270, 38);
            editor.Controls.Add(_name);
            _active.Location = new Point(575, 41);
            editor.Controls.Add(_active);

            _byDate.Location = new Point(20, 87);
            editor.Controls.Add(_byDate);
            editor.Controls.Add(new Label { Text = "Intervalo (dias)", Location = new Point(125, 87), AutoSize = true });
            _days.Location = new Point(125, 110);
            editor.Controls.Add(_days);
            editor.Controls.Add(new Label { Text = "Próxima data", Location = new Point(235, 87), AutoSize = true });
            _nextDate.Location = new Point(235, 110);
            editor.Controls.Add(_nextDate);
            _byMileage.Location = new Point(390, 87);
            editor.Controls.Add(_byMileage);
            editor.Controls.Add(new Label { Text = "Intervalo (km)", Location = new Point(485, 87), AutoSize = true });
            _kilometers.Location = new Point(485, 110);
            editor.Controls.Add(_kilometers);
            editor.Controls.Add(new Label { Text = "Próximo km", Location = new Point(620, 87), AutoSize = true });
            _nextMileage.Location = new Point(620, 110);
            editor.Controls.Add(_nextMileage);
            _save.Location = new Point(20, 153);
            editor.Controls.Add(_save);

            editor.Controls.Add(new Label { Text = "Registrar conclusão e avançar o plano",
                Location = new Point(20, 204), AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold) });
            editor.Controls.Add(new Label { Text = "Data", Location = new Point(20, 237), AutoSize = true });
            _completedOn.Location = new Point(20, 260);
            editor.Controls.Add(_completedOn);
            editor.Controls.Add(new Label { Text = "KM atual", Location = new Point(155, 237), AutoSize = true });
            _completedMileage.Location = new Point(155, 260);
            editor.Controls.Add(_completedMileage);
            editor.Controls.Add(new Label { Text = "Valor (R$)", Location = new Point(300, 237), AutoSize = true });
            _amount.Location = new Point(300, 260);
            editor.Controls.Add(_amount);
            editor.Controls.Add(new Label { Text = "Observações", Location = new Point(440, 237), AutoSize = true });
            _notes.Location = new Point(440, 260);
            editor.Controls.Add(_notes);
            _complete.Location = new Point(20, 307);
            editor.Controls.Add(_complete);

            Controls.Add(_grid);
            Controls.Add(editor);
            Controls.Add(toolbar);

            _byDate.CheckedChanged += (sender, args) => ApplyModes();
            _byMileage.CheckedChanged += (sender, args) => ApplyModes();
            _grid.SelectionChanged += (sender, args) => ShowSelected();
            _grid.CellFormatting += (sender, args) =>
            {
                if (_grid.Columns[args.ColumnIndex].Name == "VehicleColumn" && args.Value is int id &&
                    _vehicleLabels.TryGetValue(id, out var label))
                {
                    args.Value = label;
                    args.FormattingApplied = true;
                }
            };
            _new.Click += (sender, args) => ClearEditor();
            _save.Click += async (sender, args) => await SaveAsync();
            _complete.Click += async (sender, args) => await CompleteAsync();
            _previous.Click += async (sender, args) => { if (_page > 1) { _page--; await LoadPageAsync(); } };
            _next.Click += async (sender, args) => { _page++; await LoadPageAsync(); };
            Shown += async (sender, args) =>
            {
                try
                {
                    _new.Enabled = _save.Enabled = AccessClient.Current?.CanWrite == true;
                    var vehicles = (await OperationsPlanningClient.GetVehiclesAsync()).ToList();
                    foreach (var vehicle in vehicles) _vehicleLabels[vehicle.Id] = vehicle.Label;
                    _vehicle.DataSource = vehicles;
                    _vehicle.DisplayMember = "Label";
                    _vehicle.ValueMember = "Id";
                    await LoadPageAsync();
                }
                catch (Exception ex) { _message.Text = ex.Message; }
            };
            ApplyModes();
        }

        private void ApplyModes()
        {
            _days.Enabled = _nextDate.Enabled = _byDate.Checked;
            _kilometers.Enabled = _nextMileage.Enabled = _byMileage.Checked;
        }

        private async Task LoadPageAsync()
        {
            try
            {
                _result = await OperationsPlanningClient.GetPlansAsync(_page);
                _grid.DataSource = _result.Items.ToList();
                _previous.Enabled = _page > 1;
                _next.Enabled = _page * 100 < _result.Total;
                _message.Text = $"Página {_page} · {_result.Total} planos";
                if (_result.Items.Count == 0) ClearEditor();
            }
            catch (Exception ex) { _message.Text = ex.Message; }
        }

        private void ShowSelected()
        {
            var plan = _grid.CurrentRow?.DataBoundItem as MaintenancePlanRecord;
            if (plan == null) return;
            _editingId = plan.Id;
            _vehicle.Enabled = false;
            _vehicle.SelectedValue = plan.VehicleId;
            _name.Text = plan.Name;
            _active.Checked = plan.IsActive;
            _byDate.Checked = plan.IntervalDays.HasValue;
            _byMileage.Checked = plan.IntervalMileage.HasValue;
            if (plan.IntervalDays.HasValue) _days.Value = plan.IntervalDays.Value;
            if (plan.IntervalMileage.HasValue) _kilometers.Value = plan.IntervalMileage.Value;
            if (plan.NextDueDate.HasValue) _nextDate.Value = plan.NextDueDate.Value;
            if (plan.NextDueMileage.HasValue) _nextMileage.Value = plan.NextDueMileage.Value;
            _complete.Enabled = plan.IsActive && AccessClient.Current?.CanWrite == true;
        }

        private void ClearEditor()
        {
            _grid.ClearSelection();
            _editingId = 0;
            _vehicle.Enabled = true;
            _name.Clear();
            _active.Checked = true;
            _byDate.Checked = false;
            _byMileage.Checked = false;
            _nextDate.Value = DateTime.Today.AddMonths(6);
            _nextMileage.Value = 0;
            _complete.Enabled = false;
        }

        private async Task SaveAsync()
        {
            if (!(_vehicle.SelectedItem is PlanningVehicleOption vehicle))
            {
                _message.Text = "Selecione um veículo.";
                return;
            }
            try
            {
                if (_editingId == 0)
                    await OperationsPlanningClient.CreatePlanAsync(vehicle.Id, _name.Text,
                        _byDate.Checked ? (int?)_days.Value : null,
                        _byMileage.Checked ? (int?)_kilometers.Value : null,
                        _byDate.Checked ? (DateTime?)_nextDate.Value : null,
                        _byMileage.Checked ? (int?)_nextMileage.Value : null, _active.Checked);
                else
                    await OperationsPlanningClient.UpdatePlanAsync(_editingId, vehicle.Id, _name.Text,
                        _byDate.Checked ? (int?)_days.Value : null,
                        _byMileage.Checked ? (int?)_kilometers.Value : null,
                        _byDate.Checked ? (DateTime?)_nextDate.Value : null,
                        _byMileage.Checked ? (int?)_nextMileage.Value : null, _active.Checked);
                await LoadPageAsync();
                _message.Text = "Plano salvo.";
            }
            catch (Exception ex) { _message.Text = ex.Message; }
        }

        private async Task CompleteAsync()
        {
            if (_editingId == 0) return;
            try
            {
                await OperationsPlanningClient.CompletePlanAsync(_editingId, _completedOn.Value,
                    (int)_completedMileage.Value, _amount.Value, _notes.Text);
                await LoadPageAsync();
                _message.Text = "Manutenção registrada e plano avançado.";
            }
            catch (Exception ex) { _message.Text = ex.Message; }
        }
    }
}
