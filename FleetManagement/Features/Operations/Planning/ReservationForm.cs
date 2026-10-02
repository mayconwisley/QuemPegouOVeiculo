using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetManagement.Desktop.Client.Infrastructure.Api;

namespace FleetManagement
{
    public sealed class ReservationForm : Form
    {
        private readonly DataGridView _grid = new DataGridView { Dock = DockStyle.Fill,
            ReadOnly = true, AutoGenerateColumns = false, AllowUserToAddRows = false,
            RowHeadersVisible = false, MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect };
        private readonly ComboBox _filterStatus = new ComboBox { Width = 145,
            DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox _vehicle = new ComboBox { Width = 230,
            DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox _driver = new ComboBox { Width = 230,
            DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker _start = new DateTimePicker { Width = 160,
            Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm",
            ShowUpDown = true };
        private readonly DateTimePicker _end = new DateTimePicker { Width = 160,
            Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm",
            ShowUpDown = true };
        private readonly TextBox _purpose = new TextBox { Width = 450, MaxLength = 500 };
        private readonly Label _message = new Label { AutoSize = true };
        private readonly Button _previous = new Button { Text = "Anterior", Width = 85 };
        private readonly Button _next = new Button { Text = "Próxima", Width = 85 };
        private readonly Button _new = new Button { Text = "Nova reserva", Width = 115 };
        private readonly Button _save = new Button { Text = "Salvar", Width = 85 };
        private readonly Button _cancel = new Button { Text = "Cancelar reserva", Width = 125 };
        private readonly Button _startMovement = new Button { Text = "Iniciar saída", Width = 110 };
        private int _page = 1;
        private int _editingId;
        private bool _busy;
        private PlanningPage<ReservationRecord> _result;

        public ReservationForm()
        {
            Text = "Reservas de veículos";
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.FromArgb(244, 247, 251);
            MinimumSize = new Size(920, 590);

            _filterStatus.Items.AddRange(new object[] { "Todos", "Confirmada", "Em uso",
                "Concluída", "Cancelada" });
            _filterStatus.SelectedIndex = 0;
            var toolbar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48,
                Padding = new Padding(14, 8, 0, 0) };
            toolbar.Controls.AddRange(new Control[] { new Label { Text = "Status", AutoSize = true,
                Padding = new Padding(0, 6, 0, 0) }, _filterStatus, _previous, _next, _new,
                _message });

            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID",
                DataPropertyName = "Id", Width = 55 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Veículo",
                DataPropertyName = "Vehicle", Width = 180 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Motorista",
                DataPropertyName = "Driver", Width = 170 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Início",
                DataPropertyName = "Start", Width = 125 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fim",
                DataPropertyName = "End", Width = 125 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Status",
                DataPropertyName = "StatusLabel", Width = 95 });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Finalidade",
                DataPropertyName = "Purpose", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Saída ID",
                DataPropertyName = "MovementId", Width = 75 });

            var editor = new Panel { Dock = DockStyle.Bottom, Height = 205, BackColor = Color.White };
            AddField(editor, "Veículo", _vehicle, 18, 12);
            AddField(editor, "Motorista", _driver, 270, 12);
            AddField(editor, "Início", _start, 18, 72);
            AddField(editor, "Fim", _end, 205, 72);
            AddField(editor, "Finalidade", _purpose, 385, 72);
            _save.Location = new Point(18, 160);
            _cancel.Location = new Point(115, 160);
            _startMovement.Location = new Point(253, 160);
            editor.Controls.AddRange(new Control[] { _save, _cancel, _startMovement });

            Controls.Add(_grid);
            Controls.Add(editor);
            Controls.Add(toolbar);

            _grid.SelectionChanged += (sender, args) => ShowSelected();
            _new.Click += (sender, args) => ClearEditor();
            _save.Click += async (sender, args) => await SaveAsync();
            _cancel.Click += async (sender, args) => await CancelAsync();
            _startMovement.Click += async (sender, args) => await StartAsync();
            _filterStatus.SelectedIndexChanged += async (sender, args) =>
            {
                if (IsHandleCreated) { _page = 1; await LoadPageAsync(); }
            };
            _previous.Click += async (sender, args) =>
            {
                if (_page > 1) { _page--; await LoadPageAsync(); }
            };
            _next.Click += async (sender, args) => { _page++; await LoadPageAsync(); };
            Shown += async (sender, args) =>
            {
                try
                {
                    _vehicle.DataSource = (await OperationsPlanningClient.GetVehiclesAsync()).ToList();
                    _vehicle.DisplayMember = "Label";
                    _vehicle.ValueMember = "Id";
                    _driver.DataSource = (await ReservationClient.GetDriversAsync()).ToList();
                    _driver.DisplayMember = "Label";
                    _driver.ValueMember = "Id";
                    await LoadPageAsync();
                }
                catch (Exception ex) { _message.Text = ex.Message; }
            };
            ClearEditor();
        }

        private static void AddField(Control panel, string caption, Control field, int left, int top)
        {
            panel.Controls.Add(new Label { Text = caption, Location = new Point(left, top),
                AutoSize = true });
            field.Location = new Point(left, top + 22);
            panel.Controls.Add(field);
        }

        private string SelectedStatus => new[] { null, "Confirmed", "InUse", "Completed",
            "Cancelled" }[_filterStatus.SelectedIndex];

        private async Task LoadPageAsync()
        {
            if (_busy) return;
            _busy = true;
            try
            {
                _result = await ReservationClient.GetAsync(_page, status: SelectedStatus);
                _grid.DataSource = _result.Items.ToList();
                _previous.Enabled = _page > 1;
                _next.Enabled = _page * 50 < _result.Total;
                _message.Text = $"Página {_page} · {_result.Total} reservas";
                if (_result.Items.Count == 0) ClearEditor();
            }
            catch (Exception ex) { _message.Text = ex.Message; }
            finally { _busy = false; }
        }

        private void ShowSelected()
        {
            var selected = _grid.CurrentRow?.DataBoundItem as ReservationRecord;
            if (selected == null) return;
            _editingId = selected.Id;
            _vehicle.SelectedValue = selected.VehicleId;
            _vehicle.Enabled = false;
            _driver.SelectedValue = selected.DriverId;
            _start.Value = selected.StartUtc.ToLocalTime();
            _end.Value = selected.EndUtc.ToLocalTime();
            _purpose.Text = selected.Purpose;
            var writable = AccessClient.Current?.CanWrite == true && selected.Status == "Confirmed";
            _save.Enabled = _cancel.Enabled = writable;
            _startMovement.Enabled = writable && DateTime.UtcNow >= selected.StartUtc.AddMinutes(-15)
                && DateTime.UtcNow < selected.EndUtc;
        }

        private void ClearEditor()
        {
            _grid.ClearSelection();
            _editingId = 0;
            _vehicle.Enabled = true;
            _start.Value = DateTime.Now.AddHours(1);
            _end.Value = DateTime.Now.AddHours(2);
            _purpose.Clear();
            _save.Enabled = AccessClient.Current?.CanWrite == true;
            _cancel.Enabled = _startMovement.Enabled = false;
        }

        private async Task SaveAsync()
        {
            if (_busy) return;
            if (!(_vehicle.SelectedItem is PlanningVehicleOption vehicle) ||
                !(_driver.SelectedItem is PlanningDriverOption driver))
            {
                _message.Text = "Selecione um veículo e um motorista.";
                return;
            }
            if (!driver.Active)
            {
                _message.Text = "Selecione um motorista ativo.";
                return;
            }
            if (_end.Value <= _start.Value || string.IsNullOrWhiteSpace(_purpose.Text))
            {
                _message.Text = "Informe a finalidade e um fim posterior ao início.";
                return;
            }
            _busy = true;
            try
            {
                if (_editingId == 0)
                    await ReservationClient.CreateAsync(vehicle.Id, driver.Id,
                        _start.Value, _end.Value, _purpose.Text);
                else
                    await ReservationClient.UpdateAsync(_editingId, vehicle.Id, driver.Id,
                        _start.Value, _end.Value, _purpose.Text);
                _busy = false;
                await LoadPageAsync();
                _message.Text = "Reserva salva.";
            }
            catch (Exception ex) { _message.Text = ex.Message; }
            finally { _busy = false; }
        }

        private async Task CancelAsync()
        {
            if (_busy || _editingId == 0) return;
            if (MessageBox.Show(this, "Cancelar esta reserva?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            _busy = true;
            try
            {
                await ReservationClient.CancelAsync(_editingId);
                _busy = false;
                await LoadPageAsync();
                _message.Text = "Reserva cancelada.";
            }
            catch (Exception ex) { _message.Text = ex.Message; }
            finally { _busy = false; }
        }

        private async Task StartAsync()
        {
            if (_busy || _editingId == 0) return;
            using (var dialog = new Form { Text = "Iniciar saída", Font = Font,
                FormBorderStyle = FormBorderStyle.FixedDialog, StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false, MinimizeBox = false, ClientSize = new Size(350, 170) })
            {
                var mileage = new NumericUpDown { Location = new Point(15, 37), Width = 145,
                    Maximum = int.MaxValue, ThousandsSeparator = true };
                var description = new TextBox { Location = new Point(15, 95), Width = 310 };
                var confirm = new Button { Text = "Iniciar", Location = new Point(240, 132),
                    DialogResult = DialogResult.OK };
                dialog.Controls.AddRange(new Control[] {
                    new Label { Text = "KM inicial", Location = new Point(15, 16), AutoSize = true },
                    mileage,
                    new Label { Text = "Descrição da saída", Location = new Point(15, 74), AutoSize = true },
                    description, confirm });
                dialog.AcceptButton = confirm;
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                _busy = true;
                try
                {
                    await ReservationClient.StartAsync(_editingId, (int)mileage.Value, description.Text);
                    _busy = false;
                    await LoadPageAsync();
                    _message.Text = "Saída iniciada. Registre a chegada na tela de chegada.";
                }
                catch (Exception ex) { _message.Text = ex.Message; }
                finally { _busy = false; }
            }
        }
    }
}
