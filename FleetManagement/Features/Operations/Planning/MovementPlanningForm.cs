using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class MovementPlanningForm : Form
    {
        private readonly DataGridView _grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true,
            AutoGenerateColumns = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false, AllowUserToAddRows = false, RowHeadersVisible = false };
        private readonly ComboBox _status = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 145 };
        private readonly ComboBox _phase = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 140 };
        private readonly DateTimePicker _expectedReturn = new DateTimePicker { Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy HH:mm", ShowUpDown = true, ShowCheckBox = true, Width = 190 };
        private readonly CheckBox _tires = new CheckBox { Text = "Pneus", AutoSize = true };
        private readonly CheckBox _lights = new CheckBox { Text = "Luzes", AutoSize = true };
        private readonly CheckBox _fluids = new CheckBox { Text = "Fluidos", AutoSize = true };
        private readonly CheckBox _body = new CheckBox { Text = "Lataria", AutoSize = true };
        private readonly TextBox _notes = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical };
        private readonly Label _message = new Label { AutoSize = true };
        private readonly Button _previous = new Button { Text = "Anterior", Width = 85 };
        private readonly Button _next = new Button { Text = "Próxima", Width = 85 };
        private readonly Button _saveReturn = new Button { Text = "Salvar previsão", Width = 130 };
        private readonly Button _saveChecklist = new Button { Text = "Salvar checklist", Width = 135 };
        private int _page = 1;
        private PlanningPage<MovementPlanningRecord> _result;
        private MovementPlanningRecord _selected;
        private ChecklistRecord[] _checklists = new ChecklistRecord[0];

        public MovementPlanningForm()
        {
            Text = "Previsões e checklists";
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(244, 247, 251);
            MinimumSize = new Size(860, 620);

            _status.Items.AddRange(new object[] { "Em aberto", "Concluídas" });
            _status.SelectedIndex = 0;
            _phase.Items.AddRange(new object[] { "Saída", "Chegada" });
            _phase.SelectedIndex = 0;

            var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 47, Padding = new Padding(15, 9, 0, 0) };
            toolbar.Controls.Add(new Label { Text = "Movimentações", AutoSize = true, Padding = new Padding(0, 5, 6, 0) });
            toolbar.Controls.Add(_status);
            toolbar.Controls.Add(_previous);
            toolbar.Controls.Add(_next);
            toolbar.Controls.Add(_message);

            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 65 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Veículo", DataPropertyName = "Vehicle",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Motorista", DataPropertyName = "Driver", Width = 190 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Saída", DataPropertyName = "Departure", Width = 145 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Retorno previsto", DataPropertyName = "ExpectedReturn", Width = 145 });

            var details = new Panel { Dock = DockStyle.Bottom, Height = 260, BackColor = Color.White };
            details.Controls.Add(new Label { Text = "Retorno previsto", Location = new Point(20, 14), AutoSize = true });
            _expectedReturn.Location = new Point(20, 38);
            details.Controls.Add(_expectedReturn);
            _saveReturn.Location = new Point(220, 37);
            details.Controls.Add(_saveReturn);
            details.Controls.Add(new Label { Text = "Checklist", Location = new Point(20, 82), AutoSize = true });
            _phase.Location = new Point(20, 105);
            details.Controls.Add(_phase);
            var checks = new FlowLayoutPanel { Location = new Point(180, 104), Width = 430, Height = 33 };
            checks.Controls.AddRange(new Control[] { _tires, _lights, _fluids, _body });
            details.Controls.Add(checks);
            details.Controls.Add(new Label { Text = "Observações", Location = new Point(20, 146), AutoSize = true });
            _notes.SetBounds(20, 169, 580, 68);
            details.Controls.Add(_notes);
            _saveChecklist.Location = new Point(620, 200);
            details.Controls.Add(_saveChecklist);

            Controls.Add(_grid);
            Controls.Add(details);
            Controls.Add(toolbar);

            Shown += async (sender, args) => await LoadPageAsync();
            _status.SelectedIndexChanged += async (sender, args) => { _page = 1; await LoadPageAsync(); };
            _previous.Click += async (sender, args) => { if (_page > 1) { _page--; await LoadPageAsync(); } };
            _next.Click += async (sender, args) => { _page++; await LoadPageAsync(); };
            _grid.SelectionChanged += async (sender, args) => await LoadSelectedAsync();
            _phase.SelectedIndexChanged += (sender, args) => ApplyChecklist();
            _saveReturn.Click += async (sender, args) => await SaveReturnAsync();
            _saveChecklist.Click += async (sender, args) => await SaveChecklistAsync();
        }

        private async Task LoadPageAsync()
        {
            try
            {
                _result = await OperationsPlanningClient.GetMovementsAsync(_status.SelectedIndex == 0, _page);
                _grid.DataSource = _result.Items.ToList();
                _previous.Enabled = _page > 1;
                _next.Enabled = _page * 100 < _result.Total;
                _message.Text = $"Página {_page} · {_result.Total} registros";
                await LoadSelectedAsync();
            }
            catch (Exception ex) { _message.Text = ex.Message; }
        }

        private async Task LoadSelectedAsync()
        {
            _selected = _grid.CurrentRow?.DataBoundItem as MovementPlanningRecord;
            _saveReturn.Enabled = _selected != null && _selected.ArrivalUtc == null && AccessClient.Current?.CanWrite == true;
            _saveChecklist.Enabled = _selected != null && AccessClient.Current?.CanWrite == true;
            if (_selected == null) return;
            var movementId = _selected.Id;
            _expectedReturn.Checked = _selected.ExpectedReturnUtc.HasValue;
            if (_selected.ExpectedReturnUtc.HasValue)
                _expectedReturn.Value = _selected.ExpectedReturnUtc.Value.ToLocalTime();
            if (_selected.ArrivalUtc == null && _phase.SelectedIndex == 1)
                _phase.SelectedIndex = 0;
            else if (_selected.ArrivalUtc != null)
                _phase.SelectedIndex = 1;
            try
            {
                var items = await OperationsPlanningClient.GetChecklistsAsync(movementId);
                if (_selected?.Id != movementId) return;
                _checklists = items.ToArray();
                ApplyChecklist();
            }
            catch (Exception ex) { _message.Text = ex.Message; }
        }

        private void ApplyChecklist()
        {
            var phase = _phase.SelectedIndex == 0 ? "departure" : "arrival";
            var item = _checklists.FirstOrDefault(x => x.Phase == phase);
            _tires.Checked = item?.TiresOk ?? false;
            _lights.Checked = item?.LightsOk ?? false;
            _fluids.Checked = item?.FluidsOk ?? false;
            _body.Checked = item?.BodyOk ?? false;
            _notes.Text = item?.Notes ?? "";
            _saveChecklist.Enabled = _selected != null && AccessClient.Current?.CanWrite == true &&
                (phase == "departure" || _selected.ArrivalUtc != null);
        }

        private async Task SaveReturnAsync()
        {
            if (_selected == null) return;
            try
            {
                await OperationsPlanningClient.ScheduleReturnAsync(_selected.Id,
                    _expectedReturn.Checked ? (DateTime?)_expectedReturn.Value : null);
                _message.Text = "Previsão salva.";
                await LoadPageAsync();
            }
            catch (Exception ex) { _message.Text = ex.Message; }
        }

        private async Task SaveChecklistAsync()
        {
            if (_selected == null) return;
            try
            {
                await OperationsPlanningClient.SaveChecklistAsync(_selected.Id,
                    _phase.SelectedIndex == 0 ? "departure" : "arrival", _tires.Checked,
                    _lights.Checked, _fluids.Checked, _body.Checked, _notes.Text);
                _message.Text = "Checklist salvo.";
                await LoadSelectedAsync();
            }
            catch (Exception ex) { _message.Text = ex.Message; }
        }
    }
}
